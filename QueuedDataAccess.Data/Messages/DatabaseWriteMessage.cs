using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueuedDataAccess.Data.Messages
{
	public class DatabaseWriteMessage
	{
		public List<Item> Items { get; set; } = new List<Item>();
		public class Item
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
	}
}
