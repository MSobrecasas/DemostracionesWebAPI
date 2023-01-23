using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApiLibros.Data;
using WebApiLibros.Models;

namespace WebApiLibros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private readonly DBLibrosBootcampContext context;
        public LibroController(DBLibrosBootcampContext context)
        {
            this.context = context;
        }

        //GET: api/especialidad
        [HttpGet]
        public ActionResult<IEnumerable<Libro>> Get()
        {
            return context.Libros.ToList();
        }

        //GET: api/especialidad/id
        [HttpGet("autor/{id}")]
        public ActionResult<IEnumerable<Libro>> GetAutor(int id)
        {
           List<Libro> libros = (from l in context.Libros
                           where l.AutorId == id
                           select l).ToList();
            return libros;
        }

        [HttpGet("{id}")]
        public ActionResult<Libro> GetById(int id)
        {
            Libro libro = (from l in context.Libros
                           where l.Id == id
                           select l).SingleOrDefault();
            return libro;
        }

        //  POST api/especialidad
        [HttpPost]
        public ActionResult Post(Libro libro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.Libros.Add(libro);
            context.SaveChanges();
            return Ok();
        }

        //update
        //PUT api/especialidad/{id}
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Libro libro)
        {
            if (id != libro.Id)
            {
                return BadRequest();
            }
            context.Entry(libro).State = EntityState.Modified;
            context.SaveChanges();
            return NoContent();
        }

        //DELETE api/especialidad/{id}
        [HttpDelete("{id}")]
        public ActionResult<Libro> Delete(int id)
        {
            Libro libro = (from l in context.Libros
                           where l.Id == id
                           select l).SingleOrDefault();
            if (libro == null)
            {
                return NotFound();
            }
            context.Libros.Remove(libro);
            context.SaveChanges();
            return libro;
        }
    }
}
