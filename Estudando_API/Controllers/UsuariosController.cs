using Estudando_API.Contexts;
using Estudando_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Estudando_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        public UsuariosController(ApplicationDbContext context, ILogger<UsuariosController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("Produtos")]
        public ActionResult<IEnumerable<Usuario>> GetUserProduct()
        {
            _logger.LogInformation("Relacionamento entre a tabela Usuarios e Produtos");
            _logger.LogInformation(" ================= GET/Usuarios/Produtos =====================");
            try
            {
                return _context.Usuarios.Include(p => p.Produtos).Where(c => c.UsuarioId >= 1).ToList();

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar a solicitação");
            }

        }

        [HttpGet]
        public ActionResult<IEnumerable<Usuario>> AllUserGet()
        {
            _logger.LogInformation(" ================= GET/Usuarios =====================");
            try
            {
                var usuarios = _context.Usuarios.Take(10).ToList();
                if (usuarios is null)
                {
                    return NotFound($"Não há usuários cadastrados no banco de dados ...  ");
                }
                return usuarios;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tentar a sua solicitação");
            }
        }

        [HttpGet("{id:int}", Name = "ObterUsuario")]
        public ActionResult<Usuario> UserIdGet(int id)
        {
            _logger.LogInformation(" ================= GET/Usuarios/{id} =====================");
            try
            {
                var usuario = _context.Usuarios.FirstOrDefault(u => u.UsuarioId == id);
                if (usuario is null)
                {
                    return NotFound($"Usuario com id = {id} não cadastrado ...");
                }
                return usuario;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tentar a sua solicitação");

            }
        }

        [HttpPost]
        public ActionResult CreateUserPost(Usuario usuario)
        {
            _logger.LogInformation(" ================= POST/Usuarios =====================");
            try
            {
                if (usuario is null)
                {
                    return BadRequest("Dados inválidos/ erro ao cadastrar novo usuário ... ");
                }
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();

                return new CreatedAtRouteResult("ObterUsuario", new { id = usuario.UsuarioId }, usuario);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tentar a sua solicitação");
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult UpdateUserPut(int id, Usuario usuario)
        {
            _logger.LogInformation(" ================= PUT/Usuarios/{id} =====================");
            try
            {
                if (id != usuario.UsuarioId)
                    return BadRequest("Dados inválidos");
                _context.Entry(usuario).State = EntityState.Modified;
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tentar a sua solicitação");
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult DeleteUser(int id)
        {
            _logger.LogInformation(" ================= DELETE/Usuarios/{id} =====================");
            try
            {
                var usuario = _context.Usuarios.FirstOrDefault(p => p.UsuarioId == id);
                if (usuario is null)
                {
                    return NotFound($"Usuário com o ID = {id} não cadastrado/existente ..");
                }
                _context.Remove(usuario);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tentar a solicitação");
            }
        }
    }
}
