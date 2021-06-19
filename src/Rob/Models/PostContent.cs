using System;

namespace Rob.Models
{
    public class PostContent
    {
        public string id { get; set; }
        public string title { get; set; }
        public DateTime date { get; set; }
        public string subTitle { get; set; }
        public string text { get; set; }
        public string tags { get; set; }
        public string image { get; set; }
    }
}