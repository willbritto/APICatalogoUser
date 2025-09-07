using Estudando_API.Contexts;
using Estudando_API.Models;
using Estudando_API.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Storage.Internal;
using System.Threading.Tasks;

namespace Estudando_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly ILogger _logger;

        public CategoriasController(IUnitOfWork uof, ILogger<CategoriasController> logger) 
        {
            _uof = uof;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> AllCategoryGet()
        {
            try
            {
                var categorias = await _uof.CategoriaRepository.GetAllAsync();

                if (categorias is null)
                {
                    return NotFound("Categorias não existe/cadastrada ...");
                }
                return Ok(categorias);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar a solicitação ...");
            }
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public async Task<ActionResult<Categoria>> CategoryIdGet(int id) 
        {
            try
            {
                var categoria = await _uof.CategoriaRepository.GetAsync(c=>c.CategoriaId == id);

                if (categoria is null)
                {
                    return NotFound($"Produto com ID = [{id}] não existente/ou cadastrado ...");
                }
                return Ok(categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar a solicitação ...");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateCategoryPost(Categoria categoria) 
        {
            try
            {
                if (categoria is null)
                {
                    return NotFound($"Erro ao tentar cadastrar a {categoria} ... ");
                }

                _uof.CategoriaRepository.Create(categoria);
                await _uof.CommitAsync();

                return new CreatedAtRouteResult("ObterCategoria",
                   new { id = categoria.CategoriaId }, categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar a solicitação ");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateCategoryPut(int id, Categoria categoria) 
        {
            try
            {
                if (id != categoria.CategoriaId)
                    return BadRequest("Dados inválidos");


                _uof.CategoriaRepository.Update(categoria);
               await _uof.CommitAsync();

                return Ok(categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar a solicitação ...");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteCategory(int id)         
        {
            try
            {
                var categoria = await _uof.CategoriaRepository.GetAsync(p => p.CategoriaId == id);
                if (categoria is null)
                    return NotFound($"Produto com id = {id} não localizado ...");

                _uof.CategoriaRepository.Delete(categoria);
               await _uof.CommitAsync();

                return Ok(categoria);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar a solicitacão ...");
            }
        }
    }
}
