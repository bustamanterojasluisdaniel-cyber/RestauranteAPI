# 🍽️ RestauranteAPI

API REST desarrollada en **ASP.NET Core con .NET 8 y C#** para gestionar la información de un restaurante. Permite realizar operaciones CRUD sobre clientes, platos, pedidos y detalles de pedidos, además de aplicar validaciones para controlar los datos enviados a través de las solicitudes HTTP.

## 📋 Descripción del proyecto

RestauranteAPI tiene como objetivo facilitar la gestión de la información de un restaurante mediante servicios web REST. La API permite consultar, registrar, actualizar y eliminar registros utilizando herramientas como Postman.

El proyecto está organizado mediante controladores y modelos, lo que facilita la comprensión, el mantenimiento y la ampliación del código.

## 🎯 Objetivos

- Implementar una API REST utilizando ASP.NET Core.
- Gestionar clientes, platos, pedidos y detalles de pedidos.
- Implementar operaciones CRUD mediante solicitudes HTTP.
- Validar los datos recibidos para reducir errores en los registros.
- Probar los endpoints utilizando Postman.
- Organizar el código fuente para facilitar su mantenimiento.

## 🛠️ Tecnologías utilizadas

- **Lenguaje de programación:** C#.
- **Framework:** ASP.NET Core.
- **Versión de .NET:** .NET 8.
- **Entorno de desarrollo:** Visual Studio 2022.
- **Herramienta de pruebas:** Postman.
- **Control de versiones:** Git.
- **Repositorio del proyecto:** GitHub.

## 📂 Estructura del proyecto

```text
RestauranteAPI/
├── Controllers/
│   ├── ClientesController.cs
│   ├── PlatosController.cs
│   ├── PedidosController.cs
│   ├── DetallesPedidoController.cs
│   └── WeatherForecastController.cs
│
├── Models/
│   ├── Cliente.cs
│   ├── Plato.cs
│   ├── Pedido.cs
│   └── DetallePedido.cs
│
├── Properties/
│   └── launchSettings.json
│
├── Program.cs
├── appsettings.json
├── appsettings.Development.json
├── RestauranteAPI.csproj
├── RestauranteAPI.http
├── .gitignore
└── README.md
```

### Descripción de las carpetas y archivos

- **Controllers:** contiene los controladores que gestionan las solicitudes HTTP y las operaciones CRUD.
- **Models:** contiene las clases que representan los datos de clientes, platos, pedidos y detalles de pedidos.
- **Properties:** contiene configuraciones utilizadas para ejecutar el proyecto durante el desarrollo.
- **Program.cs:** configura e inicia la aplicación web.
- **appsettings.json:** contiene la configuración general de la aplicación.
- **RestauranteAPI.csproj:** define las características y dependencias del proyecto .NET.
- **RestauranteAPI.http:** permite preparar solicitudes HTTP para realizar pruebas.
- **.gitignore:** especifica los archivos y carpetas que Git debe excluir del repositorio.

## ⚙️ Funcionalidades principales

### 1. Gestión de clientes

Permite:
- Consultar la lista de clientes.
- Buscar un cliente por su identificador.
- Registrar nuevos clientes.
- Actualizar los datos de un cliente.
- Eliminar clientes.
- Validar campos obligatorios, teléfono y correo electrónico.

### 2. Gestión de platos

Permite:
- Consultar los platos registrados.
- Buscar platos por su identificador.
- Registrar nuevos platos.
- Actualizar la información de los platos.
- Eliminar platos.
- Validar el nombre, la descripción, el precio y el stock.

### 3. Gestión de pedidos

Permite:
- Consultar los pedidos registrados.
- Buscar pedidos por su identificador.
- Registrar pedidos.
- Actualizar pedidos.
- Eliminar pedidos.
- Validar los datos del pedido.

### 4. Gestión de detalles de pedidos

Permite:
- Consultar los detalles de los pedidos.
- Buscar detalles por su identificador.
- Registrar detalles de pedidos.
- Actualizar detalles.
- Eliminar detalles.
- Validar la cantidad y las referencias al pedido y al plato.

Las operaciones disponibles y las reglas específicas dependen de la implementación de cada controlador.

## ✅ Validaciones implementadas

Los modelos utilizan atributos de validación de C# para comprobar los datos recibidos.

Entre las validaciones implementadas se encuentran:

- Campos obligatorios.
- Longitud máxima de textos.
- Formato del correo electrónico.
- Número de dígitos del teléfono.
- Precio mayor que cero para los platos.
- Stock y cantidades dentro de los rangos permitidos.
- Identificadores válidos para pedidos y detalles.

