using Estudando_API.Models;
using Estudando_API.Repositories.Generic;

namespace Estudando_API.Repositories.Interface
{
    public interface IProdutoRepository : IRepository<Produto> 
    {
       Task<IEnumerable<Produto>> GetProdutosPorCategoriaAysnc(int id);
    }
}
