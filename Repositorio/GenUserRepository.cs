using next_generation.Data;
using next_generation.Models;
using next_generation.Repositorio.IRepositorio;

namespace next_generation.Repositorio
{
    public class GenUserRepository : Repositorio<NexGenUsers>, IGenUserRepository
    {
        private readonly ApplicationDbContext _db;

        public GenUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<NexGenUsers> Actualizar(NexGenUsers entidad)
        {
            entidad.CreationDate = DateTime.Now;
            _db.NextGenUser.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}
}
