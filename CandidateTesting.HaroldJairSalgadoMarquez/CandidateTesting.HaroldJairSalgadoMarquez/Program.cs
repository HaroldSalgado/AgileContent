using CandidateTesting.HaroldJairSalgadoMarquez.Business.Managers;
using CandidateTesting.HaroldJairSalgadoMarquez.Business.Managers.Interfaces;
using CandidateTesting.HaroldJairSalgadoMarquez.Business.Validators;
using CandidateTesting.HaroldJairSalgadoMarquez.Business.Validators.Interfaces;
using CandidateTesting.HaroldJairSalgadoMarquez.Data.Facades;
using CandidateTesting.HaroldJairSalgadoMarquez.Data.Facades.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace CandidateTesting.HaroldJairSalgadoMarquez
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var builder = new ConfigurationBuilder();
                BuildConfig(builder);

                if (args[0] == "convert")
                {
                    var host = Host.CreateDefaultBuilder()
                        .ConfigureServices((context, services) =>
                        {
                            services.AddTransient<IApplicationManager,ApplicationManager>();
                            services.AddTransient<ILogConverter, LogConverter>();
                            services.AddTransient<ISourceValidator, SourceValidator>();
                            services.AddTransient<IDataFacade, DataFacade>();
                        }).Build();

                    var applicationManagerService = ActivatorUtilities.CreateInstance<ApplicationManager>(host.Services);
                    var source = args[1];
                    var target = args[2];
                    applicationManagerService.ConvertLogs(source, target);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        }
    }
}
