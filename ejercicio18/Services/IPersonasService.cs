using ejercicio18.Models;

namespace ejercicio18.Services
{
    public interface IPersonasService
    {
        Persona create(Persona persona);
        Persona Read(long id);
        void Update(Persona persona);
    }
}