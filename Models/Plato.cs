using System.ComponentModel.DataAnnotations;

namespace RestauranteAPI.Models
{
    public class Plato
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del plato es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre del plato no puede superar los 100 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "La descripción del plato es obligatoria.")]
        [StringLength(200, ErrorMessage = "La descripción no puede superar los 200 caracteres.")]
        public string Descripcion { get; set; } = string.Empty;

        [Range(0.01, 10000, ErrorMessage = "El precio debe ser mayor que cero y no superar los 10000.")]
        public float Precio { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo.")]
        public int Stock { get; set; }
    }
}