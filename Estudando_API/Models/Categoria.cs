using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Estudando_API.Models;
[Table("Categorias")]
public class Categoria
{
    [Key]
    public int CategoriaId { get; set; }
    [Required]
    [StringLength(100)]
    public string? Nome { get; set; }   
   
    
}
