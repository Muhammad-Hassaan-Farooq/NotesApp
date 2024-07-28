using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesApp.DTO.Note
{
    public class AllNotesResponseDTO
    {
        public List<FolderResponseDTO> folders { get; set; }
    }
}
