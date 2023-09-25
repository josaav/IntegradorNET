# IntegradorNET
Trabajo Integrador Individual (Sección Backend) de la Academia .NET de Softtek y la Universidad del Museo Social Argentino (UMSA), 2023.

## 1. Arquitectura del proyecto
- **Controllers**: Esta capa gestiona las solicitudes HTTP y las respuestas del cliente, procesando las solicitudes y gestionando las respuestas adecuadas.
- **DataAccess**: Esta capa interactúa con la base de datos a través de Entity Framework y contiene dos subcapas:
- - **Database Seeding**: Se encarga de poblar la base de datos con datos iniciales o de prueba.
- - **Repositories**: Define y gestiona los repositorios que encapsulan las operaciones de acceso a la base de datos, proporcionando una abstracción entre la lógica de negocio y la base de datos.
- **DTOs**: Aquí se definen los objetos de transferencia de datos (DTO) utilizados para transportar datos entre las capas de la aplicación. Representan versiones simplificadas de las entidades de la base de datos.
- **Entities**: En esta capa se definen las entidades que representan los objetos en la base de datos. Cada entidad se mapea directamente a una tabla en la base de datos.
- **Helpers**: Esta capa contiene utilidades y funciones auxiliares, a saber: un helper de paginación para manejar las respuestas paginadas, un helper para la autenticación basada en tokens JWT y un helper para la encriptación de contraseñas.
- **Infraestructura**: Contiene componentes de infraestructura esenciales para la aplicación, particularmente un "response factory" utilizado para crear respuestas consistentes en toda la aplicación.
- **Migrations**: Esta capa se encarga de las migraciones de base de datos, permitiendo gestionar cambios controlados en el esquema de la base de datos a lo largo del tiempo.
- **Services**: Aquí se encuentra el patrón Unit Of Work, que proporciona una abstracción para gestionar transacciones de base de datos y garantizar la consistencia de los datos. Esta capa coordina las operaciones entre los repositorios y asegura la integridad de los datos en las transacciones.


## 2. Endpoints de la API
### 🟩 POST `/api/Login`: 
- **Descripción**: Permite a los usuarios autenticarse y obtener un JWT para acceder a los recursos protegidos de la API. 
- **Parámetros (Body)**:
- - `email` y `contrasena`: credenciales necesarias para la autenticación.
- **Respuesta exitosa**: Devolverá un JSON con un `status: 201`. Contendrá un token JWT válido que debe ser utilizado en las solicitudes posteriores a la autenticación. El token será válido por 12 horas.
---
### 🟦 GET `/api/Proyecto`
- **Descripción**: Permite hacer una lista de todos los proyectos de la empresa.
- **Parámetros opcionales (Route)**:
- - `items`: Cantidad de ítems que tendrá cada página
  - `pagina`: La página actual
- **Autorización**: Esta consulta puede ser realizada por usuarios con rol de Consultor o Administrador.
- **Respuesta exitosa**: Devolverá un objeto JSON con un `status: 200` y los datos solicitados. Si el parámetro de ruta `items` no fuese otorgado, el tamaño predeterminado de cada página será de 10 ítems. El JSON indicará la página actual, el tamaño de página, la cantidad total de páginas, la cantidad total de entidades, links a las páginas anteriores y posteriores, si hubiere, y los ítems solicitados. El campo "Eliminado" (borrado lógico) permitirá al cliente de front-end filtrar pertinentemente los resultados en una vista. Ejemplo:
```
{
  "status": 200,
  "data": {
    "paginaActual": 1,
    "tamanoPagina": 10,
    "paginasTotales": 2,
    "itemsTotales": 15,
    "urlPrev": null,
    "urlSig": "https://localhost:7274/Api/Proyecto/?pagina=2",
    "items": [
      {
        "codProyecto": 1,
        "nombre": "Proyecto Cuenca Andina",
        "direccion": "Sede Lima",
        "estado": {
          "id": 2,
          "nombre": "Confirmado"
        }
        "eliminado": 0
      }
    ]
  }
}
```
### 🟦 GET `/api/Proyecto/{id}`
- **Descripción**: Permite consultar el detalle de un proyecto a través de su ID.
- **Autorización**: Esta consulta puede ser realizada por usuarios con rol de Consultor o Administrador.
- **Parámetros (Route)**:
- - `id`: ID del proyecto que se desea consultar
- **Respuesta exitosa**: Devolverá un objeto JSON con un `status: 200` y los datos solicitados.

