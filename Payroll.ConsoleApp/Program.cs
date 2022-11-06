// See https://aka.ms/new-console-template for more information
using Application;
using Application.Handlers.MonthlyPaySlip;
using Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddApplicationServices();
        var inputFileAddress = args?.Length > 0 ? args[0] : "input.csv";
        var outputFileAddress = args?.Length> 1 ? args[1] : "output.csv";
        services.AddPaySlipFileRepositoryWithInputOutputFileAddress(inputFileAddress, outputFileAddress);
    })
    .UseSerilog((ctx, lc) =>
    {
        lc.WriteTo.Console();
        lc.ReadFrom.Configuration(ctx.Configuration);
    })
    .Build();
Log.Logger.Information("Starting application");
var mediator = host.Services.GetService<IMediator>();
var result = await mediator.Send(new GetMonthlyPaySlipsQuery());
