using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace ZMHDotNetCore.Myanmar_Months.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonthsController : ControllerBase
    {
        private async Task<Months> GetMonthsAsync()
        {
            var jsonData = await System.IO.File.ReadAllTextAsync("MyanmarMonths.json");
            var monthsData = JsonConvert.DeserializeObject<Months>(jsonData);
            return monthsData;
        }

        [HttpGet]
        public async Task<IActionResult> getMonths()
        {
            var dataList = await GetMonthsAsync();
            var months = dataList.Tbl_Months.Select(mth => mth.MonthMm).ToList();
            return Ok(months);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> detailMonth(int id)
        {
            if (id < 1 || id > 12)
            {
                return NotFound("There are only 12 months!");
            }

            var dataList = await GetMonthsAsync();
            var detail = dataList.Tbl_Months.FirstOrDefault(mth => mth.Id == id);
            return Ok(detail);
        }
    }


    public class Months
    {
        public Tbl_Months[] Tbl_Months { get; set; }
    }

    public class Tbl_Months
    {
        public int Id { get; set; }
        public string MonthMm { get; set; }
        public string MonthEn { get; set; }
        public string FestivalMm { get; set; }
        public string FestivalEn { get; set; }
        public string Description { get; set; }
        public string Detail { get; set; }
    }

}
