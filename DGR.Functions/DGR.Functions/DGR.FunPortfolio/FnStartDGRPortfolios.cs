using Azure.Identity;
using Azure.Storage.Queues;
using DGR.DAL;
using DGR.DTO;
using DGR.Functions.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DGR.FunPortfolio
{
    
    public class FnStartDGRPortfolios
    {
        DGRPoCDal _dal;

        public FnStartDGRPortfolios(DGRPoCDal dal)
        {
            _dal = dal;
        }

        [FunctionName("FnStartDGRPortfolios")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "GET", "POST", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"FnStartDGRPortfolios: {req.Body}");
            try
            {
                var funHelper = new FunctionHelper();

                string responseMessage = default;

                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var msgObject = JsonConvert.DeserializeObject<StartDGRPortfoliosMsg>(requestBody);
                if (msgObject != null)
                {
                    string storageConnStr = funHelper.GetEnvironmentVariable<string>(Constants.ENV_AZSTORAGE_CONNECTION_STRING);
                    string destQueue = funHelper.GetEnvironmentVariable<string>(Constants.ENV_Q_PORTFOLIOS);

                    // Instantiate a QueueClient to create and interact with the queue
                    QueueClient queueClient = new QueueClient(
                        storageConnStr,
                        destQueue,
                        new QueueClientOptions{
                            MessageEncoding = QueueMessageEncoding.Base64
                        }
                    );

                    // Create the queue
                    await queueClient.CreateAsync();

                    /* Getting list of portfolios and sending to queue */
                    var tblPortfolios = _dal.GetAllPortfolios();
                    foreach (DataRow r in tblPortfolios.Rows)
                    {
                        var splitMSg = new SplitPortfolioMsg()
                        {
                            PortfolioID = (long)(r["ID"]),
                            BusinessDate = msgObject.BusinessDate
                        };

                        log.LogInformation($"Requesting process Portfolio: {splitMSg.PortfolioID}");

                        await queueClient.SendMessageAsync(JsonConvert.SerializeObject(splitMSg) );
                    }

                    responseMessage = $"fnStartDGRPortfolios initiated at {DateTime.Now}";
                }

                return new OkObjectResult(responseMessage);
            }
            catch(Exception ex)
            {
                log.LogError($"{ex.Message}\r\nStackTrace: {ex.StackTrace}");
                var result = new ObjectResult(ex.Message)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };

                return result;
            }
        }
    }
}
