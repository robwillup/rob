using System;

namespace Rob.Models
{
    public class Post
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string SubTitle { get; set; }
        public string Text { get; set; }
        public string Tags { get; set; }
        public string Image { get; set; }
    }
}