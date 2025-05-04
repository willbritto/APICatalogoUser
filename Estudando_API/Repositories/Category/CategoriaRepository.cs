using Estudando_API.Contexts;
using Estudando_API.Models;
using Estudando_API.Repositories.Generic;
using Estudando_API.Repositories.Interface;

namespace Estudando_API.Repositories.Category
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(ApplicationDbContext context): base (context) { }
    }
}
