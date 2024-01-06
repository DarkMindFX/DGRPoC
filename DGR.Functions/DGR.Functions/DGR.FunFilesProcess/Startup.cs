using DGR.Functions.Common;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: FunctionsStartup(typeof(DGR.FunFilesProcess.Startup))]
namespace DGR.FunFilesProcess
{
    public class Startup : FunctionStartupBase
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var dalDGRPoC = InitDal<DGR.DAL.DGRPoCDal>();

            builder.Services.AddSingleton<DGR.DAL.DGRPoCDal>(dalDGRPoC);
        }
    }
}
