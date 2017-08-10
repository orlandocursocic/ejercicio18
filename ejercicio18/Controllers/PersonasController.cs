using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ejercicio18.Models;
using ejercicio18.Services;

namespace ejercicio18.Controllers
{
    public class PersonasController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public IPersonasService personasService;

        public PersonasController(IPersonasService personasService)
        {
            this.personasService = personasService;
        }

        // GET: api/Personas
        public IQueryable<Persona> GetPersonas()
        {
            return db.Personas;
        }

        // GET: api/Personas/5
        [ResponseType(typeof(Persona))]
        public IHttpActionResult GetPersona(long id)
        {
            Persona persona = personasService.Read(id);
            if (persona == null)
            {
                return NotFound();
            }

            return Ok(persona);
        }

        // PUT: api/Personas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPersona(long id, Persona persona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != persona.Id)
            {
                return BadRequest();
            }

            try
            {
                personasService.Update(persona);
            }
            catch (NoEncontradoException)
            {
                return NotFound();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Personas
        [ResponseType(typeof(Persona))]
        public IHttpActionResult PostPersona(Persona persona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            persona = personasService.create(persona);

            return CreatedAtRoute("DefaultApi", new { id = persona.Id }, persona);
        }

        // DELETE: api/Personas/5
        [ResponseType(typeof(Persona))]
        public IHttpActionResult DeletePersona(long id)
        {
            Persona persona = db.Personas.Find(id);
            if (persona == null)
            {
                return NotFound();
            }

            db.Personas.Remove(persona);
            db.SaveChanges();

            return Ok(persona);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonaExists(long id)
        {
            return db.Personas.Count(e => e.Id == id) > 0;
        }
    }
}