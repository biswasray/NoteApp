using Devart.Data.MySql.Entity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NoteApp.Common;
using NoteApp.DataAccess;
using NoteApp.Entities;
using System.Drawing;
using System.Linq;

namespace NoteApp.Business
{
    public class NoteService
    {
        ApplicationDbContext _applicationDbContext;
        public NoteService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }


        public  Response<List<Note>> Query(string? sort, string? keyword, bool? desc,int page=1, int limit1=4)
        {
            IQueryable<Note> notes;
            if (keyword == null)
                notes = (from b in _applicationDbContext.Notes select b);
            //Match((new[] { x.Title, x.Body }, sort, MySqlMatchSearchMode.Boolean))
            else
                notes = _applicationDbContext.Notes.Where(x => EF.Functions.Match(new[] { x.Title, x.Body }, keyword, MySqlMatchSearchMode.Boolean));

            if (sort!=null)
            {
                if (sort.ToLower().Equals("title"))
                    notes = notes.OrderBy(x => x.Title);
                else if (sort.ToLower().Equals("body"))
                    notes = notes.OrderBy(x => x.Body);
                else if (sort.ToLower().Equals("updatedat"))
                    notes = notes.OrderBy(x => x.UpdatedAt);
                else if (sort.ToLower().Equals("createdat"))
                    notes = notes.OrderBy(x => x.CreatedAt);
            }
            if (desc == true)
            {
                notes=notes.Reverse();
            }

            int size = notes.Count();
            //int size = 15;
            int offset = (page - 1) * limit1;
            notes = notes.Skip(offset).Take(limit1);
            var temp = notes.ToList();
            var res = new Response<List<Note>>() {
                Status = true,
                Results = temp,
                Metadata = new MetaData()
                {
                    Page = page,
                    PerPage = limit1,
                    PageCount = (int)Math.Ceiling((double)size / limit1),
                    PageSize = temp.Count,
                    TotalCount = size
                }
            };
            return res;
        }
        public  Note GetById(string id) {
            return _applicationDbContext.Notes.Where(item => item.Id == id).FirstOrDefault();
        }
        public  Response<List<Note>> GetAll() {

            var notes = (from b in _applicationDbContext.Notes select b).ToList();
            int size = notes.Count;

            var res = new Response<List<Note>>()
            {
                Status = true,
                Results = notes,
                Metadata = new MetaData()
                {
                    Page = 1,
                    PerPage = size,
                    PageCount = (int)Math.Ceiling((double)size / size),
                    PageSize = size,
                    TotalCount = size
                }
            };
            return res;
        }
        public  Note Add(Note data) {
            Note note = new Note()
            {
                Id = Guid.NewGuid().ToString(),
                Title = data.Title,
                Body = data.Body,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            _applicationDbContext.Notes.Add(note);
            _applicationDbContext.SaveChanges();
            return note;
        }
        public  Note Update(string id,Note data)
        {
            var note = (from d in _applicationDbContext.Notes where d.Id == id select d).Single();
            if(data.Title!= null) note.Title = data.Title;
            if(data.Body!= null) note.Body = data.Body;
            note.UpdatedAt = DateTime.Now;
            _applicationDbContext.SaveChanges();
            return note;
        }
        public  Note Remove(string id)
        {
            var note = (from d in _applicationDbContext.Notes where d.Id == id select d).Single();
            _applicationDbContext.Notes.Remove(note);
            _applicationDbContext.SaveChanges();
            return note;
        }
    }
}
