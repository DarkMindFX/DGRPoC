using System;
using System.Text.Json;
using DGR.DAL;
using DGR.DTO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace DGR.FunPortfolio
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

            SplitPortfolioMsg msgObject = JsonSerializer.Deserialize<DGR.DTO.SplitPortfolioMsg>(message);
            if (msgObject != null)
            {
            }
        }
    }
}
