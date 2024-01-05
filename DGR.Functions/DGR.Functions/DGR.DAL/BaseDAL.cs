using DGR.Functions.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGR.DAL
{
    public class BaseDAL : IInitializable
    {
        private string _connString = null;
        public IInitParams CreateInitParams()
        {
            return new InitParamsImpl();
        }

        public void Init(IInitParams initParams)
        {
            _connString = initParams.Parameters["ConnectionString"];
        }

        protected SqlConnection OpenConnection()
        {
            SqlConnection conn = new SqlConnection(_connString);
            conn.Open();

            return conn;
        }

        protected void CloseConnection(SqlConnection conn)
        {
            conn.Close();
        }

        protected DataSet FillDataSet(SqlCommand cmd)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;

            da.Fill(ds);

            return ds;
        }
    }
}
