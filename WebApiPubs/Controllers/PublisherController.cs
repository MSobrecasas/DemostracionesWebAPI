﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApiPubs.Models;

namespace WebApiPubs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly pubsContext context;

        public PublisherController(pubsContext context)
        {
            this.context = context;
        }

        //GET: api/autor
        [HttpGet]
        public ActionResult<IEnumerable<Publisher>> Get()
        {
            return context.Publishers.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Publisher> GetById(string id)
        {
            Publisher publisher = (from p in context.Publishers
                               where p.PubId == id
                           select p).SingleOrDefault();
            return publisher;
        }

  

        //api/autor
        [HttpPost]
        public ActionResult Post(Publisher publisher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.Publishers.Add(publisher);
            context.SaveChanges();
            return Ok();
        }

        //update
        //PUT api/autor/{id}
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Publisher publisher)
        {
            if (id != publisher.PubId)
            {
                return BadRequest();
            }
            context.Entry(publisher).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        //DELETE api/autor/{id}
        [HttpDelete("{id}")]
        public ActionResult<Publisher> Delete(string id)
        {
            Publisher publisher = (from p in context.Publishers
                           where p.PubId == id
                           select p).SingleOrDefault();
            if (publisher == null)
            {
                return NotFound();
            }
            context.Publishers.Remove(publisher);
            context.SaveChanges();
            return publisher;
        }
    }
}
