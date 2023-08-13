using Blazored.LocalStorage;
using DnetIndexedDb;
using DnetIndexedDb.Fluent;
using DnetIndexedDb.Models;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RaceCarInspection.Client;
using RaceCarInspection.Client.Data;
using RaceCarInspection.Client.Services;
using RaceCarInspection.Shared.Models;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IInspectionService, InspectionService>();
builder.Services.AddBlazoredLocalStorageAsSingleton();
builder.Services.AddIndexedDbDatabase<CarInspectionIndexedDb>(options =>
{
    var model = new IndexedDbDatabaseModel()
            .WithName("CarInspections")
            .WithVersion(1)
            .WithModelId(0);
    model.AddStore<Inspection>();
    options.UseDatabase(model);
});

await builder.Build().RunAsync();
