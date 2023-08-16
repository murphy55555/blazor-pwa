using Microsoft.AspNetCore.ResponseCompression;
using RaceCarInspection.Server.Data;
using RaceCarInspection.Server.Services;
using Swashbuckle.AspNetCore.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddTransient<ICarInspectionData, CarInspectionData>();
builder.Services.AddTransient<ISyncService, SyncService>();
builder.Services.AddTransient<IPushRegistrations, PushRegistrations>();
builder.Services.AddTransient<IPushNotificationService, PushNotificationService>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();
app.MapFallbackToFile("index.html");

app.Run();
