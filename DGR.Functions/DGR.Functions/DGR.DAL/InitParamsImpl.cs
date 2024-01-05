using DGR.Functions.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGR.DAL
{
    public class InitParamsImpl : IInitParams
    {
        public InitParamsImpl()
        {
            Parameters = new Dictionary<string, string>();
            Parameters["ConnectionString"] = string.Empty;
        }

        public Dictionary<string, string> Parameters
        {
            get;
            set;
        }
    }
}
