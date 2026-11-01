using System.ComponentModel.DataAnnotations;

namespace RestauranteAPI.Models
{
    public class DetallePedido
    {
        public int Id { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un pedido válido.")]
        public int PedidoId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un plato válido.")]
        public int PlatoId { get; set; } = 0;

        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser como mínimo 1.")]
        public int Cantidad { get; set; }

        [Range(0, float.MaxValue, ErrorMessage = "El precio unitario no puede ser negativo.")]
        public float PrecioUnitario { get; set; }

        [Range(0, float.MaxValue, ErrorMessage = "El subtotal no puede ser negativo.")]
        public float Subtotal { get; set; }
    }
}