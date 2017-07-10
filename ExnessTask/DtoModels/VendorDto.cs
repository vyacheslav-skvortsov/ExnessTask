using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace ExnessTask.DtoModels
{
    [BsonIgnoreExtraElements]
    public class VendorDto
    {
        public string VendorId { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }
        public List<CategoryDto> Categories { get; set; }
    }
}