using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using QueuedDataAccess.Data;
using QueuedDataAccess.Data.Messages;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<QueProjectDbContext>(
	 opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("QueProjectDb"))
);
builder.Services.AddAutoMapper(typeof(Mapper));
builder.Services.AddScoped<DatabaseWriteConsumer>();
builder.Services.AddMassTransit(x =>
{
	x.AddConsumer<DatabaseWriteConsumer>();
	// Default Port : 5672
	x.UsingRabbitMq((context, cfg) =>
	{
		cfg.Host(new Uri("rabbitmq://localhost/"), "/", host =>
		{
			host.Username("guest");
			host.Password("guest");
		});
		cfg.ReceiveEndpoint("queue_name", e =>
		{
			e.ConfigureConsumer<DatabaseWriteConsumer>(context);
		});
	});
});

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
	var db = scope.ServiceProvider.GetRequiredService<QueProjectDbContext>();
	//Same as the question
	db.Database.Migrate();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors(options => options.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

app.MapControllers();

app.Run();
