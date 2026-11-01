using RestauranteAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace RestauranteAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlatosController : ControllerBase
    {
        private static List<Plato> platos = new()
        {
            new Plato { 
                Id = 1, 
                Nombre = "Arroz con pollo", 
                Descripcion = "Arroz, pollo y verduras", 
                Precio = 15.00f, 
                Stock = 18 
            },
            new Plato { 
                Id = 2, 
                Nombre = "Lomo saltado", 
                Descripcion = "Carne, cebolla, tomate y papas", 
                Precio = 22.00f, 
                Stock = 13 
            },
            new Plato { 
                Id = 3, 
                Nombre = "Ceviche", 
                Descripcion = "Pescado, limón y cebolla", 
                Precio = 25.00f, 
                Stock = 10 
            },
            new Plato { 
                Id = 4, 
                Nombre = "Ají de gallina", 
                Descripcion = "Pollo, ají amarillo y arroz", 
                Precio = 18.00f, 
                Stock = 12 
            },
            new Plato { 
                Id = 5,
                Nombre = "Tallarin saltado", 
                Descripcion = "Tallarines salteados con verduras y carne", 
                Precio = 20.00f, 
                Stock = 8 
            },
            new Plato {
                Id = 6,
                Nombre = "Pollo a la brasa",
                Descripcion = "Pollo a la brasa con papas fritas",
                Precio = 28.00f,
                Stock = 15
            },
            new Plato {
                Id = 7,
                Nombre = "Arroz chaufa",
                Descripcion = "Arroz salteado con pollo y verduras",
                Precio = 16.00f,
                Stock = 20
            },
                new Plato {
                Id = 8,
                Nombre = "Papa a la huancaina",
                Descripcion = "Papas sancochadas con salsa a la huancaina",
                Precio = 12.00f,
                Stock = 18
            }

        };

        public static Plato? ObtenerPlatoPorId(int id)
        {
            return platos.FirstOrDefault(p => p.Id == id);
        }

        // GET: api/Platos
        [HttpGet]
        public ActionResult<List<Plato>> ObtenerPlatos()
        {
            return Ok(platos);
        }

        // GET: api/Platos/1
        [HttpGet("{id:int}")]
        public ActionResult<Plato> ObtenerPlato(int id)
        {
            var plato = platos.FirstOrDefault(p => p.Id == id);

            if (plato == null)
                return NotFound(new { mensaje = "El plato no existe." });

            return Ok(plato);
        }

        // GET: api/Platos/stockminimo/5
        [HttpGet("stockminimo/{stock:int}")]
        public ActionResult<List<Plato>> ObtenerPlatosConStockMinimo(int stock)
        {
            if (stock < 0)
                return BadRequest(new { mensaje = "El stock no puede ser negativo." });

            var platosFiltrados = platos
                .Where(p => p.Stock >= stock)
                .ToList();

            return Ok(platosFiltrados);
        }

        // GET: api/Platos/stockmaximo/10
        [HttpGet("stockmaximo/{stock:int}")]
        public ActionResult<List<Plato>> ObtenerPlatosConStockMaximo(int stock)
        {
            if (stock < 0)
                return BadRequest(new { mensaje = "El stock no puede ser negativo." });

            var platosFiltrados = platos
                .Where(p => p.Stock <= stock)
                .ToList();

            return Ok(platosFiltrados);
        }


        // GET: api/Platos/preciominimo/20
        [HttpGet("preciominimo/{minimo:float}")]
        public ActionResult<List<Plato>> ObtenerPlatosPorPrecioMinimo(float minimo)
        {
            if (minimo < 0)
                return BadRequest(new { mensaje = "El precio mínimo no puede ser negativo." });

            var platosFiltrados = platos
                .Where(p => p.Precio >= minimo)
                .OrderBy(p => p.Precio)
                .ToList();

            return Ok(platosFiltrados);
        }


        // GET: api/Platos/preciomaximo/20
        [HttpGet("preciomaximo/{maximo:float}")]
        public ActionResult<List<Plato>> ObtenerPlatosPorPrecioMaximo(float maximo)
        {
            if (maximo < 0)
                return BadRequest(new { mensaje = "El precio máximo no puede ser negativo." });

            var platosFiltrados = platos
                .Where(p => p.Precio <= maximo)
                .OrderBy(p => p.Precio)
                .ToList();

            return Ok(platosFiltrados);
        }
        // POST: api/m
        [HttpPost]
        public IActionResult CrearPlato([FromBody] Plato plato)
        {
            if (plato == null)
                return BadRequest(new { mensaje = "Debe enviar los datos del plato." });

            if (string.IsNullOrWhiteSpace(plato.Nombre))
                return BadRequest(new { mensaje = "El nombre es obligatorio." });

            if (plato.Precio <= 0 || plato.Stock < 0)
                return BadRequest(new { mensaje = "El precio debe ser mayor que cero y el stock no puede ser negativo." });

            if (platos.Any(p => p.Id == plato.Id))
                return BadRequest(new { mensaje = "Ya existe un plato con ese ID." });

            platos.Add(plato);

            return CreatedAtAction(
                nameof(ObtenerPlato),
                new { id = plato.Id },
                plato
            );
        }

        // PUT: api/Platos/1
        [HttpPut("{id:int}")]
        public IActionResult ActualizarPlato(int id, [FromBody] Plato platoActualizado)
        {
            if (platoActualizado == null)
                return BadRequest(new { mensaje = "Debe enviar los datos del plato." });

            if (id != platoActualizado.Id)
                return BadRequest(new { mensaje = "El ID de la ruta no coincide con el ID del plato." });

            if (string.IsNullOrWhiteSpace(platoActualizado.Nombre))
                return BadRequest(new { mensaje = "El nombre es obligatorio." });

            if (platoActualizado.Precio <= 0 || platoActualizado.Stock < 0)
                return BadRequest(new { mensaje = "El precio debe ser mayor que cero y el stock no puede ser negativo." });

            var platoExistente = platos.FirstOrDefault(p => p.Id == id);

            if (platoExistente == null)
                return NotFound(new { mensaje = "El plato no existe." });

            platoExistente.Nombre = platoActualizado.Nombre;
            platoExistente.Descripcion = platoActualizado.Descripcion;
            platoExistente.Precio = platoActualizado.Precio;
            platoExistente.Stock = platoActualizado.Stock;

            return Ok(platoExistente);
        }

        // DELETE: api/Platos/1
        [HttpDelete("{id:int}")]
        public IActionResult EliminarPlato(int id)
        {
            var platoExistente = platos.FirstOrDefault(p => p.Id == id);

            if (platoExistente == null)
                return NotFound(new { mensaje = "El plato no existe." });

            platos.Remove(platoExistente);

            return Ok(new { mensaje = "Plato eliminado correctamente." });
        }

        // GET: api/Platos/porprecio/10/25
        [HttpGet("porprecio/{minimo:float}/{maximo:float}")]
        public ActionResult<List<Plato>> FiltrarPorPrecio(float minimo, float maximo)
        {
            if (minimo < 0 || maximo <= minimo)
                return BadRequest(new { mensaje = "El rango de precios no es válido." });

            var platosFiltrados = platos
                .Where(p => p.Precio >= minimo && p.Precio <= maximo)
                .OrderBy(p => p.Precio)
                .ToList();

            return Ok(platosFiltrados);
        }

        // PUT: api/Platos/actualizarstock/1/2
        [HttpPut("actualizarstock/{id:int}/{cantidad:int}")]
        public IActionResult ActualizarStock(int id, int cantidad)
        {
            if (cantidad <= 0)
                return BadRequest(new { mensaje = "La cantidad debe ser mayor que cero." });

            var platoExistente = platos.FirstOrDefault(p => p.Id == id);

            if (platoExistente == null)
                return NotFound(new { mensaje = "No se encontró el plato." });

            if (platoExistente.Stock < cantidad)
                return BadRequest(new { mensaje = "No hay suficientes porciones disponibles." });

            platoExistente.Stock -= cantidad;

            return Ok(platoExistente);
        }
    }
}
