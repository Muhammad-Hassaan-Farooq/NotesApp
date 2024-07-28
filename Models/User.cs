using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NotesApp.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        [BsonElement("username")]
        public required string Username { get; set; }

        [BsonElement("email")]
        public required string Email { get; set; }

        [BsonElement("passwordHash")]
        public required string PasswordHash { get; set; }

        [BsonElement("createdAt")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [BsonElement("updatedAt")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [BsonElement("deletedAt")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime DeletedAt { get; set; }

        [BsonElement("isDeleted")]
        public bool IsDeleted { get; set; } = false;

        [BsonElement("isVerified")]
        public bool IsVerified { get; set; } = false;
    }
}
