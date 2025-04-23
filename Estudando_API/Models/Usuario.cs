using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Estudando_API.Models;

[Table("Usuarios")]
public class Usuario
{
    [Key]
    public int UsuarioId { get; set; }
    [Required]
    [StringLength(100)]
    public string?  nomeCompleto { get; set; }
    [Required]
    [StringLength(100)]
    public string?  Email { get; set; }
    [Required]
    [StringLength(32)]
    public string? Senha { get; set; }    
       
    public ICollection<Produto>? Produtos { get; set; }     
}
