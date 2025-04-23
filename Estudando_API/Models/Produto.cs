using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Estudando_API.Models;

[Table("Produtos")]
public class Produto
{
    [Key]
    public int ProdutoId { get; set; }
    [Required]
    [StringLength(100)]
    public string? Nome { get; set; }
    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Preco { get; set; }

    public float Estoque { get; set; }

    public int CategoriaId { get; set; }
    public int UsuarioId {  get; set; }

    [JsonIgnore]
    public Categoria? Categoria { get; set; }
    [JsonIgnore]
    public Usuario? Usuario { get; set; }


}
