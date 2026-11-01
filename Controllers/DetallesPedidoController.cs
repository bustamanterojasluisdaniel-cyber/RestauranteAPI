using RestauranteAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace RestauranteAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DetallesPedidoController : ControllerBase
    {
        private static List<DetallePedido> detalles = new()
        {
            new DetallePedido
            {
                Id = 1,
                PedidoId = 1,
                PlatoId = 1,
                Cantidad = 2,
                PrecioUnitario = 15.00f,
                Subtotal = 30.00f
            },
            new DetallePedido
            {
                Id = 2,
                PedidoId = 2,
                PlatoId = 2,
                Cantidad = 2,
                PrecioUnitario = 22.00f,
                Subtotal = 44.00f
            }
        };

        // GET: api/DetallesPedido
        [HttpGet]
        public ActionResult<List<DetallePedido>> ObtenerDetalles()
        {
            return Ok(detalles);
        }

        // GET: api/DetallesPedido/1
        [HttpGet("{id:int}")]
        public ActionResult<DetallePedido> ObtenerDetalle(int id)
        {
            var detalle = detalles.FirstOrDefault(d => d.Id == id);

            if (detalle == null)
                return NotFound(new
                {
                    mensaje = "El detalle del pedido no existe."
                });

            return Ok(detalle);
        }

        // GET: api/DetallesPedido/pedido/1
        [HttpGet("pedido/{pedidoId:int}")]
        public ActionResult<List<DetallePedido>> ObtenerDetallesPorPedido(int pedidoId)
        {
            var detallesFiltrados = detalles
                .Where(d => d.PedidoId == pedidoId)
                .ToList();

            return Ok(detallesFiltrados);
        }

        // POST: api/DetallesPedido
        [HttpPost]
        public IActionResult CrearDetalle([FromBody] DetallePedido detalle)
        {
            if (detalle == null)
                return BadRequest(new
                {
                    mensaje = "Debe enviar los datos del detalle."
                });

            if (detalles.Any(d => d.Id == detalle.Id))
                return BadRequest(new
                {
                    mensaje = "Ya existe un detalle con ese ID."
                });

            if (detalle.Cantidad <= 0)
                return BadRequest(new
                {
                    mensaje = "La cantidad debe ser mayor que cero."
                });

            // Verificar que el pedido exista.
            var pedido = PedidosController.ObtenerPedidoPorId(detalle.PedidoId);

            if (pedido == null)
                return BadRequest(new
                {
                    mensaje = "El pedido indicado no existe."
                });

            // No permitir agregar detalles a pedidos cerrados.
            if (pedido.Estado == "Cancelado" ||
                pedido.Estado == "Entregado")
            {
                return BadRequest(new
                {
                    mensaje = "No se pueden agregar detalles a un pedido cancelado o entregado."
                });
            }

            // Verificar que el plato exista.
            var plato = PlatosController.ObtenerPlatoPorId(detalle.PlatoId);

            if (plato == null)
                return BadRequest(new
                {
                    mensaje = "El plato indicado no existe."
                });

            // Verificar que haya suficiente stock.
            if (plato.Stock < detalle.Cantidad)
                return BadRequest(new
                {
                    mensaje = "No hay suficientes porciones disponibles."
                });

            // Obtener el precio actual del plato y calcular el subtotal.
            detalle.PrecioUnitario = plato.Precio;
            detalle.Subtotal = detalle.Cantidad * detalle.PrecioUnitario;

            // Descontar las porciones del inventario.
            plato.Stock -= detalle.Cantidad;

            detalles.Add(detalle);

            return CreatedAtAction(
                nameof(ObtenerDetalle),
                new { id = detalle.Id },
                detalle
            );
        }

        // DELETE: api/DetallesPedido/1
        [HttpDelete("{id:int}")]
        public IActionResult EliminarDetalle(int id)
        {
            var detalle = detalles.FirstOrDefault(d => d.Id == id);

            if (detalle == null)
                return NotFound(new
                {
                    mensaje = "El detalle no existe."
                });

            // Buscar el plato para devolver las porciones al stock.
            var plato = PlatosController.ObtenerPlatoPorId(detalle.PlatoId);

            if (plato != null)
            {
                plato.Stock += detalle.Cantidad;
            }

            // Eliminar el detalle.
            detalles.Remove(detalle);

            return Ok(new
            {
                mensaje = "Detalle eliminado y stock devuelto correctamente."
            });
        }
    }
}