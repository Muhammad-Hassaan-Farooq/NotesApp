using MongoDB.Driver;

namespace NotesApp.Data
{
    public class MongoDBContext
    {
        public IMongoDatabase Database { get; }

        public MongoDBContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["MONGO_URI"]);
            Database = client.GetDatabase("NotesApp");
        }
    }
}
