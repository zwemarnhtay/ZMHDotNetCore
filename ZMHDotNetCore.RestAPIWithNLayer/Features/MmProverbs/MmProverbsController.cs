using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ZMHDotNetCore.RestAPIWithNLayer.Features.MmProverbs
{
    [Route("api/[controller]")]
    [ApiController]
    public class MmProverbsController : ControllerBase
    {
        private async Task<MmProverbsDTO> getDtatAsync()
        {
            var jsonData = await System.IO.File.ReadAllTextAsync("MyanmarProverbs.json");
            var modal = JsonConvert.DeserializeObject<MmProverbsDTO>(jsonData);
            return modal!;
        }

        [HttpGet]
        public async Task<IActionResult> get()
        {
            var modal = await getDtatAsync();
            return Ok(modal.ProverbsTitle);
        }

        [HttpGet("{titleName}")]
        public async Task<IActionResult> getByTitle(string titleName)
        {
            var modal = await getDtatAsync();

            var proverbTitle = modal.ProverbsTitle.FirstOrDefault(x => x.TitleName == titleName);
            if (proverbTitle == null) return NotFound("no data found");

            var proverbs = modal.Proverbs.Where(x => x.TitleId == proverbTitle.TitleId);

            List<ProverbsHead> proverbsList = proverbs.Select(x => new ProverbsHead 
            { 
                TitleId = x.TitleId,
                ProverbId = x.ProverbId,
                ProverbName = x.ProverbName
            }).ToList();

            return Ok(proverbsList);
        }

        [HttpGet("{titleId}/{proverbId}")]
        public async Task<IActionResult> getProverbs(int titleId, int proverbId)
        {
            var modal = await getDtatAsync();

            var proverb = modal.Proverbs.FirstOrDefault(x => x.TitleId == titleId && x.ProverbId == proverbId);

            return Ok(proverb);
        }
    }
}


public class MmProverbsDTO
{
    public ProverbsTitle[] ProverbsTitle { get; set; }
    public Proverbs[] Proverbs { get; set; }
}

public class ProverbsTitle
{
    public int TitleId { get; set; }
    public string TitleName { get; set; }
}

public class Proverbs
{
    public int TitleId { get; set; }
    public int ProverbId { get; set; }
    public string ProverbName { get; set; }
    public string ProverbDesp { get; set; }
}

public class ProverbsHead
{
    public int TitleId { get; set; }
    public int ProverbId { get; set; }
    public string ProverbName { get; set; }
}

