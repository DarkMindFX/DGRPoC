using System;
using System.Text.Json;
using DGR.DTO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace DGR.FunFilesProcess
{
    public class FnProcessFile
    {
        [FunctionName("FnProcessFile")]
        public void Run([QueueTrigger("dgr-files-queue", Connection = "AzureWebJobsStorage")] string message, ILogger log)
        {
            log.LogInformation($"SplitPortfolio: {message}");

            FileReadyMsg msgObject = JsonSerializer.Deserialize<DGR.DTO.FileReadyMsg>(message);
            if (msgObject != null)
            {

            }
        }
    }
}
