using Rob.Models;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Rob.Services;
using System;
using System.Net.Http;
using System.Net.Http.Json;

namespace Rob.Pages
{
    public partial class Blog : ComponentBase
    {
        [Inject]
        public HttpClient Http { get; set; }
        
        [Parameter]
        public PostPreview[] Posts { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Posts = await Http.GetFromJsonAsync<PostPreview[]>("sample-data/post-list.json");
        }
    }
}