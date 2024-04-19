using Ces.Api;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
public class Program
{
    public static void Main(string[] args)
    {
        var culture = new CultureInfo("pt-BR");
        culture.NumberFormat.NumberDecimalDigits = 2;
        CultureInfo.CurrentCulture = culture;

        Activity.DefaultIdFormat = ActivityIdFormat.W3C;
        Activity.ForceDefaultIdFormat = true;

        try
        {
            CreateHostBuilder(args).Build().Run();
        }
        catch (Exception ex)
        {
            Log.Fatal($"Failed to start {Assembly.GetExecutingAssembly().GetName().Name}", ex);
            throw;
        }

    }
    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        var hostBuilder = Host.CreateDefaultBuilder(args)
             .ConfigureWebHostDefaults(webBuilder =>
             {
                 webBuilder.UseStartup<Startup>();
             }).ConfigureAppConfiguration(configuration =>
             {
                 configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                 configuration.AddJsonFile(
                     $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
                     optional: true);
             })
                .UseSerilog((context, configuration) =>
                {
                    configuration.Enrich.FromLogContext()
                    .Enrich.WithMachineName()
                    .WriteTo.Console()
                    .WriteTo.Elasticsearch(ConfigureElasticSink(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")))
                    .Enrich.WithProperty("Environment", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"))
                    .ReadFrom.Configuration(context.Configuration);
                });

        return hostBuilder;
    }
    private static ElasticsearchSinkOptions ConfigureElasticSink(string environment)
    {
        return new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
        {
            AutoRegisterTemplate = true,
            IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower()}-{environment.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}",
            NumberOfReplicas = 1,
            NumberOfShards = 2
        };
    }
}