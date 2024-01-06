using DGR.Functions.Common;
using System.Data;
using System.Data.SqlClient;

namespace DGR.DAL
{
    public class DGRPoCDal : BaseDAL
    {
        public DataTable GetAllPortfolios()
        {
            DataTable result = null;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("dbo.p_GetPortfolios", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count >= 1)
                {
                    result = ds.Tables[0];
                }
            }
            return result;

        }

        public DataTable GetPortfolioValues(long portfolioID, DateTime businessDate)
        {
            DataTable result = null;

            using (SqlConnection conn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand("dbo.p_GetPortfolioValues", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                var pPortfolioID = new SqlParameter("@PortfolioID", portfolioID);
                var pBusinessDate = new SqlParameter("@BusinessDate", businessDate);

                cmd.Parameters.Add(pPortfolioID);
                cmd.Parameters.Add(pBusinessDate);

                var ds = FillDataSet(cmd);

                if (ds.Tables.Count >= 1)
                {
                    result = ds.Tables[0];
                }
            }

            return result;
        }
        
    }
}