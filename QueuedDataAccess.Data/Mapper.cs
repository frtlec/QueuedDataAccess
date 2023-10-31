using AutoMapper;
using QueuedDataAccess.Data.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueuedDataAccess.Data
{
	public class Mapper : Profile
	{
		public Mapper()
		{
			CreateMap<DatabaseWriteMessage.Item, Activity>().ReverseMap();
		}
	}
}
