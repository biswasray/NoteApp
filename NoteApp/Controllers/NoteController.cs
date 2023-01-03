using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoteApp.Entities;

namespace NoteApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NoteController : Controller
    {
        [HttpGet]
        public List<Note> GetAll()
        {
            List<Note> notes = new List<Note>();
            foreach (var item in TestDb.NoteDb)
            {
                notes.Add(item.Value);
            }
            return notes;
        }
        [HttpGet("{id}")]
        public Note Get(string id)
        {
            return TestDb.NoteDb[id];
        }
        [HttpPost]
        public Note Create(Note data)
        {
            Note note = new Note()
            {
                Id = Guid.NewGuid().ToString(),
                Title = data.Title,
                Body = data.Body,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            TestDb.NoteDb.Add(note.Id, note);
            Console.WriteLine(TestDb.NoteDb.Count);
            return TestDb.NoteDb[note.Id];
        }
        [HttpPut("{id}")]
        public ActionResult Update(string id, Note data)
        {
            Note note = TestDb.NoteDb[id];
            if (data.Title != null)
                note.Title = data.Title;
            if (data.Body != null)
                note.Body = data.Body;
            note.UpdatedAt = DateTime.Now;
            TestDb.NoteDb[id] = note;
            return Ok(new { Status = true });
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            TestDb.NoteDb.Remove(id);
            return Ok(new { Status = true });
        }
    }
}
