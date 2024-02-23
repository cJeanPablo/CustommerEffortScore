using Ces.Api;
using System.Diagnostics;
using System.Globalization;

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
            Console.WriteLine("Iniciando");
            CreateHostBuilder(args).Build().Run();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Falha ao iniciar. Erro: {ex.Message} . Detalhes: {ex.InnerException?.Message ?? ""}");
        }

    }


    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        var hostBuilder = Host.CreateDefaultBuilder(args)
             .ConfigureWebHostDefaults(webBuilder =>
             {
                 webBuilder.UseStartup<Startup>();
             });

        return hostBuilder;
    }
}