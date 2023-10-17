using next_generation.Models;

namespace next_generation.Repositorio.IRepositorio
{
    public interface IGenUserRepository : IRepositorio<NexGenUsers>
    {
        Task<NexGenUsers> Actualizar(NexGenUsers entidad);
    }
}
