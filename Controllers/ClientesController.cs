using RestauranteAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace RestauranteAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private static List<Cliente> clientes = new()
        {
            new Cliente { 
                Id = 1, 
                Nombre = "Daniel", 
                Apellido = "Bustamante", 
                Telefono = "995695422", 
                Correo = "daniel@gmail.com" 
            },
            new Cliente { 
                Id = 2, 
                Nombre = "Merly", 
                Apellido = "Falla", 
                Telefono = "976543210", 
                Correo = "merly@gmail.com" 
            },
            new Cliente { 
                Id = 3, 
                Nombre = "Jose", 
                Apellido = "Torres", 
                Telefono = "965432109", 
                Correo = "jose@gmail.com" 
            },
            new Cliente
            {
                Id = 4,
                Nombre = "Lucia",
                Apellido = "Fernandez",
                Telefono = "987123456",
                Correo = "lucia.fernandez@gmail.com"
            },
            new Cliente
            {
                Id = 5,
                Nombre = "Miguel",
                Apellido = "Castillo",
                Telefono = "986234567",
                Correo = "miguel.castillo@gmail.com"
            },
            new Cliente
            {
                Id = 6,
                Nombre = "Andrea",
                Apellido = "Vasquez",
                Telefono = "985345678",
                Correo = "andrea.vasquez@gmail.com"
            }
        };

        public static bool ExisteCliente(int id)
        {
            return clientes.Any(c => c.Id == id);
        }

        // GET: api/Clientes
        [HttpGet]
        public ActionResult<List<Cliente>> ObtenerClientes()
        {
            return Ok(clientes);
        }

        // GET: api/Clientes/1
        [HttpGet("{id:int}")]
        public ActionResult<Cliente> ObtenerCliente(int id)
        {
            var cliente = clientes.FirstOrDefault(c => c.Id == id);

            if (cliente == null)
                return NotFound(new { mensaje = "El cliente no existe." });

            return Ok(cliente);
        }

        // POST: api/Clientes
        [HttpPost]
        public IActionResult CrearCliente([FromBody] Cliente cliente)
        {
            if (cliente == null)
                return BadRequest(new { mensaje = "Debe enviar los datos del cliente." });

            if (string.IsNullOrWhiteSpace(cliente.Nombre) ||
                string.IsNullOrWhiteSpace(cliente.Apellido))
                return BadRequest(new { mensaje = "El nombre y apellido son obligatorios." });

            if (clientes.Any(c => c.Id == cliente.Id))
                return BadRequest(new { mensaje = "Ya existe un cliente con ese ID." });

            if (!string.IsNullOrWhiteSpace(cliente.Correo) &&
                clientes.Any(c => c.Correo == cliente.Correo))
                return BadRequest(new { mensaje = "El correo ya está registrado." });

            clientes.Add(cliente);

            return CreatedAtAction(
                nameof(ObtenerCliente),
                new { id = cliente.Id },
                cliente
            );
        }

        // PUT: api/Clientes/1
        [HttpPut("{id:int}")]
        public IActionResult ActualizarCliente(int id, [FromBody] Cliente clienteActualizado)
        {
            if (clienteActualizado == null)
                return BadRequest(new { mensaje = "Debe enviar los datos del cliente." });

            if (id != clienteActualizado.Id)
                return BadRequest(new { mensaje = "El ID de la ruta no coincide con el ID del cliente." });

            if (string.IsNullOrWhiteSpace(clienteActualizado.Nombre) ||
                string.IsNullOrWhiteSpace(clienteActualizado.Apellido))
                return BadRequest(new { mensaje = "El nombre y apellido son obligatorios." });

            var clienteExistente = clientes.FirstOrDefault(c => c.Id == id);

            if (clienteExistente == null)
                return NotFound(new { mensaje = "El cliente no existe." });

            if (!string.IsNullOrWhiteSpace(clienteActualizado.Correo) &&
                clientes.Any(c => c.Id != id && c.Correo == clienteActualizado.Correo))
                return BadRequest(new { mensaje = "El correo ya está registrado por otro cliente." });

            clienteExistente.Nombre = clienteActualizado.Nombre;
            clienteExistente.Apellido = clienteActualizado.Apellido;
            clienteExistente.Telefono = clienteActualizado.Telefono;
            clienteExistente.Correo = clienteActualizado.Correo;

            return Ok(clienteExistente);
        }

        // DELETE: api/Clientes/1
        [HttpDelete("{id:int}")]
        public IActionResult EliminarCliente(int id)
        {
            var cliente = clientes.FirstOrDefault(c => c.Id == id);

            if (cliente == null)
                return NotFound(new { mensaje = "El cliente no existe." });

            clientes.Remove(cliente);

            return NoContent();
        }
    }
}
