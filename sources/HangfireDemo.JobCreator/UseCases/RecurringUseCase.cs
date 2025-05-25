using DustInTheWind.HangfireDemoSimple.JobCreator.Helpers;
using Hangfire;

namespace DustInTheWind.HangfireDemoSimple.JobCreator.UseCases;

internal class RecurringUseCase
{
    public void Execute()
    {
        EnqueuedJob("my-recurring-job", Cron.Minutely());
    }

    private static void EnqueuedJob(string jobId, string cron)
    {
        RecurringJob.AddOrUpdate(
            jobId,
            () => Console.WriteLine($"Hello from recurring job! Created at: {DateTime.UtcNow.ToCustomString()}"),
            cron);

        Console.WriteLine($"Recurring job has been created. Id: '{jobId}'");
    }
}