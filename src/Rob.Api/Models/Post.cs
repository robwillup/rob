using System;
using MongoDB.Bson;

namespace Rob.Api.Models
{
    public class Post
    {
        public Post()
        {
            Id = ObjectId.GenerateNewId();            
        }
        public ObjectId Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }        
        public string Tags { get; set; }
        public string Image { get; set; }
    }    
}