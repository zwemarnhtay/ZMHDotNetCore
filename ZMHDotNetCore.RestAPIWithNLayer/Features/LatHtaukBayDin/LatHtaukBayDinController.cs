using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ZMHDotNetCore.RestAPIWithNLayer.Features.LatHtaukBayDin
{
    [Route("api/[controller]")]
    [ApiController]
    public class LatHtaukBayDinController : ControllerBase
    {
        private async Task<LatHtaukBayDin> getDataAsync()
        {
            var jsonStr = await System.IO.File.ReadAllTextAsync("data.json");
            var model = JsonConvert.DeserializeObject<LatHtaukBayDin>(jsonStr);
            return model!;
        }

        [HttpGet("questions")]
        public async Task<IActionResult> getQuestions()
        {
            var model = await getDataAsync();
            return Ok(model.questions);
        }

        [HttpGet]
        public async Task<IActionResult> getNumberList()
        {
            var model = await getDataAsync();
            return Ok(model.numberList);
        }

        [HttpGet("{qNo}/{No}")]
        public async Task<IActionResult> findAnswer(int qNo, int No)
        {
            var model = await getDataAsync();
            var answer = model.answers.FirstOrDefault(x=> x.questionNo == qNo && x.answerNo == No);
            return Ok(answer);
        }
    }



    public class LatHtaukBayDin
    {
        public Question[] questions { get; set; }
        public Answer[] answers { get; set; }
        public string[] numberList { get; set; }
    }

    public class Question
    {
        public int questionNo { get; set; }
        public string questionName { get; set; }
    }

    public class Answer
    {
        public int questionNo { get; set; }
        public int answerNo { get; set; }
        public string answerResult { get; set; }
    }

}
