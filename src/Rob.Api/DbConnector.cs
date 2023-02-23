using System.Collections.Generic;
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
        public IMongoCollection<Article> _collection { get; }
        public DbConnector(string connStr)
        {
            _client = new MongoClient(connStr);
            _database = _client.GetDatabase("rob");
            _collection = _database.GetCollection<Article>("article");
        }

        public async Task InsertOneDocAsync(Article post)
        {
            await _collection.InsertOneAsync(post);
        }

        public async Task<List<Article>> GetAllDocs()
        {
            return (await _collection.FindAsync(_ => true)).ToList();
        }

        public async Task<Article> GetOneDocByTitleAsync(string title)
        {
            return await _collection.Find(x => x.Title == title).FirstOrDefaultAsync();
        }
    }
}
