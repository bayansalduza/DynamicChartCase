using DynamicChartCase.Domain.Entities;

namespace DynamicChartCase.WebAPI.Models
{
    public class ChartDataRequest
    {
        public ConnectionParameters ConnectionParameters { get; set; }

        public string DataSourceName { get; set; }

        public string ObjectType { get; set; }

        public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
    }
}
