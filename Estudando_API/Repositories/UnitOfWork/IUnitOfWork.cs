using Estudando_API.Repositories.Interface;

namespace Estudando_API.Repositories.UnitOfWork
{
    public interface IUnitOfWork
    {
        IProdutoRepository ProdutoRepository { get; }
        ICategoriaRepository CategoriaRepository { get; }
        IUsuarioRepository UsuarioRepository { get; }

        void Commit();
    }
}
