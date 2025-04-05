using NBomber.CSharp;
using System.Text;
using System.Net.Http;
using System.Text.Json;

using var httpClient = new HttpClient();
var url = "http://localhost:8080/v1/user";
var data = new Dictionary<string, object>
{
    { "Name", "hugo" },
    { "Email", "huguinho@boss.com" },
    { "Password", "testinho" }
};
var json = JsonSerializer.Serialize(data);
var content  = new StringContent(json, Encoding.UTF8, "application/json");

var scenario1 = Scenario.Create("insertion_users_scenario", async context => {
    var response = await httpClient.PostAsync(url,content);

    return response.IsSuccessStatusCode ? Response.Ok() : Response.Fail();
}).WithoutWarmUp()
    .WithLoadSimulations(
      Simulation.Inject(rate: 50,
      interval: TimeSpan.FromSeconds(1),
      during: TimeSpan.FromSeconds(30)));


var dataTask = new Dictionary<string, object>
{
    { "objetivo", "hugo" },
    { "notas", "huguinho@boss.com" },
    { "category", 2 },
    { "fetio", false },
    { "aDayToComplet", "2025-04-05T12:00:00" }
};

url = "http://localhost:8080/v1/task?userId=db3a5ec0-6172-4a6e-a988-a4294056d3e0";
var jsonTask = JsonSerializer.Serialize(dataTask);
var contentTask  = new StringContent(jsonTask, Encoding.UTF8, "application/json");
var scenario2 = Scenario.Create("insertion_tasks_scenario", async context => {
    var response = await httpClient.PostAsync(url,contentTask);

    return response.IsSuccessStatusCode ? Response.Ok() : Response.Fail();
}).WithoutWarmUp()
    .WithLoadSimulations(
      Simulation.Inject(rate: 500,
      interval: TimeSpan.FromSeconds(1),
      during: TimeSpan.FromSeconds(30)));
NBomberRunner
    .RegisterScenarios(scenario1,scenario2)
    .Run();