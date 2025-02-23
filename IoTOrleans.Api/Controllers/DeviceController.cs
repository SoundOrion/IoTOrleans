using Microsoft.AspNetCore.Mvc;
using Orleans;
using System.Threading.Tasks;
using IoTOrleans.Api.Grains;

namespace IoTOrleans.Api.Controllers;

[ApiController]
[Route("api/device")]
public class DeviceController : ControllerBase
{
    private readonly IGrainFactory _grainFactory;

    public DeviceController(IGrainFactory grainFactory)
    {
        _grainFactory = grainFactory;
    }

    //private readonly IClusterClient _client;

    //public DeviceController(IClusterClient client)
    //{
    //    _client = client;
    //}

    [HttpPost("{deviceId}/update")]
    public async Task<IActionResult> UpdateSensorData(string deviceId, [FromBody] SensorDataModel model)
    {

        var grain2 = _grainFactory.GetGrain<IManagementGrain>(0);
        var hosts = await grain2.GetDetailedHosts(true);
        //var aa = hosts.Select(x => new SiloDetails
        //{
        //    FaultZone = x.FaultZone,
        //    HostName = x.HostName,
        //    IAmAliveTime = x.IAmAliveTime.ToISOString(),
        //    ProxyPort = x.ProxyPort,
        //    RoleName = x.RoleName,
        //    SiloAddress = x.SiloAddress.ToParsableString(),
        //    SiloName = x.SiloName,
        //    StartTime = x.StartTime.ToISOString(),
        //    Status = x.Status.ToString(),
        //    SiloStatus = x.Status,
        //    UpdateZone = x.UpdateZone
        //}).ToArray();

        var grain = _grainFactory.GetGrain<IDeviceGrain>(deviceId);
        await grain.UpdateSensorData(model.Temperature, model.Humidity);
        return Ok("Data updated");
    }

    [HttpGet("{deviceId}")]
    public async Task<IActionResult> GetSensorData(string deviceId)
    {
        var grain = _grainFactory.GetGrain<IDeviceGrain>(deviceId);
        var data = await grain.GetSensorData();
        return Ok(new { data.temperature, data.humidity, data.lastUpdate });
    }
}

public class SensorDataModel
{
    public float Temperature { get; set; }
    public float Humidity { get; set; }
}