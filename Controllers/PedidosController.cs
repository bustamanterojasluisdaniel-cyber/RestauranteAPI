using RestauranteAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace RestauranteAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private static List<Pedido> pedidos = new()
        {
            new Pedido {
                Id = 1,
                ClienteId = 1,
                Estado = "Pendiente",
                Total = 30.00f
            },
            new Pedido {
                Id = 2,
                ClienteId = 2,
                Estado = "EnPreparacion",
                Total = 44.00f
            },
            new Pedido {
                Id = 3,
                ClienteId = 3,
                Estado = "Entregado",
                Total = 25.00f
            },

            new Pedido {
                Id = 4,
                ClienteId = 4,
                Estado = "Pendiente",
                Total = 56.00f
            },
            new Pedido {
                Id = 5,
                ClienteId = 5,
                Estado = "Pagado",
                Total = 45.00f
            },
            new Pedido {
                Id = 6,
                ClienteId = 6,
                Estado = "EnPreparacion",
                Total = 60.00f
            }
        };

        private static readonly string[] EstadosPermitidos =
        {
            "Pendiente",
            "Pagado",
            "EnPreparacion",
            "Entregado",
            "Cancelado"

        };

        public static bool ExistePedido(int id)
        {
            return pedidos.Any(p => p.Id == id);
        }

        // GET: api/Pedidos
        [HttpGet]
        public ActionResult<List<Pedido>> ObtenerPedidos()
        {
            return Ok(pedidos);
        }

        // GET: api/Pedidos/1
        [HttpGet("{id:int}")]
        public ActionResult<Pedido> ObtenerPedido(int id)
        {
            var pedido = pedidos.FirstOrDefault(p => p.Id == id);

            if (pedido == null)
                return NotFound(new { mensaje = "El pedido no existe." });

            return Ok(pedido);
        }

        // GET: api/Pedidos/estado/Pendiente
        [HttpGet("estado/{estado}")]
        public ActionResult<List<Pedido>> ObtenerPedidosPorEstado(string estado)
        {
            if (!EstadosPermitidos.Contains(estado))
                return BadRequest(new { mensaje = "El estado indicado no es válido." });

            var pedidosFiltrados = pedidos
                .Where(p => p.Estado == estado)
                .ToList();

            return Ok(pedidosFiltrados);
        }

        // GET: api/Pedidos/porfecha?inicio=2026-10-31&fin=2026-10-31
        [HttpGet("porfecha")]
        public ActionResult<List<Pedido>> ObtenerPedidosPorFecha(DateTime inicio, DateTime fin)
        {
            if (inicio.Date > fin.Date)
                return BadRequest(new { mensaje = "La fecha inicial no puede ser mayor que la fecha final." });

            var pedidosFiltrados = pedidos
                .Where(p => p.Fecha.Date >= inicio.Date && p.Fecha.Date <= fin.Date)
                .ToList();

            return Ok(pedidosFiltrados);
        }
        // GET: api/Pedidos/cliente/1
        [HttpGet("cliente/{clienteId:int}")]
        public ActionResult<List<Pedido>> ObtenerPedidosPorCliente(int clienteId)
        {
            if (!ClientesController.ExisteCliente(clienteId))
                return NotFound(new { mensaje = "El cliente no existe." });

            var pedidosFiltrados = pedidos
                .Where(p => p.ClienteId == clienteId)
                .ToList();

            return Ok(pedidosFiltrados);
        }

        // PUT: api/Pedidos/pagar/1
        [HttpPut("pagar/{id:int}")]
        public IActionResult PagarPedido(int id)
        {
            var pedido = pedidos.FirstOrDefault(p => p.Id == id);

            if (pedido == null)
                return NotFound(new { mensaje = "El pedido no existe." });

            if (pedido.Estado == "Cancelado")
                return BadRequest(new { mensaje = "No se puede pagar un pedido cancelado." });

            if (pedido.Estado == "Entregado")
                return BadRequest(new { mensaje = "No se puede pagar un pedido que ya fue entregado." });

            if (pedido.Estado == "Pagado")
                return BadRequest(new { mensaje = "El pedido ya se encuentra pagado." });

            if (pedido.Estado != "Pendiente")
                return BadRequest(new { mensaje = "Solo se pueden pagar pedidos pendientes." });

            pedido.Estado = "Pagado";

            return Ok(new { mensaje = "Pago registrado correctamente.", pedido });
        }

        // POST: api/Pedidos
        [HttpPost]
        public IActionResult CrearPedido([FromBody] Pedido pedido)
        {
            if (pedido == null)
                return BadRequest(new { mensaje = "Debe enviar los datos del pedido." });

            if (pedidos.Any(p => p.Id == pedido.Id))
                return BadRequest(new { mensaje = "Ya existe un pedido con ese ID." });

            if (!ClientesController.ExisteCliente(pedido.ClienteId))
                return BadRequest(new { mensaje = "El cliente indicado no existe." });

            if (pedido.Total < 0)
                return BadRequest(new { mensaje = "El total no puede ser negativo." });

            // Todo pedido nuevo comienza pendiente.
            pedido.Estado = "Pendiente";
            pedido.Fecha = DateTime.Now;

            pedidos.Add(pedido);

            return CreatedAtAction(
                nameof(ObtenerPedido),
                new { id = pedido.Id },
                pedido
            );
        }

        // PUT: api/Pedidos/1
        [HttpPut("{id:int}")]
        public IActionResult ActualizarPedido(int id, [FromBody] Pedido pedidoActualizado)
        {
            if (pedidoActualizado == null)
                return BadRequest(new { mensaje = "Debe enviar los datos del pedido." });

            if (id != pedidoActualizado.Id)
                return BadRequest(new { mensaje = "El ID de la ruta no coincide con el ID del pedido." });

            var pedidoExistente = pedidos.FirstOrDefault(p => p.Id == id);

            if (pedidoExistente == null)
                return NotFound(new { mensaje = "El pedido no existe." });

            if (!ClientesController.ExisteCliente(pedidoActualizado.ClienteId))
                return BadRequest(new { mensaje = "El cliente indicado no existe." });

            if (!EstadosPermitidos.Contains(pedidoActualizado.Estado))
                return BadRequest(new { mensaje = "El estado indicado no es válido." });

            // Validar los cambios de estado del pedido
            if (pedidoExistente.Estado == "Entregado" ||
                pedidoExistente.Estado == "Cancelado")
            {
                return BadRequest(new
                {
                    mensaje = "No se puede modificar un pedido entregado o cancelado."
                });
            }

            if (pedidoExistente.Estado == "Pendiente" &&
                pedidoActualizado.Estado != "Pagado" &&
                pedidoActualizado.Estado != "Cancelado")
            {
                return BadRequest(new
                {
                    mensaje = "Un pedido pendiente solo puede pasar a Pagado o Cancelado."
                });
            }

            if (pedidoExistente.Estado == "Pagado" &&
                pedidoActualizado.Estado != "EnPreparacion" &&
                pedidoActualizado.Estado != "Cancelado")
            {
                return BadRequest(new
                {
                    mensaje = "Un pedido pagado solo puede pasar a EnPreparacion o Cancelado."
                });
            }

            if (pedidoExistente.Estado == "EnPreparacion" &&
                pedidoActualizado.Estado != "Entregado" &&
                pedidoActualizado.Estado != "Cancelado")
            {
                return BadRequest(new
                {
                    mensaje = "Un pedido en preparación solo puede pasar a Entregado o Cancelado."
                });
            }

            if (pedidoActualizado.Total < 0)
                return BadRequest(new { mensaje = "El total no puede ser negativo." });

            pedidoExistente.ClienteId = pedidoActualizado.ClienteId;
            pedidoExistente.Estado = pedidoActualizado.Estado;
            pedidoExistente.Total = pedidoActualizado.Total;

            return Ok(pedidoExistente);
        }

        public static Pedido? ObtenerPedidoPorId(int id)
        {
            return pedidos.FirstOrDefault(p => p.Id == id);
        }

        // DELETE: api/Pedidos/1
        [HttpDelete("{id:int}")]
        public IActionResult EliminarPedido(int id)
        {
            var pedido = pedidos.FirstOrDefault(p => p.Id == id);

            if (pedido == null)
                return NotFound(new { mensaje = "El pedido no existe." });

            pedidos.Remove(pedido);

            return Ok(new { mensaje = "El pedido se eliminó correctamente." });
        }
    }
}