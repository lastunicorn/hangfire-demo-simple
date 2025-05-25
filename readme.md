# Hangfire Demo (Simple)

Hangfire is a .NET library for creating and running jobs in the background.

https://www.hangfire.io/

## Overview

This repo contains a simple example of using the Hangfire library.

Hangfire provides three functionalities:

- create jobs
- run jobs
- monitor jobs

These functionalities can be included all in the same application or each of them in a different application.

This is possible because the jobs, when they are created, are first stored into a database and, at a later time, they are retrieved from that database and executed by the Hangfire server. This architecture allows the server to run in a different process if needed.

## Projects

This demo contains 3 projects, one for each functionality:

- **JobCreator** - creates jobs
- **JobRunner** - runs jobs
- **WebDashboard** - monitors job status

## 1) JobCreator

This is a console application that creates a jobs. There are two important steps to keep in mind:

- a) Setup Hangfire
- b) Create jobs

**a) Setup Hangfire**

```c#
string connectionString = "Data Source=localhost; Initial Catalog=HangfireDemo; Integrated Security=true; TrustServerCertificate=True";

GlobalConfiguration.Configuration.UseSqlServerStorage(connectionString);
```

**b) Create Jobs**

The jobs are just stored in the database. The execution will be performed, later, by the JobRunner application.

```c#
// Fire-and-forget job
string jobId = BackgroundJob.Enqueue(
    () => Console.WriteLine("Fire-and-forget!"));

// Delayed job
string jobId = BackgroundJob.Schedule(
    () => Console.WriteLine("Delayed!"),
    TimeSpan.FromDays(7));

// Recurring job
RecurringJob.AddOrUpdate(
    "myrecurringjob",
    () => Console.WriteLine("Recurring!"),
    Cron.Daily);
```

**Note**

> To keep the code as simple as possible, the example is written entirely in the `Program.cs` file. In a real application you may want to create a more suitable design.

Fill free to play with the code. Uncomment the existing functions, change them to create different jobs.

## 2) JobRunner

This is a console application that runs the enqueued jobs existing in the database.

There are two important steps to keep in mind:

- a) Setup Hangfire
- b) Start Hangfire server

**a) Setup Hangfire**

```c#
string connectionString = "Data Source=localhost; Initial Catalog=HangfireDemo; Integrated Security=true; TrustServerCertificate=True";

GlobalConfiguration.Configuration.UseSqlServerStorage(connectionString);
```

**b) Start Hangfire server**

```c#
BackgroundJobServerOptions options = new()
{
    Queues = ["default", "queue-1"]
};

using (new BackgroundJobServer(options))
{
    Console.WriteLine("Hangfire Server started. Press ENTER to exit...");
    Console.ReadLine();
}
```

**Note**

> To keep the code as simple as possible, the example is written entirely in the `Program.cs` file. In a real application you may want to create a more suitable design.

## 3) WebDashboard

This is a web application that displays the Hengfire Dashboard where the user can check the stratus of the enqueued jobs.

The setup includes two steps:

- a) Configure services
- b) Add the Hangfire Dashboard middleware

**a) Configure services**

```c#
string hangfireConnectionString = "Data Source=localhost; Initial Catalog=HangfireDemo; Integrated Security=true; TrustServerCertificate=True";

builder.Services.AddHangfire(configuration => configuration
    .UseSqlServerStorage(hangfireConnectionString));
```

**b) Add the Hangfire Dashboard middleware**

```c#
app.UseHangfireDashboard();
```

