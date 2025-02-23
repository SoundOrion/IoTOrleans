using Microsoft.AspNetCore.Mvc;
using Orleans;
using System.Threading.Tasks;
using IoTOrleans.Api.Grains;

namespace IoTOrleans.Api.Controllers;

[ApiController]
[Route("api/device")]
public class DeviceController : ControllerBase
{
    private readonly IClusterClient _client;

    public DeviceController(IClusterClient client)
    {
        _client = client;
    }

    [HttpPost("{deviceId}/update")]
    public async Task<IActionResult> UpdateSensorData(string deviceId, [FromBody] SensorDataModel model)
    {
        var grain = _client.GetGrain<IDeviceGrain>(deviceId);
        await grain.UpdateSensorData(model.Temperature, model.Humidity);
        return Ok("Data updated");
    }

    [HttpGet("{deviceId}")]
    public async Task<IActionResult> GetSensorData(string deviceId)
    {
        var grain = _client.GetGrain<IDeviceGrain>(deviceId);
        var data = await grain.GetSensorData();
        return Ok(new { data.temperature, data.humidity, data.lastUpdate });
    }
}

public class SensorDataModel
{
    public float Temperature { get; set; }
    public float Humidity { get; set; }
}
