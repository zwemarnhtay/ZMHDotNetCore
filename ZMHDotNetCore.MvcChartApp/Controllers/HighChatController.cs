using Microsoft.AspNetCore.Mvc;

namespace ZMHDotNetCore.MvcChartApp.Controllers
{
    public class HighChatController : Controller
    {
        public IActionResult RadialBarChart()
        {
            return View();
        }
    }
}
