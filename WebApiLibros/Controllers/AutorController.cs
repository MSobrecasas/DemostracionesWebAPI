using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApiLibros.Data;
using WebApiLibros.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApiLibros.Controllers
{
    //api/Autor
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly DBLibrosBootcampContext context;

        public AutorController(DBLibrosBootcampContext context)
        {
            this.context = context;
        }

        //GET: api/autor
        [HttpGet]
        public ActionResult<IEnumerable<Autor>> Get()
        {
            return context.Autores.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Autor> GetById(int id)
        {
            Autor autor = (from a in context.Autores
                           where a.IdAutor== id
                           select a).SingleOrDefault();
            return autor;
        }

        [HttpGet("edad/{edad}")]
        public ActionResult<IEnumerable<Autor>> Get(int edad)
        {
            List<Autor> list = (from a in context.Autores
                                where a.Edad == edad
                                select a).ToList();
            return list;
        }

        //api/autor
        [HttpPost]
        public ActionResult Post(Autor autor) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.Autores.Add(autor);
            context.SaveChanges();
            return Ok();
        }

        //update
        //PUT api/autor/{id}
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]Autor autor)
        {
            if (id !=autor.IdAutor)
            {
                return BadRequest();
            }
            context.Entry(autor).State= EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        //DELETE api/autor/{id}
        [HttpDelete("{id}")]
        public ActionResult<Autor> Delete(int id)
        {
            Autor autor = (from a in context.Autores
                           where a.IdAutor == id
                           select a).SingleOrDefault();
            if (autor == null)
            {
                return NotFound();
            }
            context.Autores.Remove(autor);
            context.SaveChanges();
            return autor;
        }

    }
}
