using DynamicChartCase.Domain.Entities;

namespace DynamicChartCase.Application.Services
{
    public interface IConnectionService
    {
        bool TestConnection(ConnectionParameters connParams);
    }
}
