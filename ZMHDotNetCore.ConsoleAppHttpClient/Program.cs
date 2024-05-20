// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using System.Text.Json.Serialization;

Console.WriteLine("Hello, World!");

string jsonStr = await File.ReadAllTextAsync("data.json");
var model = JsonConvert.DeserializeObject<MainDTO>(jsonStr);

//Console.WriteLine(jsonStr);
foreach (var item in model.Questions)
{
    Console.WriteLine(item.QuestionNo);
}

Console.ReadLine();

public class MainDTO
{
    public Question[] Questions { get; set; }
    public Answer[] answers { get; set; }
    public string[] numberList { get; set; }
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
