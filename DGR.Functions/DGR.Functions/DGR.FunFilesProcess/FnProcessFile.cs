using System;
using System.Collections.Generic;
using System.Text.Json;
using DGR.DTO;
using DGR.Functions.Common;
using Microsoft.Azure.Management.DataFactory;
using Microsoft.Azure.Management.DataFactory.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.OData.Edm;
using Microsoft.Rest;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace DGR.FunFilesProcess
{
    public class FnProcessFile
    {
        [FunctionName("FnProcessFile")]
        public void Run([QueueTrigger("dgr-files-queue", Connection = "AzureWebJobsStorage")] string message, ILogger log)
        {
            log.LogInformation($"FnProcessFile: {message}");

            try
            {

                FileReadyMsg msgObject = JsonSerializer.Deserialize<DGR.DTO.FileReadyMsg>(message);
                if (msgObject != null)
                {
                    var funHelper = new FunctionHelper();

                    var client = PrepareClient(funHelper);

                    string resourceGroup = funHelper.GetEnvironmentVariable<string>(Constants.ENV_AZ_RESGROUP);
                    string factoryName = funHelper.GetEnvironmentVariable<string>(Constants.ENV_AZ_FACTORY);
                    string pipelineName = funHelper.GetEnvironmentVariable<string>(Constants.ENV_AZ_PIPELINE);

                    // ****** Run pipeline *************
                    CreateRunResponse runResponse;
                    PipelineRun pipelineRun;

                    log.LogInformation("Called pipeline with parameters.");

                    // preparing parameters
                    Dictionary<string, object> parameters = new Dictionary<string, object>();
                    parameters["file_path"] = msgObject.FilePath;
                    parameters["business_date"] = msgObject.BusinessDate.ToString();
                    parameters["portfolio_id"] = msgObject.PortfolioID.ToString();

                    // Running pipeline
                    runResponse = client.Pipelines.CreateRunWithHttpMessagesAsync(
                        resourceGroup, factoryName, pipelineName, parameters: parameters).Result.Body;

                    log.LogInformation("Pipeline run ID: " + runResponse.RunId);

                    //Wait and check for pipeline result
                    log.LogInformation("Checking pipeline run status...");
                    bool isRunning = true;
                    while (isRunning)
                    {
                        pipelineRun = client.PipelineRuns.Get(
                            resourceGroup, factoryName, runResponse.RunId);

                        log.LogInformation("Status: " + pipelineRun.Status);

                        if (pipelineRun.Status != "InProgress" && pipelineRun.Status != "Queued")
                            isRunning = false;
                        else
                            System.Threading.Thread.Sleep(15000);

                    }
                }
            }
            catch(Exception ex)
            {
                log.LogError($"{ex.Message}\r\nStackTrace: {ex.StackTrace}");
            }
        }

        private DataFactoryManagementClient PrepareClient(FunctionHelper funHelper)
        {
            string tenantId = funHelper.GetEnvironmentVariable<string>(Constants.ENV_AZ_TENANTID);
            string applicationId = funHelper.GetEnvironmentVariable<string>(Constants.ENV_AZ_APPID);
            string authenticationKey = funHelper.GetEnvironmentVariable<string>(Constants.ENV_AZ_AUTHKEY);
            string subscriptionId = funHelper.GetEnvironmentVariable<string>(Constants.ENV_AZ_SUBSCRIPTIONID);

            //Create a data factory management client
            var context = new AuthenticationContext("https://login.windows.net/" + tenantId);
            ClientCredential cc = new ClientCredential(applicationId, authenticationKey);
            AuthenticationResult result = context.AcquireTokenAsync("https://management.azure.com/", cc).Result;
            ServiceClientCredentials cred = new TokenCredentials(result.AccessToken);
            var client = new DataFactoryManagementClient(cred)
            {
                SubscriptionId = subscriptionId
            };

            return client;

        }
    }
}
