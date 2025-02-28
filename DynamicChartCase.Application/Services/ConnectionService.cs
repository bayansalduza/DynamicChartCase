using DynamicChartCase.Domain.Entities;
using DynamicChartCase.Domain.Repositories;

namespace DynamicChartCase.Application.Services
{
    public class ConnectionService : IConnectionService
    {
        private readonly IChartRepository _chartRepository;

        public ConnectionService(IChartRepository chartRepository)
        {
            _chartRepository = chartRepository;
        }

        public bool TestConnection(ConnectionParameters connParams)
        {
            return _chartRepository.TestConnection(connParams);
        }
    }
}
