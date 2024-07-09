using Microsoft.AspNetCore.Mvc;

namespace ZMHDotNetCore.MvcChartApp.Controllers
{
    public class CanvasJsController : Controller
    {
        public IActionResult CandlestickChart()
        {
            return View();
        }
    }
}
