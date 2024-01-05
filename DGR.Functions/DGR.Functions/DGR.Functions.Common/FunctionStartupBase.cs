using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.VisualBasic;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;

namespace DGR.Functions.Common
{
    public abstract class FunctionStartupBase : FunctionsStartup
    {        

        public CompositionContainer Container
        {
            get;
            set;
        }

        public TDal InitDal<TDal>() where TDal : IInitializable, new()
        {
            var funHelper = new FunctionHelper();

            var dal = new TDal();
            var dalInitParams = dal.CreateInitParams();

            dalInitParams.Parameters["ConnectionString"] = funHelper.GetEnvironmentVariable<string>(Constants.ENV_SQL_CONNECTION_STRING);

            dalInitParams.Parameters = dalInitParams.Parameters;
            dal.Init(dalInitParams);

            return dal;
        }

        #region Support methods

        private void PrepareComposition()
        {
            /*
            AggregateCatalog catalog = new AggregateCatalog();
            DirectoryCatalog directoryCatalog = new DirectoryCatalog(AssemblyDirectory);
            catalog.Catalogs.Add(directoryCatalog);
            Container = new CompositionContainer(catalog);
            Container.ComposeParts(this);
            */
        }

        private string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().Location;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        #endregion
    }
}