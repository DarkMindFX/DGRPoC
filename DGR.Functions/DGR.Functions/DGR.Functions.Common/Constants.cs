using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace DGR.Functions.Common
{
    public class Constants
    {
        public static string ENV_SQL_CONNECTION_STRING = "DalInitParams__ConnectionString";
        public static string ENV_AZSTORAGE_CONNECTION_STRING = "AzureStorage__ConnectionString";
        public static string ENV_Q_PORTFOLIOS = "Q_Portfolios";
        public static string ENV_Q_FILESINFO = "Q_Files";
        public static string ENV_DATA_CHUNK_SIZE = "Data_Chunk_Size";
        public static string ENV_AZ_TENANTID = "AZ_TenantID";
        public static string ENV_AZ_APPID = "AZ_AppID";
        public static string ENV_AZ_AUTHKEY = "AZ_AuthKey";
        public static string ENV_AZ_SUBSCRIPTIONID = "AZ_SubscriptionID";
        public static string ENV_AZ_RESGROUP = "AZ_ResourceGroup";
        public static string ENV_AZ_FACTORY = "AZ_FactoryName";
        public static string ENV_AZ_PIPELINE = "AZ_PipelineName";
    }
}