### 🟦 GET `/api/Proyecto/Estado/{id}`
- **Descripción**: Permite hacer una lista de todos los proyectos, otorgando la posibilidad de filtrar según su estado.
- **Parámetros (Route)**:
- - `id`: ID del estado, que puede ser 1 (Pendiente), 2 (Confirmado) o 3 (Terminado)
- **Parámetros opcionales (Route)**:
- - `items`: Cantidad de ítems que tendrá cada página
- - `pagina`: La página actual
- **Autorización**: Esta consulta puede ser realizada por usuarios con rol de Consultor o Administrador.
- **Respuesta exitosa**: Devolverá un objeto JSON con un `status: 200` y los datos solicitados. Si el parámetro de ruta `items` no fuese otorgado, el tamaño predeterminado de cada página será de 10 ítems. El JSON indicará la página actual, el tamaño de página, la cantidad total de páginas, la cantidad total de entidades, links a las páginas anteriores y posteriores, si hubiere, y los ítems solicitados. Para un ejemplo del JSON completo, ver el endpoint GET `/api/Proyecto/`

### 🟩 POST `/api/Proyecto`
- **Descripción**: Permite registrar un nuevo proyecto.
- **Autorización**: Esta operación puede ser realizada sólo por usuarios con rol de Administrador.
- **Parámetros (Body)**:
- - `nombre`: Nombre del proyecto
- - `direccion`: Dirección física donde se realiza el proyecto
- - `estado`: 1 (Pendiente), 2 (Confirmado) o 3 (Terminado)
- **Respuesta exitosa**: Devolverá un objeto JSON con un `status: 201` y un mensaje confirmando la operación.

### 🟧 PUT `/api/Proyecto`
- **Descripción**: Permite editar un proyecto.
- **Autorización**: Esta operación puede ser realizada sólo por usuarios con rol de Administrador.
- **Parámetros (Route)**:
- - `id`: ID del proyecto
- **Parámetros (Body)**:
- - `nombre`: Nombre del proyecto
- - `direccion`: Dirección física donde se realiza el proyecto
- - `estado`: 1 (Activo) o 0 (Inactivo)
- **Respuesta exitosa**: Devolverá un objeto JSON con un `status: 200` y un mensaje confirmando la operación.

### 🟥 DELETE `/api/Proyecto`
- **Descripción**: Permite realizar el borrado lógico (soft delete) de un proyecto
- **Autorización**: Esta operación puede ser realizada sólo por usuarios con rol de Administrador.
- **Parámetros (Route)**:
- - `id`: ID del proyecto
- **Respuesta exitosa**: Devolverá un objeto JSON con un `status: 200` y un mensaje confirmando la operación.

### 🟧 PUT `/api/Proyecto/Restaurar`
- **Descripción**: Permite restaurar un proyecto que se ha borrado mediante borrado lógico.
- **Autorización**: Esta operación puede ser realizada sólo por usuarios con rol de Administrador.
- **Parámetros (Route)**:
- - `id`: ID del proyecto
- **Respuesta exitosa**: Devolverá un objeto JSON con un `status: 200` y un mensaje confirmando la operación.

---

### 🟦 GET `/api/Servicio`
- **Descripción**: Permite hacer una lista de todos los servicios de la empresa.
- **Parámetros opcionales (Route)**:
- - `items`: Cantidad de ítems que tendrá cada página
  - `pagina`: La página actual
- **Autorización**: Esta consulta puede ser realizada por usuarios con rol de Consultor o Administrador.
- **Respuesta exitosa**: Devolverá un objeto JSON con un `status: 200` y los datos solicitados. Si el parámetro de ruta `items` no fuese otorgado, el tamaño predeterminado de cada página será de 10 ítems. El JSON indicará la página actual, el tamaño de página, la cantidad total de páginas, la cantidad total de entidades, links a las páginas anteriores y posteriores, si hubiere, y los ítems solicitados. El campo "Eliminado" (borrado lógico) permitirá al cliente de front-end filtrar pertinentemente los resultados en una vista. Para un ejemplo del JSON completo, ver el endpoint GET `/api/Proyecto/`

### 🟦 GET `/api/Servicio/{id}`
- **Descripción**: Permite consultar el detalle de un servicio a través de su ID.
- **Autorización**: Esta consulta puede ser realizada por usuarios con rol de Consultor o Administrador.
- **Parámetros (Route)**:
- - `id`: ID del servicio que se desea consultar
- **Respuesta exitosa**: Devolverá un objeto JSON con un `status: 200` y los datos solicitados.

### 🟦 GET `/api/Servicio/Activos`
- **Descripción**: Permite hacer una lista de todos los servicios **activos** que ofrece la empresa.
- **Parámetros opcionales (Route)**:
- - `items`: Cantidad de ítems que tendrá cada página
- - `pagina`: La página actual
- **Autorización**: Esta consulta puede ser realizada por usuarios con rol de Consultor o Administrador.
- **Respuesta exitosa**: Devolverá un objeto JSON con un `status: 200` y los datos solicitados. Si el parámetro de ruta `items` no fuese otorgado, el tamaño predeterminado de cada página será de 10 ítems. El JSON indicará la página actual, el tamaño de página, la cantidad total de páginas, la cantidad total de entidades, links a las páginas anteriores y posteriores, si hubiere, y los ítems solicitados. El campo "Eliminado" (borrado lógico) permitirá al cliente de front-end filtrar pertinentemente los resultados en una vista. Para un ejemplo del JSON completo, ver el endpoint GET `/api/Proyecto/`

