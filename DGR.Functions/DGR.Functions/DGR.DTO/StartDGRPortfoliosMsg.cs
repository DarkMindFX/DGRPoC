using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DGR.DTO
{
    public class StartDGRPortfoliosMsg
    {
        [JsonPropertyName("BusinessDate")]
        public DateTime BusinessDate
        {
            get; set;
        }
    }
}
