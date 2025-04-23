using Estudando_API.Contexts;
using Estudando_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Storage.Internal;

namespace Estudando_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoriasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> AllCategoryGet()
        {
            try
            {
                var categorias = _context.Categorias.Take(10).ToList();

                if (categorias is null)
                {
                    return NotFound("Categorias não existe/cadastrada ...");
                }
                return categorias;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar a solicitação ...");
            }
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Categoria> CategoryIdGet(int id) 
        {
            try
            {
                var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);

                if (categoria is null)
                {
                    return NotFound($"Produto com ID = [{id}] não existente/ou cadastrado ...");
                }
                return categoria;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar a solicitação ...");
            }
        }

        [HttpPost]
        public ActionResult CreateCategoryPost(Categoria categoria) 
        {
            try
            {
                if (categoria is null)
                {
                    return NotFound($"Erro ao tentar cadastrar a {categoria} ... ");
                }

                _context.Categorias.Add(categoria);
                _context.SaveChanges();

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
        public ActionResult UpdateCategoryPut(int id, Categoria categoria) 
        {
            try
            {
                if (id != categoria.CategoriaId)
                    return BadRequest("Dados inválidos");
                _context.Entry(categoria).State = EntityState.Modified;
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao tentar a solicitação ...");
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult DeleteCategory(int id)         
        {
            try
            {
                var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);
                if (categoria is null)
                    return NotFound($"Produto com id = {id} não localizado ...");

                _context.Categorias.Remove(categoria);
                _context.SaveChanges();

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
