using DynamicChartCase.Domain.Entities;
using DynamicChartCase.Domain.Repositories;

namespace DynamicChartCase.Application.Services
{
    public class ChartDataService : IChartDataService
    {
        private readonly IChartRepository _chartRepository;

        public ChartDataService(IChartRepository chartRepository)
        {
            _chartRepository = chartRepository;
        }

        public List<Dictionary<string, object>> GetData(ConnectionParameters connParams, string dataSourceName, string objectType, Dictionary<string, object> parameters
        )
        {
            return _chartRepository.ExecuteDataSource(connParams, dataSourceName, objectType, parameters);
        }
    }
}
