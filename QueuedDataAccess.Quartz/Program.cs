// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.AspNetCore;
using QueuedDataAccess.Quartz;

CreateHostBuilder(args).Build().Run();

static IHostBuilder CreateHostBuilder(string[] args) =>
		Host.CreateDefaultBuilder(args)
				.ConfigureServices((hostContext, services) =>
				{
					services.AddQuartz(q =>
					{
						// Just use the name of your job that you created in the Jobs folder.
						var jobKey = new JobKey("ActivityJob");
						q.AddJob<ActivityJob>(opts => opts.WithIdentity(jobKey));

						q.AddTrigger(opts => opts
								.ForJob(jobKey)
								.WithIdentity("ActivityJob-trigger")
								 //This Cron interval can be described as "run every minute" (when second is zero)
								 .WithSimpleSchedule(x => x
								.WithIntervalInSeconds(10)
								.RepeatForever())
						);
					});

					// ASP.NET Core hosting
					services.AddQuartzServer(options =>
					{
						// when shutting down we want jobs to complete gracefully
						options.WaitForJobsToComplete = true;
					});

				});
