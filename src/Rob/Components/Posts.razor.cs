using Microsoft.AspNetCore.Components;
using Rob.Models;

namespace Rob.Components
{
    public partial class Posts : ComponentBase
    {
        [Parameter]
        public PostPreview[] PostList { get; set; }

        [Parameter]
        public EventCallback<PostPreview[]> PostListChanged { get; set; }

        [Parameter]
        public string PostTitle { get; set; }
        [Parameter]
        public EventCallback<string> PostTitleChanged { get; set; }

        private void OnPostSelected(string title)
        {
            PostTitle = title;            
            PostTitleChanged.InvokeAsync(PostTitle);
        }
    }
}