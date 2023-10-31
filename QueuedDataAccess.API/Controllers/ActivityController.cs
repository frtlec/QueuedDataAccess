using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QueuedDataAccess.Data;

namespace QueuedDataAccess.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ActivityController : ControllerBase
	{
		private readonly QueProjectDbContext _context;

		public ActivityController(QueProjectDbContext context)
		{
			_context = context;
		}
		[HttpGet]
		public IActionResult GetCount() 
		{
			//hepsini çekiyoruz ki db zorlansın

			var data = _context.Activity.OrderByDescending(f=>f.TransactionDate).AsEnumerable();

			return Ok(data);
		
		}

	}
}
