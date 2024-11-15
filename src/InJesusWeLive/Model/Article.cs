using System.Text.Json.Serialization;

namespace InJesusWeLive;

public class GetArticles
{
    public string Title { get; set; }
    public DateTime Date { get; set; }
    public string Topic { get; set; }
}