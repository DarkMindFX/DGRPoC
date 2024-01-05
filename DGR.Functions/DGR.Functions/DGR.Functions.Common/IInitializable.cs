using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGR.Functions.Common
{
    public interface IInitParams
    {
        public Dictionary<string, string> Parameters
        {
            get;
            set;
        }
    }

    public interface IInitializable
    {
        void Init(IInitParams initParams);

        IInitParams CreateInitParams();
    }
}
