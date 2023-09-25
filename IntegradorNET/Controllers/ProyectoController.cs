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
    public class ProyectoController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProyectoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Devuelve todos los proyectos con un paginado. Toma dos parámetros opcionales de la ruta,
        /// con los cuales realiza un paginado, "items" (cantidad de items por página) y "pagina"
        /// (número de página). De no ser enviados dichos parámetros, la página que muestra primero
        /// es la primera, y hay 10 ítems por página.
        /// </summary>
        /// <returns>Retorna todos los proyectos</returns>

        [Authorize(Policy = "AdminConsultor")]
        [HttpGet]
        public async Task<IActionResult> ObtenerProyectos()
        {
            var proyectos = await _unitOfWork.ProyectoRepository.ObtenerTodos();

            List<ProyectoDetalleDto> lista = proyectos.Select(entity => new ProyectoDetalleDto
            {
                CodProyecto = entity.Id,
                Nombre = entity.Nombre,
                Direccion = entity.Direccion,
                Estado = new EstadoProyectoDto
                {
                    Id = (int)entity.Estado,
                    Nombre = entity.Estado.ToString()
                }
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
        /// Devuelve todos los proyectos con un estado determinado.
        /// Toma dos parámetros opcionales de la ruta, con los cuales realiza un paginado: "items" (cantidad de items
        /// por página) y "pagina" (número de página). De no ser enviados dichos parámetros, la página que muestra primero
        /// es la primera, y hay 10 ítems por página.
        /// <param name="id">Id del estado</param>
        /// </summary>
        /// <returns>Retorna todos los proyectos</returns>

        [Authorize(Policy = "AdminConsultor")]
        [HttpGet("/api/Proyecto/Estado/{id}")]
        public async Task<IActionResult> ObtenerProyectosPorEstado([FromRoute] int id)
        {
            var proyectos = await _unitOfWork.ProyectoRepository.ObtenerProyectosPorEstado(id);

            List<ProyectoDetalleDto> lista = proyectos.Select(entity => new ProyectoDetalleDto
            {
                CodProyecto = entity.Id,
                Nombre = entity.Nombre,
                Direccion = entity.Direccion,
                Estado = new EstadoProyectoDto
                {
                    Id = (int)entity.Estado,
                    Nombre = entity.Estado.ToString()
                },
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
        /// Retorna los datos de un proyecto con un Id determinado.
        /// </summary>
        /// <param name="id">Id del proyecto</param>
        /// <returns>Si el proyecto existe, retornará un status code 200 y sus datos, caso contrario
        /// retornará un error 500</returns>

        [Authorize(Policy = "AdminConsultor")]
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerProyectoPorId([FromRoute] int id)
        {
            var resultado = await _unitOfWork.ProyectoRepository.ObtenerPorId(id);

            if (resultado == null)
            {
                return ResponseFactory.CreateErrorResponse(500, "No existe el proyecto");
            }
            else
            {
                var proyecto = new ProyectoDetalleDto()
                {
                    CodProyecto = resultado.Id,
                    Nombre = resultado.Nombre,
                    Direccion = resultado.Direccion,
                    Estado = new EstadoProyectoDto
                    {
                        Id = (int)resultado.Estado,
                        Nombre = resultado.Estado.ToString()
                    },
                    Eliminado = resultado.Eliminado
                };

                return ResponseFactory.CreateSuccessResponse(200, proyecto);
            }
        }

        /// <summary>
        /// Da de alta un nuevo proyecto
        /// </summary>
        /// <param name="dto">Proyecto a registrar</param>
        /// <returns>Devuelve un status code 201 si se registró con éxito</returns>

        [Authorize(Policy = "Admin")]
        [HttpPost]
        public async Task<IActionResult> NuevoProyecto(ProyectoNuevoDto dto)
        {
            var proyecto = new Proyecto(dto);
            await _unitOfWork.ProyectoRepository.Insert(proyecto);
            await _unitOfWork.Complete();

            return ResponseFactory.CreateSuccessResponse(201, "Proyecto creado con éxito");
        }

        /// <summary>
        /// Actualiza un proyecto
        /// </summary>
        /// <param name="id">Id del proyecto a actualizar</param>
        /// <param name="dto">Propiedades del proyecto a actualizar</param>
        /// <returns>Devuelve un status code 200 si es que el proyecto pudo ser actualizado, de lo contrario
        /// devuelve un error 500.</returns>

        [Authorize(Policy = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarProyecto([FromRoute] int id, ProyectoNuevoDto dto)
        {
            var resultado = await _unitOfWork.ProyectoRepository.Actualizar(new Proyecto(dto, id));

            if (!resultado)
            {
                return ResponseFactory.CreateErrorResponse(500, "Hubo un error al actualizar el proyecto");
            }
            else
            {
                await _unitOfWork.Complete();
                return ResponseFactory.CreateSuccessResponse(200, "Proyecto actualizado con éxito");
            }
        }

        /// <summary>
        /// Elimina un proyecto (borrado lógico), actualizando el campo "Eliminado" y haciéndolo equivalente a 1
        /// </summary>
        /// <param name="id">Id del proyecto a eliminar</param>
        /// <returns>Devuelve un status code 200 en el caso de que pueda eliminarse, de lo contrario
        /// devuelve un error 500</returns>

        [Authorize(Policy = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarProyecto([FromRoute] int id)
        {
            var resultado = await _unitOfWork.ProyectoRepository.Eliminar(id);

            if (!resultado)
            {
                return ResponseFactory.CreateErrorResponse(500, "Hubo un error al eliminar el proyecto");
            }
            else
            {
                await _unitOfWork.Complete();
                return ResponseFactory.CreateSuccessResponse(200, "Proyecto eliminado con éxito");
            }
        }

        /// <summary>
        /// Restaura un proyecto que ha sido borrado lógicamente, actualizando el campo "Eliminado" y volviéndolo
        /// equivalente a 0
        /// </summary>
        /// <param name="id">Id del proyecto a restaurar</param>
        /// <returns>Retorna un status code 200 si es que el proyecto pudo ser restaurado exitosamente, de lo contrario
        /// retorna un error 500</returns>

        [Authorize(Policy = "Admin")]
        [HttpPut("/api/Proyecto/Restaurar/{id}")]
        public async Task<IActionResult> RestaurarProyecto([FromRoute] int id)
        {
            var resultado = await _unitOfWork.ProyectoRepository.Restaurar(id);

            if (!resultado)
            {
                return ResponseFactory.CreateErrorResponse(500, "Hubo un error al restaurar el proyecto");
            }
            else
            {
                await _unitOfWork.Complete();
                return ResponseFactory.CreateSuccessResponse(200, "Proyecto restaurado con éxito");
            }
        }

    }
}

