using Estudando_API.Contexts;
using Estudando_API.Models;
using Estudando_API.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

       
        [HttpGet]
        public ActionResult<IEnumerable<Produto>> AllProductGet()
        {
            try
            {
                var produtos = _uof.ProdutoRepository.GetAll();

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
        public ActionResult<Produto> ProductIdGet(int id)
        {
            try
            {
                var produto = _uof.ProdutoRepository.Get(p => p.ProdutoId == id);

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
        public ActionResult CreateProductPost(Produto produto)
        {
            try
            {
                if (produto is null)
                    return NotFound($"Erro ao tenta cadastrar o {produto.Nome} ...");

                _uof.ProdutoRepository.Create(produto);
                _uof.Commit();

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
        public ActionResult UpdateUserPut(int id, Produto produto)
        {
            try
            {
                if (id != produto.ProdutoId)
                {
                    return BadRequest("Dados inválidos");
                }
                _uof.ProdutoRepository.Update(produto);
                _uof.Commit();

                return Ok(produto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar a solicitação ");
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult DeleteProduct(int id)
        {
            try
            {
                var produto = _uof.ProdutoRepository.Get(p => p.ProdutoId == id);
                if (produto is null)
                {
                    return NotFound($"Produto com ID = [{id}] não existe/cadastrado ...");
                }
                _uof.ProdutoRepository.Delete(produto);
                _uof.Commit();

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
