using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DGR.DTO
{
    public class FileReadyMsg
    {
        [JsonPropertyName("PortfolioID")]
        public long PortfolioID { get; set; }

        [JsonPropertyName("BusinessDate")]
        public DateTime BusinessDate { get; set; }

        [JsonPropertyName("FilePath")]
        public string FilePath { get; set; }

    }
}
