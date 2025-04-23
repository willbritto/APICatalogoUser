using Estudando_API.Contexts;
using Estudando_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Estudando_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProdutosController(ApplicationDbContext context)
        {
            _context = context;
        }

       
        [HttpGet]
        public ActionResult<IEnumerable<Produto>> AllProductGet()
        {
            try
            {
                var produtos = _context.Produtos.Take(10).ToList();

                if (produtos is null)
                {
                    return NotFound("Não existe produtos cadastrados no banco...");
                }

                return produtos;

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
                var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);

                if (produto is null)
                {
                    return NotFound($"Produto com ID = [{id}] não existe/cadastrado ...");
                }

                return produto;

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

                _context.Produtos.Add(produto);
                _context.SaveChanges();

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
                _context.Entry(produto).State = EntityState.Modified;
                _context.SaveChanges();

                return Ok();
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
                var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
                if (produto is null)
                {
                    return NotFound($"Produto com ID = [{id}] não existe/cadastrado ...");
                }
                _context.Produtos.Remove(produto);
                _context.SaveChanges();

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
