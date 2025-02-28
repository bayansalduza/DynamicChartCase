using System.Collections.Generic;
using DynamicChartCase.Domain.Entities;

namespace DynamicChartCase.Application.Services
{
    public interface IMetadataService
    {
        List<DataSourceInfo> GetDataSources(ConnectionParameters connParams);
    }
}
