using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NotesApp.DTO.Note;
using NotesApp.Models;

namespace NotesApp.Mappers.Notes
{
    public static class NotesMapper
    {
        public static NoteResponseDTO NoteToNoteRepsonseDTO(Note note)
        {
            return new NoteResponseDTO
            {
                NoteID = note.NoteId,
                FolderID = note.FolderId,
                Title = note.Title,
                Content = note.Content,
                createdAt = note.CreatedAt,
                updatedAt = note.UpdatedAt
            };
        }
    }
}
