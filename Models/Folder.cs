using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NotesApp.Models
{
    public class Folder
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string FolderId { get; set; }

        [BsonElement("name")]
        public required string Name { get; set; }

        [BsonElement("userId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string UserId { get; set; }

        [BsonElement("notes")]
        public List<string> Notes { get; set; } = new List<string>();

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
    }
}
