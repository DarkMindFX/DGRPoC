using System;
using DGR.DTO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace DGR.FunPortfolio
{
    public class FnProcessFile
    {
        [FunctionName("FnProcessFile")]
        public void Run([QueueTrigger("dgr-files-queue", Connection = "AzureWebJobsStorage")] string message, ILogger log)
        {
            log.LogInformation($"SplitPortfolio: {message}");

            SplitPortfolioMsg msgObject = JsonSerializer.Deserialize<DGR.DTO.SplitPortfolioMsg>(message);
            if (msgObject != null)
            {
            }
        }
    }
}
