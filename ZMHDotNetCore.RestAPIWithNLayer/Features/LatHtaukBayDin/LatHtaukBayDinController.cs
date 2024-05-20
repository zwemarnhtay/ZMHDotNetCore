using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ZMHDotNetCore.RestAPIWithNLayer.Features.LatHtaukBayDin
{
    [Route("api/[controller]")]
    [ApiController]
    public class LatHtaukBayDinController : ControllerBase
    {
        private async Task<LatHtaukBayDin> GetDataAsync()
        {
            var jsonStr = await System.IO.File.ReadAllTextAsync("data.json");
            var model = JsonConvert.DeserializeObject<LatHtaukBayDin>(jsonStr);
            return model!;
        }

        [HttpGet("questions")]
        public async Task<IActionResult> GetQuestions()
        {
            var model = await GetDataAsync();
            return Ok(model.Questions);
        }

        [HttpGet]
        public async Task<IActionResult> GetNumberList()
        {
            var model = await GetDataAsync();
            return Ok(model.NumberList);
        }

        [HttpGet("{qNo}/{No}")]
        public async Task<IActionResult> FindAnswer(int qNo, int No)
        {
            var model = await GetDataAsync();
            var answer = model.Answers.FirstOrDefault(x=> x.QuestionNo == qNo && x.AnswerNo == No);
            return Ok(answer);
        }
    }



    public class LatHtaukBayDin
    {
        public Question[] Questions { get; set; }
        public Answer[] Answers { get; set; }
        public string[] NumberList { get; set; }
    }

    public class Question
    {
        public int QuestionNo { get; set; }
        public string QuestionName { get; set; }
    }

    public class Answer
    {
        public int QuestionNo { get; set; }
        public int AnswerNo { get; set; }
        public string AnswerResult { get; set; }
    }

}
