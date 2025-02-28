using DynamicChartCase.Domain.Entities;

namespace DynamicChartCase.Domain.Repositories
{
    public interface IChartRepository
    {
        bool TestConnection(ConnectionParameters connParams);
        List<DataSourceInfo> GetDataSources(ConnectionParameters connParams);
        List<Dictionary<string, object>> ExecuteDataSource(ConnectionParameters connParams, string dataSourceName, string objectType, Dictionary<string, object> parameters
        );
    }
}
