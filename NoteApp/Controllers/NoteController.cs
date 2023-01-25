using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NoteApp.Business;
using NoteApp.Common;
using NoteApp.Entities;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using System.Collections.Generic;
using NoteApp.DataAccess;

namespace NoteApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NoteController : Controller
    {
        ApplicationDbContext applicationDbContext;
        NoteService service;
        public NoteController(ApplicationDbContext applicationDbContext) {
            this.applicationDbContext = applicationDbContext;
            service=new NoteService(this.applicationDbContext);
        }
        [HttpGet("all")]
        public ActionResult GetAll()
        {
            var result = service.GetAll();
            return Ok(result);
        }
        [HttpGet]
        public ActionResult Get(string? sort, string? keyword, bool? desc,int page = 1, int limit = 4)
        {
            //if (sort == null&& keyword == null&& desc == null && page == null && limit == null)
            //{
            //    List<Note> notes0 = service.GetAll().ToList();
            //    return Ok(notes0);
            //}
            var result = service.Query(sort,keyword,desc, page,limit);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public ActionResult Get(string id)
        {
            var note = service.GetById(id);
            return Ok(new Response<Note>()
            {
                Status = true,
                Results = note
            });
        }
        [HttpPost]
        public ActionResult Create(Note data)
        {
            var note = service.Add(data);
            return Ok(new Response<Note>()
            {
                Status=true,
                Results= note
            });
        }
        [HttpPut("{id}")]
        public ActionResult Update(string id, Note data)
        {
            service.Update(id, data);
            return Ok(new Response<string>() { Status = true });
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            service.Remove(id);
            return Ok(new Response<string>(){ Status = true });
        }
    }
}
