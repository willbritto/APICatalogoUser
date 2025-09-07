using Estudando_API.Contexts;
using Estudando_API.Models;
using Estudando_API.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Estudando_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly ILogger _logger;

        public ProdutosController(ILogger<ProdutosController> logger, IUnitOfWork uof)
        {
            _uof = uof;
            _logger = logger;
        }

        [HttpGet("Categorias/{id}")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUserProduct(int id)
        {
            _logger.LogInformation("\n================== Relacionamento entre a tabela Produtos e Categorias ==================\n");
            _logger.LogInformation("\n ================= GET/Produtos/Categorias/{id} =====================\n");
            try
            {

                var produtos = await _uof.ProdutoRepository.GetProdutosPorCategoriaAysnc(id);

                if (produtos is null)
                {
                    _logger.LogWarning($"Produto com ID = [{id}] não cadastrado/existe");
                    return NotFound($"Produto com ID = [{id}] não cadastrado/existe");
                }

                return Ok(produtos);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar a solicitação");
            }

        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Produto>>> AllProductGet()
        {
            try
            {
                var produtos = await _uof.ProdutoRepository.GetAllAsync();

                if (produtos is null)
                {
                    return NotFound("Não existe produtos cadastrados no banco...");
                }

                return Ok(produtos);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar a solicitação");
            }
        }

        [HttpGet("{id:int}", Name = "ObterProduto")]
        public async Task<ActionResult<Produto>> ProductIdGet(int id)
        {
            try
            {
                var produto = await _uof.ProdutoRepository.GetAsync(p => p.ProdutoId == id);

                if (produto is null)
                {
                    return NotFound($"Produto com ID = [{id}] não existe/cadastrado ...");
                }

                return Ok(produto);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar a solicitação ");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateProductPost(Produto produto)
        {
            try
            {
                if (produto is null)
                    return NotFound($"Erro ao tenta cadastrar o {produto.Nome} ...");

                _uof.ProdutoRepository.Create(produto);
               await _uof.CommitAsync();

                return new CreatedAtRouteResult("ObterProduto",
                    new { id = produto.ProdutoId }, produto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar a solicitação ");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateUserPut(int id, Produto produto)
        {
            try
            {
                if (id != produto.ProdutoId)
                {
                    return BadRequest("Dados inválidos");
                }
                _uof.ProdutoRepository.Update(produto);
               await _uof.CommitAsync();

                return Ok(produto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar a solicitação ");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            try
            {
                var produto = await _uof.ProdutoRepository.GetAsync(p => p.ProdutoId == id);
                if (produto is null)
                {
                    return NotFound($"Produto com ID = [{id}] não existe/cadastrado ...");
                }
                _uof.ProdutoRepository.Delete(produto);
                await _uof.CommitAsync();

                return Ok(produto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Erro ao tentar a solicitação ...");
            }
        }

    }
}
