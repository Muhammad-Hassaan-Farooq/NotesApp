using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using NotesApp.Data;
using NotesApp.DTO.Note;
using NotesApp.Interfaces.Note;
using NotesApp.Models;

namespace NotesApp.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly MongoDBContext _context;
        private readonly IMongoCollection<Note> _notes;
        private readonly IMongoCollection<Folder> _folders;

        public NoteRepository(MongoDBContext context)
        {
            _context = context;
            _notes = _context.Database.GetCollection<Note>("Notes");
            _folders = _context.Database.GetCollection<Folder>("Folders");
        }

        public async Task<List<Folder>> GetUserFoldersAsync(string userID)
        {
            var folders = await _folders.Find(f => f.UserId == userID).ToListAsync();
            return folders;
        }

        public async Task<List<Note>> GetUserNotesAsync(string userID)
        {
            var notes = await _notes.Find(n => n.UserId == userID).ToListAsync();
            return notes;
        }

        public async Task CreateNoteAsync(string userID, CreateNoteDTO createNoteDTO)
        {
            Note note = new Note
            {
                UserId = userID,
                Title = createNoteDTO.Title,
                Content = createNoteDTO.Content,
                FolderId = createNoteDTO.FolderID,
            };

            await _notes.InsertOneAsync(note);

            await _folders.UpdateOneAsync(
                f => f.FolderId == createNoteDTO.FolderID,
                Builders<Folder>.Update.Push(f => f.Notes, note.NoteId)
            );
        }

        public async Task CreateFolderAsync(string userID, string folderName)
        {
            Folder folder = new Folder { UserId = userID, Name = folderName, };

            await _folders.InsertOneAsync(folder);
        }

        public async Task<bool> DoesFolderExist(string folderID)
        {
            var folder = await _folders.Find(f => f.FolderId == folderID).FirstOrDefaultAsync();
            return folder != null;
        }

        public async Task<bool> DoesNoteExist(string noteID)
        {
            var note = await _notes.Find(n => n.NoteId == noteID).FirstOrDefaultAsync();
            return note != null;
        }

        public async Task<List<Note>> GetFolderNotesAsync(string folderID)
        {
            var folder = await _folders.Find(f => f.FolderId == folderID).FirstOrDefaultAsync();
            var notes = await _notes.Find(n => folder.Notes.Contains(n.NoteId)).ToListAsync();
            return notes;
        }

        public async Task UpdateNoteAsync(UpdateNoteDTO updateNoteDTO)
        {
            var filter = Builders<Note>.Filter.Eq(n => n.NoteId, updateNoteDTO.NoteID);
            var update = Builders<Note>
                .Update.Set(n => n.Title, updateNoteDTO.Title)
                .Set(n => n.Content, updateNoteDTO.Content)
                .Set(n => n.UpdatedAt, DateTime.Now);

            await _notes.UpdateOneAsync(filter, update);
        }

        public async Task<bool> DoesUserOwnFolder(string userID, string folderID)
        {
            var folder = await _folders.Find(f => f.FolderId == folderID).FirstOrDefaultAsync();
            return folder.UserId == userID;
        }

        public async Task<bool> DoesUserOwnNote(string userID, string noteID)
        {
            var note = await _notes.Find(n => n.NoteId == noteID).FirstOrDefaultAsync();
            return note.UserId == userID;
        }
    }
}
