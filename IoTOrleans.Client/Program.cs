using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


using var client = new HttpClient();

// 環境変数からデバイス ID を取得
string deviceId = Environment.MachineName;

// シミュレーション: センサーデータを生成
var sensorData = new { Temperature = 23.5f, Humidity = 60.2f };
var content = new StringContent(JsonSerializer.Serialize(sensorData), Encoding.UTF8, "application/json");

Console.WriteLine($"Device ID: {deviceId}");

// デバイスのデータをAPIに送信
var updateResponse = await client.PostAsync($"http://localhost:5032/api/device/{deviceId}/update", content);
Console.WriteLine("Post:");
Console.WriteLine(await updateResponse.Content.ReadAsStringAsync());

// 最新のセンサーデータを取得
var getResponse = await client.GetAsync($"http://localhost:5032/api/device/{deviceId}");
Console.WriteLine("Get:");
Console.WriteLine(await getResponse.Content.ReadAsStringAsync());

Console.ReadLine();
