using DustInTheWind.HangfireDemoSimple.JobCreator.UseCases;
using Hangfire;

namespace DustInTheWind.HangfireDemoSimple.JobCreator;

internal class Program
{
    public static void Main(string[] args)
    {
        SetupHangfire();
        CreateJobs();
    }

    public static void SetupHangfire()
    {
        string connectionString = "Data Source=localhost; Initial Catalog=HangfireDemo; Integrated Security=true; TrustServerCertificate=True";
        GlobalConfiguration.Configuration.UseSqlServerStorage(connectionString);
    }

    private static void CreateJobs()
    {
        //FireAndForgetUseCase useCase = new();
        //useCase.Execute();

        //DelayedUseCase useCase = new();
        //useCase.Execute();

        RecurringUseCase useCase = new();
        useCase.Execute();
    }
}
