using Estudando_API.Contexts;
using Estudando_API.Models;
using Estudando_API.Repositories.Generic;
using Estudando_API.Repositories.Interface;

namespace Estudando_API.Repositories.Category
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(ApplicationDbContext context): base(context) { }
    }
}
