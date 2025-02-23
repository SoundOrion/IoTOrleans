using IoTOrleans.Api.Grains;
using Orleans;
using System;
using System.Threading.Tasks;

namespace IoTOrleans.Api.Grains;

public class DeviceGrain : Grain, IDeviceGrain
{
    private float _temperature;
    private float _humidity;
    private long _lastUpdate;

    public Task UpdateSensorData(float temperature, float humidity)
    {
        _temperature = temperature;
        _humidity = humidity;
        _lastUpdate = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        return Task.CompletedTask;
    }

    public Task<(float temperature, float humidity, long lastUpdate)> GetSensorData()
    {
        return Task.FromResult((_temperature, _humidity, _lastUpdate));
    }
}