### 🟩 POST `/api/Servicio`
- **Descripción**: Permite registrar un nuevo servicio.
- **Autorización**: Esta operación puede ser realizada sólo por usuarios con rol de Administrador.
- **Parámetros (Body)**:
- - `descripcion`: Descripción del servicio
- - `estado`: 1 (Activo) o 0 (Inactivo)
- - `valorHora`: Valor decimal que indica el costo por hora de ese servicio
- **Respuesta exitosa**: Devolverá un objeto JSON con un `status: 201` y un mensaje confirmando la operación

### 🟧 PUT `/api/Servicio`
- **Descripción**: Permite editar un servicio.
- **Autorización**: Esta operación puede ser realizada sólo por usuarios con rol de Administrador.
- **Parámetros (Route)**:
- - `id`: ID del servicio
- **Parámetros (Body)**:
- - `descripcion`: Descripción del servicio
- - `estado`: 1 (Activo) o 0 (Inactivo)
- - `valorHora`: Valor decimal que indica el costo por hora de ese servicio
- **Respuesta exitosa**: Devolverá un objeto JSON con un `status: 200` y un mensaje confirmando la operación.

### 🟥 DELETE `/api/Servicio`
- **Descripción**: Permite realizar el borrado lógico (soft delete) de un servicio
- **Autorización**: Esta operación puede ser realizada sólo por usuarios con rol de Administrador.
- **Parámetros (Route)**:
- - `id`: ID del servicio
- **Respuesta exitosa**: Devolverá un objeto JSON con un `status: 200` y un mensaje confirmando la operación.

### 🟧 PUT `/api/Servicio/Restaurar`
- **Descripción**: Permite restaurar un servicio que se ha borrado mediante borrado lógico.
- **Autorización**: Esta operación puede ser realizada sólo por usuarios con rol de Administrador.
- **Parámetros (Route)**:
- - `id`: ID del servicio
- **Respuesta exitosa**: Devolverá un objeto JSON con un `status: 200` y un mensaje confirmando la operación.

---

### 🟦 GET `/api/Trabajo`
- **Descripción**: Permite hacer una lista de todos los trabajos de la empresa.
- **Parámetros opcionales (Route)**:
- - `items`: Cantidad de ítems que tendrá cada página
  - `pagina`: La página actual
- **Autorización**: Esta consulta puede ser realizada por usuarios con rol de Consultor o Administrador.
- **Respuesta exitosa**: Devolverá un objeto JSON con un `status: 200` y los datos solicitados. Si el parámetro de ruta `items` no fuese otorgado, el tamaño predeterminado de cada página será de 10 ítems. El JSON indicará la página actual, el tamaño de página, la cantidad total de páginas, la cantidad total de entidades, links a las páginas anteriores y posteriores, si hubiere, y los ítems solicitados. El campo "Eliminado" (borrado lógico) permitirá al cliente de front-end filtrar pertinentemente los resultados en una vista. Para un ejemplo del JSON completo, ver el endpoint GET `/api/Proyecto/`

### 🟦 GET `/api/Trabajo/{id}`
- **Descripción**: Permite consultar el detalle de un trabajo a través de su ID.
- **Autorización**: Esta consulta puede ser realizada por usuarios con rol de Consultor o Administrador.
- **Parámetros (Route)**:
- - `id`: ID del trabajo que se desea consultar
- **Respuesta exitosa**: Devolverá un objeto JSON con un `status: 200` y los datos solicitados.

### 🟩 POST `/api/Trabajo`
- **Descripción**: Permite registrar un nuevo trabajo.
- **Autorización**: Esta operación puede ser realizada sólo por usuarios con rol de Administrador.
- **Parámetros (Body)**:
- - `fecha`: Fecha del trabajo (formato DateTime)
- - `codProyecto`: ID del proyecto asociado
- - `codServicio`: ID del servicio asociado
- - `cantHoras`: valor entero que representa la cantidad de horas solicitadas
- **Respuesta exitosa**: Devolverá un objeto JSON con un `status: 201` y un mensaje confirmando la operación

