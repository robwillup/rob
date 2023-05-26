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

        [Inject]
        public PostManager PostManager { get; set; }
        
        [Parameter]
        public PostPreview[] PostItems { get; set; }

        [Parameter]
        public List<Topic> Topics { get; set; }

        [Parameter]
        public PostContent OpenPost { get; set; }

        [Parameter]
        public string PostTitle { get; set; }

        protected override async Task OnInitializedAsync()
        {
            PostItems = await Http.GetFromJsonAsync<PostPreview[]>("sample-data/post-list.json");
            Topics = await PostManager.ListTopics();
        }
    }
}