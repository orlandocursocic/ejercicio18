using ejercicio18.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ejercicio18.Services
{
    public class PersonasService : IPersonasService
    {
        private IPersonasRepository personasRepository;

        public PersonasService(IPersonasRepository personasRepository)
        {
            this.personasRepository = personasRepository;
        }

        public Persona create(Persona persona)
        {
            return  personasRepository.Create(persona);
        }

        public Persona Read(long id)
        {
            return personasRepository.Read(id);
        }

        public void Update(Persona persona)
        {
            personasRepository.Update(persona);
        }
    }
}