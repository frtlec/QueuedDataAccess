// See https://aka.ms/new-console-template for more information


using RestSharp;

List<Task> tasks = new List<Task>();

for (int i = 0; i < 300; i++)
{
	var task = Task.Run(async () =>
	{
		var options = new RestClientOptions
		{
			MaxTimeout = -1,
		};
		var client = new RestClient(options);
		var request = new RestRequest("https://localhost:7161/getactivity", Method.Get);
		RestResponse response = await client.ExecuteAsync(request);
	});
	tasks.Add(task);
}



Task.WaitAll(tasks.ToArray());
