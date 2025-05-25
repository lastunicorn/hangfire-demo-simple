using Hangfire;

namespace DustInTheWind.HangfireDemoSimple.JobRunner;

internal class Program
{
    public static void Main(string[] args)
    {
        SetupHangfire();
        RunHangfireServer();
    }

    public static void SetupHangfire()
    {
        string connectionString = "Data Source=localhost; Initial Catalog=HangfireDemo; Integrated Security=true; TrustServerCertificate=True";
        GlobalConfiguration.Configuration.UseSqlServerStorage(connectionString);
    }

    private static void RunHangfireServer()
    {
        BackgroundJobServerOptions options = new()
        {
            Queues = ["default", "queue-1"]
        };

        using (new BackgroundJobServer(options))
        {
            Console.WriteLine("Hangfire Server started. Press ENTER to exit...");
            Console.ReadLine();
        }
    }
}
