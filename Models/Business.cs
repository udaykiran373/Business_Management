using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BusinessManagement.Models
{
    public class Business
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("businessId")]
        public string BusinessId { get; set; } = string.Empty;

        [BsonElement("businessName")]
        public string BusinessName { get; set; } = string.Empty;

        [BsonElement("creatorId")]
        public string CreatorId { get; set; } = string.Empty;

        [BsonElement("creatorName")]
        public string CreatorName { get; set; } = string.Empty;

        [BsonElement("createdDate")]
        public DateTime CreatedDate { get; set; }

        [BsonElement("updatedDate")]
        public DateTime UpdatedDate { get; set; }

        [BsonElement("isDeleted")]
        public bool IsDeleted { get; set; } = false;
    }
}
