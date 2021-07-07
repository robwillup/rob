using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Rob.Api.Models;

namespace Rob.Api.Mongo
{
    public class DbConnector
    {
        public MongoClient _client { get; }
        public IMongoDatabase _database { get; }
        public IMongoCollection<Post> _collection { get; }
        public DbConnector(string connStr)
        {
            _client = new MongoClient(connStr);
            _database = _client.GetDatabase("robdb");
            _collection = _database.GetCollection<Post>("posts");
        }        

        public async Task InsertOneDocAsync(Post post)
        {
            await _collection.InsertOneAsync(post);
        }

        public async Task<Post> GetOneDocAsync(string id)
        {            
            return await _collection.Find(x => x.Id == ObjectId.Parse(id)).FirstOrDefaultAsync();
        }
    }
}
