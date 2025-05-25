using DustInTheWind.HangfireDemoSimple.JobCreator.Helpers;
using Hangfire;

namespace DustInTheWind.HangfireDemoSimple.JobCreator;

internal class Program
{
    public static void Main(string[] args)
    {
        SetupHangfire();

        EnqueueFireAndForgetJob("queue-1");
        EnqueueFireAndForgetJob("queue-2");
        EnqueueFireAndForgetJob();

        //EnqueueDelayedJob("queue-1", TimeSpan.FromSeconds(10));
        //EnqueueDelayedJob("queue-2", TimeSpan.FromSeconds(20));
        //EnqueueDelayedJob(TimeSpan.FromSeconds(30));

        //EnqueueRecurringJob("my-recurring-job", Cron.Minutely());
    }

    public static void SetupHangfire()
    {
        string connectionString = "Data Source=localhost; Initial Catalog=HangfireDemo; Integrated Security=true; TrustServerCertificate=True";
        GlobalConfiguration.Configuration.UseSqlServerStorage(connectionString);
    }

    private static void EnqueueFireAndForgetJob()
    {
        string jobId = BackgroundJob.Enqueue(
            () => Console.WriteLine($"Hello from fire-and-forget job! Created at: {DateTime.UtcNow.ToCustomString()}"));

        Console.WriteLine($"Fire-and-forget job has been created. Id: {jobId}; Queue: [default].");
    }

    private static void EnqueueFireAndForgetJob(string queueName)
    {
        string jobId = BackgroundJob.Enqueue(
            queueName,
            () => Console.WriteLine($"Hello from fire-and-forget job! Created at: {DateTime.UtcNow.ToCustomString()}"));

        Console.WriteLine($"Fire-and-forget job has been created. Id: {jobId}; Queue: '{queueName}'.");
    }

    private static void EnqueueDelayedJob(TimeSpan delay)
    {
        string jobId = BackgroundJob.Schedule(
            () => Console.WriteLine($"Hello from delayed job! Created at: {DateTime.UtcNow.ToCustomString()}"),
            delay);

        Console.WriteLine($"Delayed job has been created. Id: {jobId}; Queue: [default]; Delay: {delay}");
    }

    private static void EnqueueDelayedJob(string queueName, TimeSpan delay)
    {
        string jobId = BackgroundJob.Schedule(
            queueName,
            () => Console.WriteLine($"Hello from delayed job! Created at: {DateTime.UtcNow.ToCustomString()}"),
            delay);

        Console.WriteLine($"Delayed job has been created. Id: {jobId}; Queue: '{queueName}'; Delay: {delay}");
    }

    private static void EnqueueRecurringJob(string jobId, string cron)
    {
        RecurringJob.AddOrUpdate(
            jobId,
            () => Console.WriteLine($"Hello from recurring job! Created at: {DateTime.UtcNow.ToCustomString()}"),
            cron);

        Console.WriteLine($"Recurring job has been created. Id: '{jobId}'");
    }
}
