using MongoDB.Driver;

namespace ExnessTask.DataRepository
{
    public class Repository
    {
        protected IMongoDatabase _db;
        public Repository(string connectionString)
        {
            MongoClient client = new MongoClient(connectionString);
            _db = client.GetDatabase("exness");
        }
    }
}