using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesApp.DTO.Note
{
    public class NoteResponseDTO
    {
        public required string NoteID { get; set; }
        public required string FolderID { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public DateTime createdAt { set; get; }
        public DateTime updatedAt { get; set; }
    }
}
