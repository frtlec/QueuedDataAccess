var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
DataManager.CreateData();


app.MapGet("/getactivity", () =>
{
	DataManager.CreateData(new Random().Next(100,800));
	return Data.FakeData.OrderByDescending(f=>f.TransactionDate).ToList();
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();




static class DataManager
{
	public static void CreateData(int count=300)
	{

		Parallel.For(1, count, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount - 1 }, x =>
		{
			Data.AddData(new BankAccountActivityModel
			{
				TransactionDate = DateTime.Now,
				ValorDate = DateTime.Now,
				Total = new Random().Next(10,50),
				TargetIban = Guid.NewGuid().ToString().Replace("-", ""),
				BaseIban = Guid.NewGuid().ToString().Replace("-", ""),
				BankCode = "0065",
				Description = $"Test {x}",
				Receipt = x + Guid.NewGuid().ToString().Replace("-", ""),
				RefNo = Guid.NewGuid().ToString().Replace("-", ""),
			});
		});
	}
}
static class Data
{
	private static object _mutext = new object();
	public static List<BankAccountActivityModel> FakeData { get; private set; }

	public static void AddData(BankAccountActivityModel model)
	{
		lock (_mutext)
		{
			if (FakeData==null)
			{
				FakeData = new List<BankAccountActivityModel>();
			}
			model.Balance = GetBalance(model.Total);
			FakeData.Add(model);
		}
	}
	static decimal GetBalance(decimal total)
	{
		var last = FakeData.OrderByDescending(f => f.TransactionDate).Take(1).FirstOrDefault();
		if (last == null)
			return total;
		return last.Balance + total;
	}
}
class BankAccountActivityModel
{
	public DateTime TransactionDate { get; set; }
	public DateTime ValorDate { get; set; }
	public string? BaseIban { get; set; }
	public string? TargetIban { get; set; }
	public string Description { get; set; }
	public string? BankCode { get; set; }
	public decimal Total { get; set; }
	public decimal Balance { get; set; }
	public string Receipt { get; set; }
	public string RefNo { get; set; }
}