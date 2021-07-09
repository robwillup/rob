using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Rob.Models;

namespace Rob.Components
{
    public partial class Post : ComponentBase
    {
        public PostContent OpenPost { get; set; }

        [Inject]
        public HttpClient Http { get; set; }

        [Parameter]
        public string PostTitle { get; set; }

        [Parameter]
        public EventCallback<string> PostTitleChanged { get; set; }
        protected override async Task OnInitializedAsync()
        {
            // "https://rob-api-5cz6keqa3a-wl.a.run.app/posts/60e8cdb84e6e8872b379d4f4"            
            var obj = await Http.GetFromJsonAsync<Res>("http://localhost:5002/posts/60e8cdb84e6e8872b379d4f4");
            OpenPost =  obj.result;
        }
    }
}