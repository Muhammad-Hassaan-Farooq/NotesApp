using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesApp.DTO.Note
{
    public class FolderResponseDTO
    {
        public string FolderId { get; set; }
        public required string Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public List<NoteResponseDTO> Notes { get; set; } = new List<NoteResponseDTO>();
    }
}
