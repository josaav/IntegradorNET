using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntegradorNET.DTOs;
using IntegradorNET.Entities;
using IntegradorNET.Helpers;
using IntegradorNET.Infrastructure;
using IntegradorNET.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IntegradorNET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrabajoController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public TrabajoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Devuelve todos los trabajos con un paginado. Toma dos parámetros opcionales de la ruta,
        /// con los cuales realiza un paginado, "items" (cantidad de items por página) y "pagina"
        /// (número de página). De no ser enviados dichos parámetros, la página que muestra primero
        /// es la primera, y hay 10 ítems por página.
        /// </summary>
        /// <returns>Retorna todos los trabajos</returns>

        [Authorize(Policy = "AdminConsultor")]
        [HttpGet]
        public async Task<IActionResult> ObtenerTrabajos()
        {
            var trabajos = await _unitOfWork.TrabajoRepository.ObtenerTodos();

            List<TrabajoDetalleDto> lista = trabajos.Select(entity => new TrabajoDetalleDto
            {
                CodTrabajo = entity.Id,
                Fecha = entity.Fecha,
                CodProyecto = entity.ProyectoId,
                NombreProyecto = entity.Proyecto.Nombre,
                CodServicio = entity.ServicioId,
                DescripcionServicio = entity.Servicio.Descripcion,
                CantHoras = entity.CantHoras,
                ValorHora = entity.ValorHora,
                Costo = entity.Costo,
                Eliminado = entity.Eliminado
            }).ToList();

            int paginaPorMostrar = 1;
            int tamanoPagina = 10;

            if (Request.Query.ContainsKey("pagina")) int.TryParse(Request.Query["pagina"], out paginaPorMostrar);
            if (Request.Query.ContainsKey("items")) int.TryParse(Request.Query["items"], out tamanoPagina);

            var url = new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}").ToString();

            var listaPaginada = PaginateHelper.Paginate(lista, paginaPorMostrar, tamanoPagina, url);

            return ResponseFactory.CreateSuccessResponse(200, listaPaginada);
        }

        /// <summary>
        /// Retorna los datos de un trabajo con un Id determinado.
        /// </summary>
        /// <param name="id">Id del trabajo</param>
        /// <returns>Si el trabajo existe, retornará un status code 200 y sus datos, caso contrario
        /// retornará un error 500</returns>

        [Authorize(Policy = "AdminConsultor")]
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerTrabajoPorId([FromRoute] int id)
        {
            var resultado = await _unitOfWork.TrabajoRepository.ObtenerPorId(id);

            if (resultado == null)
            {
                return ResponseFactory.CreateErrorResponse(500, "No existe el trabajo");
            }
            else
            {
                var trabajo = new TrabajoDetalleDto()
                {
                    CodTrabajo = resultado.Id,
                    Fecha = resultado.Fecha,
                    CodProyecto = resultado.ProyectoId,
                    NombreProyecto = resultado.Proyecto.Nombre,
                    CodServicio = resultado.ServicioId,
                    DescripcionServicio = resultado.Servicio.Descripcion,
                    CantHoras = resultado.CantHoras,
                    ValorHora = resultado.ValorHora,
                    Costo = resultado.Costo,
                    Eliminado = resultado.Eliminado
                };

                return ResponseFactory.CreateSuccessResponse(200, trabajo);
            }
        }

        /// <summary>
        /// Da de alta un nuevo trabajo
        /// </summary>
        /// <param name="dto">Trabajo a registrar</param>
        /// <returns>Devuelve un status code 201 si se registró con éxito</returns>

        [Authorize(Policy = "Admin")]
        [HttpPost]
        public async Task<IActionResult> NuevoTrabajo(TrabajoNuevoDto dto)
        {
            var trabajo = await _unitOfWork.TrabajoRepository.NuevoTrabajo(dto);
            await _unitOfWork.TrabajoRepository.Insert(trabajo);
            await _unitOfWork.Complete();

            return ResponseFactory.CreateSuccessResponse(201, "Trabajo creado con éxito");
        }

        /// <summary>
        /// Actualiza un trabajo
        /// </summary>
        /// <param name="id">Id del trabajo a actualizar</param>
        /// <param name="dto">Propiedades del trabajo a actualizar</param>
        /// <returns>Devuelve un status code 200 si es que el trabajo pudo ser actualizado, de lo contrario
        /// devuelve un error 500.</returns>

        [Authorize(Policy = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarTrabajo([FromRoute] int id, TrabajoNuevoDto dto)
        {
            var resultado = await _unitOfWork.TrabajoRepository.Actualizar(dto, id);

            if (!resultado)
            {
                return ResponseFactory.CreateErrorResponse(500, "Hubo un error al actualizar el trabajo");
            }
            else
            {
                await _unitOfWork.Complete();
                return ResponseFactory.CreateSuccessResponse(200, "Trabajo actualizado con éxito");
            }
        }

        /// <summary>
        /// Elimina un trabajo (borrado lógico), actualizando el campo "Eliminado" y haciéndolo equivalente a 1
        /// </summary>
        /// <param name="id">Id del trabajo a eliminar</param>
        /// <returns>Devuelve un status code 200 en el caso de que pueda eliminarse, de lo contrario
        /// devuelve un error 500</returns>

        [Authorize(Policy = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarTrabajo([FromRoute] int id)
        {
            var resultado = await _unitOfWork.TrabajoRepository.Eliminar(id);

            if (!resultado)
            {
                return ResponseFactory.CreateErrorResponse(500, "Hubo un error al eliminar el trabajo");
            }
            else
            {
                await _unitOfWork.Complete();
                return ResponseFactory.CreateSuccessResponse(200, "Trabajo eliminado con éxito");
            }
        }

        /// <summary>
        /// Restaura un trabajo que ha sido borrado lógicamente, actualizando el campo "Eliminado" y volviéndolo
        /// equivalente a 0
        /// </summary>
        /// <param name="id">Id del trabajo a restaurar</param>
        /// <returns>Retorna un status code 200 si es que el trabajo pudo ser restaurado exitosamente, de lo contrario
        /// retorna un error 500</returns>

        [Authorize(Policy = "Admin")]
        [HttpPut("/api/Trabajo/Restaurar/{id}")]
        public async Task<IActionResult> RestaurarTrabajo([FromRoute] int id)
        {
            var resultado = await _unitOfWork.TrabajoRepository.Restaurar(id);

            if (!resultado)
            {
                return ResponseFactory.CreateErrorResponse(500, "Hubo un error al restaurar el trabajo");
            }
            else
            {
                await _unitOfWork.Complete();
                return ResponseFactory.CreateSuccessResponse(200, "Trabajo restaurado con éxito");
            }
        }

    }
}

