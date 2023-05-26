using System.Net.Http.Headers;
using System.Text.Json;
using Rob.Models;

namespace Rob.Services
{
    public class PostManager
    {
        public async Task<List<Topic>> ListTopics()
        {
            Uri uri = new("https://api.github.com/repos/robwillup/mithrandir/contents/docs");
            HttpClient http = new();
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "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");
            http.DefaultRequestHeaders.Add("Accept", "application/vnd.github+json");
            http.DefaultRequestHeaders.Add("X-GitHub-Api-Version", "2022-11-28");

            string res = await http.GetStringAsync(uri);

            List<Topic> topics = JsonSerializer.Deserialize<List<Topic>>(res);

            return topics;
        }
    }
}