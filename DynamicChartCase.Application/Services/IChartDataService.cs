using DynamicChartCase.Domain.Entities;

namespace DynamicChartCase.Application.Services
{
    public interface IChartDataService
    {
        List<Dictionary<string, object>> GetData(ConnectionParameters connParams, string dataSourceName, string objectType, Dictionary<string, object> parameters);
    }
}
