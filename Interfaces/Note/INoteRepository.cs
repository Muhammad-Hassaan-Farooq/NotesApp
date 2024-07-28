using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NotesApp.DTO.Note;
using NotesApp.Models;

namespace NotesApp.Interfaces.Note
{
    public interface INoteRepository
    {
        Task<List<Folder>> GetUserFoldersAsync(string userID);
        Task<List<NotesApp.Models.Note>> GetUserNotesAsync(string userID);
        Task CreateNoteAsync(string userID, CreateNoteDTO createNoteDTO);
        Task CreateFolderAsync(string userID, string folderName);
        Task<bool> DoesFolderExist(string folderID);
        Task<List<NotesApp.Models.Note>> GetFolderNotesAsync(string folderID);
        Task<bool> DoesNoteExist(string noteID);
        Task<bool> DoesUserOwnFolder(string userID, string folderID);
        Task<bool> DoesUserOwnNote(string userID, string noteID);
        Task UpdateNoteAsync(UpdateNoteDTO updateNoteDTO);
    }
}
