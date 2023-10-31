using AutoMapper;
using EFCore.BulkExtensions;
using MassTransit;

namespace QueuedDataAccess.Data.Messages
{
	public class DatabaseWriteConsumer : IConsumer<DatabaseWriteMessage>
	{
		public async Task Consume(ConsumeContext<DatabaseWriteMessage> context)
		{
			QueProjectDbContext _context = context.GetServiceOrCreateInstance<QueProjectDbContext>();
			IMapper _mapper = context.GetServiceOrCreateInstance<IMapper>();


			List<Activity> datas = _mapper.Map<List<Activity>>(context.Message.Items);

			await _context.BulkInsertOrUpdateAsync(datas, new BulkConfig
			{
				UpdateByProperties = new List<string>()
				{
					nameof(Activity.BaseIban),nameof(Activity.TargetIban),nameof(Activity.Receipt),nameof(Activity.Total)
				}
			});

		}
	}
}
