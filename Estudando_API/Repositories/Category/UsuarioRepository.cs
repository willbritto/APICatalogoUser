using Estudando_API.Contexts;
using Estudando_API.Models;
using Estudando_API.Repositories.Generic;
using Estudando_API.Repositories.Interface;

namespace Estudando_API.Repositories.Category
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(ApplicationDbContext context) : base(context) { }

        public IEnumerable<Usuario> GetUsuariosPorProduto(int id)
        {
            return GetAll().Where(u => u.UsuarioId == id);
        }
    }
}