Cuando una solicitud no cumple las validaciones automáticas del modelo, la API puede responder con el código HTTP `400 Bad Request` y los detalles de los errores.

## 📦 Requisitos previos

Para ejecutar el proyecto se necesita:

1. Windows u otro sistema operativo compatible con .NET 8.
2. Visual Studio 2022 con la carga de trabajo de desarrollo de ASP.NET y web, o el SDK de .NET 8.
3. Postman para probar los endpoints.

No se requiere una base de datos para la versión actual, ya que los controladores utilizan listas en memoria. Por ello, los registros creados durante la ejecución no se conservan al reiniciar la aplicación.

## 🚀 Instalación y ejecución

### Opción 1: Visual Studio 2022

1. Descarga o clona el repositorio de GitHub.
2. Abre el archivo `RestauranteAPI.csproj` o la solución del proyecto en Visual Studio 2022.
3. Espera a que Visual Studio restaure las dependencias.
4. Compila el proyecto desde el menú **Compilar → Compilar solución**.
5. Ejecuta la aplicación con el botón verde de inicio.
6. Revisa la dirección HTTPS o HTTP que aparece en la configuración de ejecución o en la consola.

### Opción 2: Terminal

Abre una terminal dentro de la carpeta que contiene `RestauranteAPI.csproj` y ejecuta:

```bash
dotnet restore
dotnet build
dotnet run
```

La terminal mostrará la dirección local en la que se ejecuta la API.

## 🧪 Pruebas con Postman

Para probar la API:

1. Abre Postman.
2. Ejecuta el proyecto en Visual Studio o desde la terminal.
3. Copia la dirección local que utiliza la aplicación.
4. Añade la ruta del endpoint que deseas probar.
5. Selecciona el método HTTP correspondiente.
6. Para solicitudes `POST` y `PUT`, selecciona **Body → raw → JSON** cuando el endpoint reciba datos JSON.
7. Envía la solicitud y revisa el código de respuesta y el contenido devuelto.

### Métodos HTTP utilizados

| Método HTTP | Operación | Descripción |
|---|---|---|
| GET | Consultar | Obtiene registros o busca un registro específico. |
| POST | Crear | Registra un nuevo elemento. |
| PUT | Actualizar | Modifica un registro existente. |
| DELETE | Eliminar | Elimina un registro según su identificador. |

### Endpoints principales

Los controladores utilizan la ruta base `api/[controller]`.

| Recurso | Ruta base |
|---|---|
| Clientes | `/api/Clientes` |
| Platos | `/api/Platos` |
| Pedidos | `/api/Pedidos` |
| Detalles de pedidos | `/api/DetallesPedido` |

La disponibilidad de las rutas específicas depende de los métodos definidos en cada controlador.

### Ejemplo de solicitud POST para registrar un cliente

```http
POST /api/Clientes
Content-Type: application/json
```

```json
{
  "id": 7,
  "nombre": "Pedro",
  "apellido": "Ramirez",
  "telefono": "987654321",
  "correo": "pedro.ramirez@gmail.com"
}
```

Este ejemplo presupone que el identificador y el correo electrónico no están registrados y que el controlador acepta esos datos.

**Nota:** La dirección completa de la solicitud debe incluir la URL local que muestra Visual Studio cuando se inicia la API.

## 🔍 Códigos de respuesta HTTP

| Código | Significado |
|---|---|
| 200 OK | La solicitud se procesó correctamente. |
| 201 Created | Se creó un registro correctamente. |
| 204 No Content | La operación se completó sin contenido de respuesta. |
| 400 Bad Request | La solicitud contiene datos inválidos. |
| 404 Not Found | No se encontró el recurso solicitado. |
| 500 Internal Server Error | Se produjo un error interno del servidor. |

Los códigos concretos pueden variar según la implementación de cada endpoint.

## ⚠️ Consideraciones

- La versión actual utiliza listas en memoria, no una base de datos persistente.
- Los datos iniciales y las reglas de negocio están definidos en los controladores.
- Los registros creados durante la ejecución se pierden al reiniciar la aplicación.
- Para probar las solicitudes, la API debe estar ejecutándose.
- Las rutas y los códigos de respuesta deben verificarse con la implementación actual de cada controlador.

## 👥 Autores

Proyecto académico desarrollado para practicar la creación de servicios web REST, operaciones CRUD y validación de datos con ASP.NET Core.

## 📄 Licencia

Proyecto de carácter académico y educativo.
