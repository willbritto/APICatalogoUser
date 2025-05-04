using Estudando_API.Contexts;
using Estudando_API.Repositories.Category;
using Estudando_API.Repositories.Interface;

namespace Estudando_API.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private IProdutoRepository _produtoRepository;
        private ICategoriaRepository _categoriaRepository;
        private IUsuarioRepository _usuarioRepository;

        public ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IProdutoRepository ProdutoRepository
        {
            get
            {
                return _produtoRepository = _produtoRepository ?? new ProdutoRepository(_context);
            }
        }

        public ICategoriaRepository CategoriaRepository
        {
            get
            {
                return _categoriaRepository = _categoriaRepository ?? new CategoriaRepository(_context);
            }
        }

        public IUsuarioRepository UsuarioRepository
        {
            get
            {
                return _usuarioRepository = _usuarioRepository ?? new UsuarioRepository(_context);
            }
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
        public void Dispose() 
        {
            _context.Dispose();
        }
    }
}
