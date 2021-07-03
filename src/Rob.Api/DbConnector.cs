using System.Threading.Tasks;
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

        public BsonDocument GetOneDoc(string name, string value)
        {            
            return _collection.Find(new BsonDocument(name, value)).FirstOrDefault();
        }
    }
}
