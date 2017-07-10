using System.Threading.Tasks;
using ExnessTask.DtoModels;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace ExnessTask.DataRepository
{
    public class VendorRepository:Repository
    {
        protected IMongoCollection<BsonDocument> _vendorCollection;
        public VendorRepository(string connectionString) : base(connectionString)
        {
            _vendorCollection = _db.GetCollection<BsonDocument>("vendors");
            //Делаю поле VendoId уникальным
            _vendorCollection.Indexes.CreateOneAsync("{ VendorId: 1 }");
        }

        public async Task AddVendor(VendorDto vendor)
        {
            await _vendorCollection.InsertOneAsync(vendor.ToBsonDocument());
        }

        public async Task<VendorDto> GetVendorById(string id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("VendorId", id);
            var result = await _vendorCollection.Find(filter).ToListAsync();
            if (result.Count == 0)
            {
                return null;
            }

            return BsonSerializer.Deserialize<VendorDto>(result[0]);//result[0] так как VendorId уникальный для коллекции
        }

        public async Task CleanUp()
        {
           await _vendorCollection.DeleteManyAsync(FilterDefinition<BsonDocument>.Empty);
        }
    }
}