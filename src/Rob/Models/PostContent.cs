using System;
using MongoDB.Bson;

namespace Rob.Models
{
    public class Res 
    {
        public PostContent result { get; set; }
    }
    public class PostContent
    {        
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }        
        public string Tags { get; set; }
        public string Image { get; set; }
    }
}