using Rob.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Rob.Services
{
    public class PostManager
    {
        public async Task<PostPreview[]> GetPostPreviewsAsync()
        {
            HttpClient http = new HttpClient();
            PostPreview[] res = await http.GetFromJsonAsync<PostPreview[]>("sample-data/post-list.json");
            foreach (var item in res)
            {
                System.Console.WriteLine(item.title);
            }
            return res;
        }
    }
}