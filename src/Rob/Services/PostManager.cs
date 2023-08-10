using System.Net.Http.Headers;
using System.Text.Json;
using Rob.Models;

namespace Rob.Services
{
    public class PostManager
    {
        public async Task<List<Topic>> ListTopics()
        {
            string token = File.ReadAllText("data/token_public_repo.json");
            Uri uri = new("https://api.github.com/repos/robwillup/mithrandir/contents/docs");
            HttpClient http = new();
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            http.DefaultRequestHeaders.Add("Accept", "application/vnd.github+json");
            http.DefaultRequestHeaders.Add("X-GitHub-Api-Version", "2022-11-28");

            string res = await http.GetStringAsync(uri);

            List<Topic> topics = JsonSerializer.Deserialize<List<Topic>>(res);

            return topics;
        }
    }
}