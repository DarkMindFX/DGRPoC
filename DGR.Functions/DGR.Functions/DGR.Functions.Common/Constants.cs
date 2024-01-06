using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
