using System.Text.Json.Serialization;

namespace DGR.DTO
{
    public class SplitPortfolioMsg
    {
        [JsonPropertyName("PortfolioID")]
        public long PortfolioID { get; set; }

        [JsonPropertyName("BusinessDate")]
        public DateTime BusinessDate { get; set; }

    }
}