### 🟧 PUT `/api/Trabajo`
- **Descripción**: Permite editar un trabajo.
- **Autorización**: Esta operación puede ser realizada sólo por usuarios con rol de Administrador.
- **Parámetros (Route)**:
- - `id`: ID del trabajo
- **Parámetros (Body)**:
- - `fecha`: Fecha del trabajo (formato DateTime)
- - `codProyecto`: ID del proyecto asociado
- - `codServicio`: ID del servicio asociado
- - `cantHoras`: valor entero que representa la cantidad de horas solicitadas
- **Respuesta exitosa**: Devolverá un objeto JSON con un `status: 200` y un mensaje confirmando la operación.

### 🟥 DELETE `/api/Trabajo`
- **Descripción**: Permite realizar el borrado lógico (soft delete) de un trabajo
- **Autorización**: Esta operación puede ser realizada sólo por usuarios con rol de Administrador.
- **Parámetros (Route)**:
- - `id`: ID del trabajo
- **Respuesta exitosa**: Devolverá un objeto JSON con un `status: 200` y un mensaje confirmando la operación.

### 🟧 PUT `/api/Trabajo/Restaurar`
- **Descripción**: Permite restaurar un trabajo que se ha borrado mediante borrado lógico.
- **Autorización**: Esta operación puede ser realizada sólo por usuarios con rol de Administrador.
- **Parámetros (Route)**:
- - `id`: ID del trabajo
- **Respuesta exitosa**: Devolverá un objeto JSON con un `status: 200` y un mensaje confirmando la operación.

---

### 🟦 GET `/api/Usuario`
- **Descripción**: Permite hacer una lista de todos los usuarios de la empresa.
- **Parámetros opcionales (Route)**:
- - `items`: Cantidad de ítems que tendrá cada página
  - `pagina`: La página actual
- **Autorización**: Esta consulta puede ser realizada por usuarios con rol de Consultor o Administrador.
- **Respuesta exitosa**: Devolverá un objeto JSON con un `status: 200` y los datos solicitados. Si el parámetro de ruta `items` no fuese otorgado, el tamaño predeterminado de cada página será de 10 ítems. El JSON indicará la página actual, el tamaño de página, la cantidad total de páginas, la cantidad total de entidades, links a las páginas anteriores y posteriores, si hubiere, y los ítems solicitados. El campo "Eliminado" (borrado lógico) permitirá al cliente de front-end filtrar pertinentemente los resultados en una vista. Para un ejemplo del JSON completo, ver el endpoint GET `/api/Proyecto/`

### 🟦 GET `/api/Usuario/{id}`
- **Descripción**: Permite consultar el detalle de un usuario a través de su ID.
- **Autorización**: Esta consulta puede ser realizada por usuarios con rol de Consultor o Administrador.
- **Parámetros (Route)**:
- - `id`: ID del usuario que se desea consultar
- **Respuesta exitosa**: Devolverá un objeto JSON con un `status: 200` y los datos solicitados.

### 🟩 POST `/api/Usuario`
- **Descripción**: Permite registrar un nuevo usuario.
- **Autorización**: Esta operación puede ser realizada sólo por usuarios con rol de Administrador.
- **Parámetros (Body)**:
- - `nombre`: Nombre y apellido del usuario
- - `dni`: DNI del usuario
- - `email`: Email del usuario
- - `tipo`: 1 (Administrador) o 2 (Consultor)
- - `contrasena`: Contraseña del usuario
- **Respuesta exitosa**: Devolverá un objeto JSON con un `status: 201` y un mensaje confirmando la operación

### 🟧 PUT `/api/Usuario`
- **Descripción**: Permite editar un usuario.
- **Autorización**: Esta operación puede ser realizada sólo por usuarios con rol de Administrador.
- **Parámetros (Route)**:
- - `id`: ID del usuario
- **Parámetros (Body)**:
- - `nombre`: Nombre y apellido del usuario
- - `dni`: DNI del usuario
- - `email`: Email del usuario
- - `tipo`: 1 (Administrador) o 2 (Consultor)
- - `contrasena`: Contraseña del usuario
- **Respuesta exitosa**: Devolverá un objeto JSON con un `status: 200` y un mensaje confirmando la operación.

### 🟥 DELETE `/api/Usuario`
- **Descripción**: Permite realizar el borrado lógico (soft delete) de un usuario
- **Autorización**: Esta operación puede ser realizada sólo por usuarios con rol de Administrador.
- **Parámetros (Route)**:
- - `id`: ID del usuario
- **Respuesta exitosa**: Devolverá un objeto JSON con un `status: 200` y un mensaje confirmando la operación.

### 🟧 PUT `/api/Usuario/Restaurar`
- **Descripción**: Permite restaurar un usuario que se ha borrado mediante borrado lógico.
- **Autorización**: Esta operación puede ser realizada sólo por usuarios con rol de Administrador.
- **Parámetros (Route)**:
- - `id`: ID del usuario
- **Respuesta exitosa**: Devolverá un objeto JSON con un `status: 200` y un mensaje confirmando la operación.

