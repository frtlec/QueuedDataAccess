using MassTransit;
using Newtonsoft.Json;
using Quartz;
using QueuedDataAccess.Data.Messages;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueuedDataAccess.Quartz
{
	public class ActivityJob : IJob
	{
		public async Task Execute(IJobExecutionContext context)
		{
			try
			{
				var options = new RestClientOptions
				{
					MaxTimeout = -1,
				};
				var client = new RestClient(options);
				var request = new RestRequest("https://localhost:7228/getactivity", Method.Get);
				RestResponse response = await client.ExecuteAsync(request);

				var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
				{
					cfg.Host(new Uri("rabbitmq://localhost/"), h =>
					{
						h.Username("guest");
						h.Password("guest");
					});
				});

				await bus.StartAsync();

				// Mesajı hazırla
				List<DatabaseWriteMessage.Item> yourMessage = JsonConvert.DeserializeObject<List<DatabaseWriteMessage.Item>>(response.Content);

				// Yayıncı oluştur
				var endpoint = await bus.GetSendEndpoint(new Uri("rabbitmq://localhost/queue_name"));

				// Mesajı yayınla
				await endpoint.Send(new DatabaseWriteMessage
				{
					Items= yourMessage
				});

				Console.WriteLine("Mesaj yayınlandı: " + yourMessage);

				await bus.StopAsync();
			}
			catch (Exception ex)
			{

				throw;
			}


		}
	}
}
