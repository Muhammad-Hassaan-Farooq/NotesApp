using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using NotesApp.DTO.Note;
using NotesApp.Interfaces.Note;
using NotesApp.Mappers.Notes;

namespace NotesApp.Controllers
{
    [ApiController]
    [Route("api/notes")]
    [Authorize]
    public class NoteController : ControllerBase
    {
        private readonly INoteRepository _noteRepository;

        public NoteController(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetNotes()
        {
            try
            {
                var userID = User.Claims.First(c => c.Type == "UserID").Value;
                var notes = await _noteRepository.GetUserNotesAsync(userID);
                var returnobject = notes.Select(n => NotesMapper.NoteToNoteRepsonseDTO(n)).ToList();
                return Ok(returnobject);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("folders")]
        public async Task<IActionResult> GetNotesByFolders()
        {
            try
            {
                var userID = User.Claims.First(c => c.Type == "UserID").Value;
                var folders = await _noteRepository.GetUserFoldersAsync(userID);
                var returnobject = new AllNotesResponseDTO
                {
                    folders = folders
                        .Select(f => new FolderResponseDTO
                        {
                            FolderId = f.FolderId,
                            Name = f.Name,
                        })
                        .ToList()
                };
                foreach (var folder in returnobject.folders)
                {
                    var notes = await _noteRepository.GetFolderNotesAsync(folder.FolderId);
                    folder.Notes = notes.Select(n => NotesMapper.NoteToNoteRepsonseDTO(n)).ToList();
                }
                return Ok(returnobject);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("create-note")]
        public async Task<IActionResult> CreateNote([FromBody] CreateNoteDTO createNoteDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (!ObjectId.TryParse(createNoteDTO.FolderID, out _))
                {
                    return BadRequest("Invalid Folder ID");
                }
                bool folderExists = await _noteRepository.DoesFolderExist(createNoteDTO.FolderID);

                if (!folderExists)
                {
                    return BadRequest("Folder doesnot exists");
                }
                var userID = User.Claims.First(c => c.Type == "UserID").Value;
                await _noteRepository.CreateNoteAsync(userID, createNoteDTO);
                return Ok("Note created");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500);
            }
        }

        [HttpPost("create-folder")]
        public async Task<IActionResult> CreateFolder(
            [FromBody] CreateFolderRequestDTO createFolderRequestDTO
        )
        {
            if (createFolderRequestDTO.folderName.Length == 0)
            {
                return BadRequest("Folder Name is required");
            }
            try
            {
                var userId = User.Claims.First(c => c.Type == "UserID").Value;
                await _noteRepository.CreateFolderAsync(userId, createFolderRequestDTO.folderName);
                return Ok("Folder created");
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpPut("update-note")]
        public async Task<IActionResult> UpdateNote(UpdateNoteDTO updateNoteDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var userId = User.Claims.First(c => c.Type == "UserID").Value;

                if (!await _noteRepository.DoesNoteExist(updateNoteDTO.NoteID))
                {
                    return BadRequest("Note does not exist");
                }
                if (!await _noteRepository.DoesUserOwnNote(userId, updateNoteDTO.NoteID))
                {
                    return Unauthorized("User does not own the note");
                }
                await _noteRepository.UpdateNoteAsync(updateNoteDTO);
                return Ok("Note updated");
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
    }
}
