using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Rob.Api.Mongo
{
    public class DbConnector
    {
        public MongoClient _client { get; }
        public IMongoDatabase _database { get; }
        public IMongoCollection<BsonDocument> _collection { get; }
        public DbConnector(string connStr)
        {
            _client = new MongoClient(connStr);
            _database = _client.GetDatabase("robdb");
            _collection = _database.GetCollection<BsonDocument>("posts");
        }        

        public async Task InsertOneDocAsync(string name, string value)
        {
            await _collection.InsertOneAsync(new BsonDocument(name, value));
        }

        public async Task<List<BsonDocument>> GetOneDocAsync(string name, string value)
        {
            return await _collection.Find(new BsonDocument(name, value))
            .ToListAsync();
        }

        // foreach(var document in list)
        // {
        //     Console.WriteLine(document["Name"]);
        // }
    }
}
