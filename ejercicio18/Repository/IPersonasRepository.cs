using ejercicio18.Models;

namespace ejercicio18
{
    public interface IPersonasRepository
    {
        Persona Create(Persona persona);
        Persona Read(long id);
        void Update(Persona persona);
    }
}