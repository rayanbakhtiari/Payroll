using Application;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Globalization;
using System.Reflection;
//Set CultureInfo to en-US in order to prevent CultureInfo at destination runtime.
SetCultureInfoToEnUS();
string outputCsvDirectory = $@"{Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}/OutputData";
if (!Directory.Exists(outputCsvDirectory))
    Directory.CreateDirectory(outputCsvDirectory);
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices();
builder.Services.AddPaySlipFileRepositoryWithInputStream(outputCsvDirectory);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();

void SetCultureInfoToEnUS()
{
    var cultureInfo = new CultureInfo("en-US");

    CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
    CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
}
