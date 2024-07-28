using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NotesApp.DTO.Note
{
    public class CreateNoteDTO
    {
        [Required]
        public string FolderID { get; set; }

        [Required]
        public string Title { get; set; }

        public string Content { get; set; } = "";
    }
}
