using System;
using System.ComponentModel.DataAnnotations;

namespace RestauranteAPI.Models
{
    public class Pedido
    {
        public int Id { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un cliente válido.")]
        public int ClienteId { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "El estado del pedido es obligatorio.")]
        [StringLength(30, ErrorMessage = "El estado no puede superar los 30 caracteres.")]
        public string Estado { get; set; } = "Pendiente";

        [Range(0, float.MaxValue, ErrorMessage = "El total del pedido no puede ser negativo.")]
        public float Total { get; set; }
    }
}