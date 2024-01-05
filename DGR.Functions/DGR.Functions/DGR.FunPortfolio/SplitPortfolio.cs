using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace DGR.FunPortfolio
{
    public class SplitPortfolio
    {
        [FunctionName("SplitPortfolio")]
        public void Run([QueueTrigger("dgr-portfolios-queue", Connection = "")]string message, ILogger log)
        {
            log.LogInformation($"SplitPortfolio: {message}");
        }
    }
}
