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