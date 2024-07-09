using Microsoft.AspNetCore.Mvc;

namespace ZMHDotNetCore.MvcChartApp.Controllers
{
    public class HighChatController : Controller
    {
        private readonly ILogger<HighChatController> _logger;

		public HighChatController(ILogger<HighChatController> logger)
		{
			_logger = logger;
		}

		public IActionResult RadialBarChart()
        {
            _logger.LogInformation("high chart - radial bar chart....");
            return View();
        }
    }
}
