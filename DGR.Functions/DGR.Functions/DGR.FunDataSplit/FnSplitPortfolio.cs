using Azure.Storage.Blobs;
using System;
using System.Data;
using DGR.DAL;
using DGR.DTO;
using DGR.Functions.Common;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Text;
using Azure.Storage.Queues;
using Newtonsoft.Json;
using System.IO;

namespace DGR.FunDataSplit
{
    public class FnSplitPortfolio
    {
        DGRPoCDal _dal;

        public FnSplitPortfolio(DGRPoCDal dal)
        {
            _dal = dal;
        }

        [FunctionName("fnSplitPortfolio")]
        public void Run([QueueTrigger("dgr-portfolios-queue", Connection = "AzureWebJobsStorage")] string message, ILogger log)
        {
            log.LogInformation($"SplitPortfolio: {message}");

            SplitPortfolioMsg msgObject = System.Text.Json.JsonSerializer.Deserialize<DGR.DTO.SplitPortfolioMsg>(message);
            if (msgObject != null)
            {
                var funHelper = new FunctionHelper();

                DataTable values = _dal.GetPortfolioValues(msgObject.PortfolioID, msgObject.BusinessDate);

                if (values != null)
                {

                    string storageConnStr = funHelper.GetEnvironmentVariable<string>(Constants.ENV_AZSTORAGE_CONNECTION_STRING);
                    string destQueue = funHelper.GetEnvironmentVariable<string>(Constants.ENV_Q_FILESINFO);
                    int chunkSize = Int32.Parse(funHelper.GetEnvironmentVariable<string>(Constants.ENV_DATA_CHUNK_SIZE));

                    // Instantiate a QueueClient to create and interact with the queue
                    QueueClient queueClient = new QueueClient(
                        storageConnStr,
                        destQueue,
                        new QueueClientOptions
                        {
                            MessageEncoding = QueueMessageEncoding.Base64
                        }
                    );

                    // Create the queue
                    queueClient.Create();

                    // init blob client and create root container if needed
                    BlobContainerClient contDGRShare = new BlobContainerClient(storageConnStr, "dgr-share");

                    for (int i = 0; i < values.Rows.Count / chunkSize + (values.Rows.Count % chunkSize > 0 ? 1 : 0); ++i)
                    {
                        // preparing CSV file content
                        StringBuilder fileContent = new StringBuilder();
                        for (int r = i * chunkSize; r < Math.Min((i + 1) * chunkSize, values.Rows.Count); ++r)
                        {
                            var row = values.Rows[r] as DataRow;
                            for (int c = 0; c < values.Columns.Count; ++c)
                            {
                                fileContent.Append(row[c].ToString() + (c + 1 < values.Columns.Count ? "," : ""));
                            }

                            fileContent.Append("\r\n");
                        }

                        // uploading file to blob
                        string destFilePath = $"staging/{msgObject.PortfolioID}/Portfolio-{msgObject.PortfolioID}.{msgObject.BusinessDate.Year}.{msgObject.BusinessDate.Month}.{msgObject.BusinessDate.Day}-chunk{i}.csv";

                        var blob = contDGRShare.GetBlobClient(destFilePath);

                        StreamWriter sr = new StreamWriter(blob.OpenWrite(true));

                        sr.Write(fileContent.ToString());
                        sr.Flush();

                        // if uploaded successfully - sending message to notification queue
                        var fileReadyMsg = new FileReadyMsg()
                        {
                            BusinessDate = msgObject.BusinessDate,
                            FilePath = destFilePath,
                            PortfolioID = msgObject.PortfolioID
                        };

                        queueClient.SendMessageAsync(JsonConvert.SerializeObject(fileReadyMsg));
                    }
                }
            }
        }
    }
}
