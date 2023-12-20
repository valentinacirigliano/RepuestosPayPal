using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Repuestos2023.Models.Models
{
    [Index(nameof(NombreCategoria), IsUnique = true)]
    public class Categoria
    {
        [Key]
        public int CategoriaId { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The field {0} must be between {2} y {1} characters", MinimumLength = 3)]
        [DisplayName("Category Name")]
        public string NombreCategoria { get; set; }

    }
}
