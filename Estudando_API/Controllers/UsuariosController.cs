using Estudando_API.Contexts;
using Estudando_API.Models;
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
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        public UsuariosController(ApplicationDbContext context, ILogger<UsuariosController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("Produtos")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUserProductAsync()
        {
            _logger.LogInformation("================== Relacionamento entre a tabela Usuarios e Produtos ===================");
            _logger.LogInformation(" ================= GET/Usuarios/Produtos =====================");
            try
            {
                return await _context.Usuarios.Include(p => p.Produtos).Where(c => c.UsuarioId >= 1).ToListAsync();

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar a solicitação");
            }

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> AllUserGetAsync()
        {
            _logger.LogInformation(" ================= GET/Usuarios =====================");
            try
            {
                var usuarios = await _context.Usuarios.Take(10).ToListAsync();
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
        public async Task<ActionResult<Usuario>> UserIdGetAsync(int id)
        {
            _logger.LogInformation(" ================= GET/Usuarios/{id} =====================");
            try
            {
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.UsuarioId == id);
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
        public async Task<ActionResult> CreateUserPostAsync(Usuario usuario)
        {
            _logger.LogInformation(" ================= POST/Usuarios =====================");
            try
            {
                if (usuario is null)
                {
                    return BadRequest("Dados inválidos/ erro ao cadastrar novo usuário ... ");
                }
                _context.Usuarios.Add(usuario);
               await _context.SaveChangesAsync();

                return new CreatedAtRouteResult("ObterUsuario", new { id = usuario.UsuarioId }, usuario);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tentar a sua solicitação");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateUserPutAsync(int id, Usuario usuario)
        {
            _logger.LogInformation(" ================= PUT/Usuarios/{id} =====================");
            try
            {
                if (id != usuario.UsuarioId)
                    return BadRequest("Dados inválidos");
                _context.Entry(usuario).State = EntityState.Modified;
               await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tentar a sua solicitação");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteUserAsync(int id)
        {
            _logger.LogInformation(" ================= DELETE/Usuarios/{id} =====================");
            try
            {
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(p => p.UsuarioId == id);
                if (usuario is null)
                {
                    return NotFound($"Usuário com o ID = {id} não cadastrado/existente ..");
                }
                _context.Remove(usuario);
              await  _context.SaveChangesAsync();

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
