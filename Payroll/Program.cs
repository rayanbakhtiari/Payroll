using Application;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Globalization;
using System.Reflection;
using Serilog;

internal class Program
{
    private static void Main(string[] args)
    {
        //Set CultureInfo to en-US in order to prevent CultureInfo at destination runtime.
        SetCultureInfoToEnUS();

        string outputCsvDirectory = $@"{Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location)}/OutputData";
        CreateCsvOutputDirectory(outputCsvDirectory );
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console());
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

    }
    private static void SetCultureInfoToEnUS()
    {
        var cultureInfo = new CultureInfo("en-US");

        CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
        CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
    }
    private static void CreateCsvOutputDirectory(string path)
    {
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
    }
}