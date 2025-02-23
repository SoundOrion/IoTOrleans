using Orleans;
using System.Threading.Tasks;

namespace IoTOrleans.Api.Grains;

public interface IDeviceGrain : IGrainWithStringKey
{
    Task UpdateSensorData(float temperature, float humidity);
    Task<(float temperature, float humidity, long lastUpdate)> GetSensorData();
}
