using System.Collections.Generic;
using DynamicChartCase.Domain.Entities;
using DynamicChartCase.Domain.Repositories;

namespace DynamicChartCase.Application.Services
{
    public class MetadataService : IMetadataService
    {
        private readonly IChartRepository _chartRepository;

        public MetadataService(IChartRepository chartRepository)
        {
            _chartRepository = chartRepository;
        }

        public List<DataSourceInfo> GetDataSources(ConnectionParameters connParams)
        {
            return _chartRepository.GetDataSources(connParams);
        }
    }
}
