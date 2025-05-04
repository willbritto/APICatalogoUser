using Estudando_API.Contexts;
using Estudando_API.Models;
using Estudando_API.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Estudando_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly ILogger _logger;
        public UsuariosController(IUnitOfWork uof, ILogger<UsuariosController> logger)
        {
           
            _uof = uof;
            _logger = logger;
        }

        [HttpGet("Produtos")]
        public ActionResult<IEnumerable<Usuario>> GetUserProductAsync(int id)
        {
            _logger.LogInformation("\n================== Relacionamento entre a tabela Usuarios e Produtos ==================\n");
            _logger.LogInformation("\n ================= GET/Usuarios/Produtos =====================\n");
            try
            {
                var usuarios = _uof.UsuarioRepository.GetUsuariosPorProduto(id); 
                return Ok(usuarios);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar a solicitação");
            }

        }

        [HttpGet]
        public ActionResult<IEnumerable<Usuario>> AllUserGetAsync()
        {
            _logger.LogInformation(" ================= GET/Usuarios =====================");
            try
            {
                var usuarios =  _uof.UsuarioRepository.GetAll();
                if (usuarios is null)
                {
                    _logger.LogWarning($"Não há usuários cadastrados no banco de dados ...  ");
                    return NotFound($"Não há usuários cadastrados no banco de dados ...  ");
                }
                return Ok(usuarios);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tentar a sua solicitação");
            }
        }

        [HttpGet("{id:int}", Name = "ObterUsuario")]
        public ActionResult<Usuario> UserIdGetAsync(int id)
        {
            _logger.LogInformation(" ================= GET/Usuarios/{id} =====================");
            try
            {
                var usuario = _uof.UsuarioRepository.Get(u => u.UsuarioId == id);
                if (usuario is null)
                {
                    _logger.LogWarning($"Usuario com id = {id} não cadastrado ...");
                    return NotFound($"Usuario com id = {id} não cadastrado ...");
                }
                return Ok(usuario);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tentar a sua solicitação");

            }
        }

        [HttpPost]
        public ActionResult CreateUserPostAsync(Usuario usuario)
        {
            _logger.LogInformation(" ================= POST/Usuarios =====================");
            try
            {
                if (usuario is null)
                {
                    _logger.LogWarning("Dados inválidos/ erro ao cadastrar novo usuário ... ");
                    return BadRequest("Dados inválidos/ erro ao cadastrar novo usuário ... ");
                }
                _uof.UsuarioRepository.Create(usuario);
                _uof.Commit();

                return new CreatedAtRouteResult("ObterUsuario", new { id = usuario.UsuarioId }, usuario);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tentar a sua solicitação");
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult UpdateUserPutAsync(int id, Usuario usuario)
        {
            _logger.LogInformation(" ================= PUT/Usuarios/{id} =====================");
            try
            {
                if (id != usuario.UsuarioId)
                {
                    _logger.LogWarning("Dados inválidos");
                    return BadRequest("Dados inválidos");
                }

                _uof.UsuarioRepository.Update(usuario);
                _uof.Commit();

                return Ok(usuario);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tentar a sua solicitação");
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult DeleteUserAsync(int id)
        {
            _logger.LogInformation(" ================= DELETE/Usuarios/{id} =====================");
            try
            {
                var usuario = _uof.UsuarioRepository.Get(p => p.UsuarioId == id);
                if (usuario is null)
                {
                    _logger.LogWarning($"Usuário com o ID = {id} não cadastrado/existente ..");
                    return NotFound($"Usuário com o ID = {id} não cadastrado/existente ..");
                }
                _uof.UsuarioRepository.Delete(usuario);
                _uof.Commit();

                return Ok(usuario);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tentar a solicitação");
            }
        }
    }
}
