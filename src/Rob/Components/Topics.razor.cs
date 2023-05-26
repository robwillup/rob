using Microsoft.AspNetCore.Components;
using Rob.Models;
using Rob.Services;

namespace Rob.Components
{
    public partial class Topics
    {
        [Parameter]
        public List<Topic> TopicList { get; set; }

        [Parameter]
        public EventCallback<List<Topic>> TopicListChanged { get; set; }
    }
}
