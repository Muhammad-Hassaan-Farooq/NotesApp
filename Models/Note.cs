using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NotesApp.Models
{
    public class Note
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string NoteId { get; set; }

        [BsonElement("title")]
        public required string Title { get; set; }

        [BsonElement("content")]
        public required string Content { get; set; }

        [BsonElement("createdAt")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [BsonElement("updatedAt")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [BsonElement("folderId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string FolderId { get; set; }

        [BsonElement("userId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        [BsonElement("deletedAt")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime DeletedAt { get; set; }

        [BsonElement("isDeleted")]
        public bool IsDeleted { get; set; } = false;
    }
}
