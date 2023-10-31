using System.ComponentModel.DataAnnotations;

namespace QueuedDataAccess.Data
{
	public class Activity
	{
		[Key]
		public int Id { get; set; }
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
		public DateTime CreatedDate { get; set; }
		public DateTime UpdatedDate { get; set; }
	}
}