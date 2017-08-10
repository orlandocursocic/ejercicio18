using ejercicio18.Models;
using ejercicio18.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ejercicio18
{
    public class PersonasRepository : IPersonasRepository
    {
        public Persona Create(Persona persona)
        {
            Persona personaAux = ApplicationDbContext.applicationDbContext.Personas.Add(persona);
            return personaAux;
        }

        public Persona Read(long id)
        {
            return ApplicationDbContext.applicationDbContext.Personas.Find(id);
        }

        public void Update(Persona persona)
        {
            // Si no existe la entrada en la bd
            if (!(ApplicationDbContext.applicationDbContext.Personas.Count(e => e.Id == persona.Id) > 0))
            {
                throw new NoEncontradoException("No he encontrado la entidad");
            }
            ApplicationDbContext.applicationDbContext.Entry(persona).State = EntityState.Modified;
        }
    }
}