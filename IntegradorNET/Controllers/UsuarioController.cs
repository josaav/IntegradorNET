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
    public class UsuarioController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsuarioController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Devuelve todos los usuarios
        /// </summary>
        /// <returns>Retorna todos los usuarios</returns>
        
        [Authorize(Policy = "AdminConsultor")]
        [HttpGet]
        public async Task<IActionResult> ObtenerUsuarios()
        {
            var usuarios = await _unitOfWork.UsuarioRepository.ObtenerTodos();

            List<UsuarioListaDto> lista = usuarios.Select(entity => new UsuarioListaDto
            {
                codUsuario = entity.Id,
                Nombre = entity.Nombre,
                Dni = entity.Dni,
                Email = entity.Email,
                Tipo = new TipoUsuarioDto
                {
                    Id = (int)entity.Tipo,
                    Nombre = entity.Tipo.ToString()
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
        /// Retorna los datos de un usuario con un Id determinado.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Si el usuario existe, retornará un status code 200 y sus datos, caso contrario
        /// retornará un error 500</returns>
     
        [Authorize(Policy = "AdminConsultor")]
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerUsuarioPorId([FromRoute] int id)
        {
            var resultado = await _unitOfWork.UsuarioRepository.ObtenerPorId(id);

            if (resultado == null)
            {
                return ResponseFactory.CreateErrorResponse(500, "No existe el usuario");
            }
            else
            {
                var usuario = new UsuarioListaDto()
                {
                    codUsuario = resultado.Id,
                    Nombre = resultado.Nombre,
                    Dni = resultado.Dni,
                    Email = resultado.Email,
                    Tipo = new TipoUsuarioDto
                    {
                        Id = (int)resultado.Tipo,
                        Nombre = resultado.Tipo.ToString()
                    }
                };

                return ResponseFactory.CreateSuccessResponse(200, usuario);
            }
        }


        /// <summary>
        /// Registra el usuario
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>Devuelve un status code 201 si se registró con éxito, status code 409 si es que ya
        /// existe un usuario registrado con el mismo email</returns>

        [Authorize(Policy = "Admin")]
        [HttpPost]
        public async Task<IActionResult> RegistrarUsuario(UsuarioRegistroDto dto)
        {
            if (await _unitOfWork.UsuarioRepository.UsuarioExiste(dto.Email))
                return ResponseFactory.CreateErrorResponse(409, "Ya existe un usuario registrado con ese email");

            var usuario = new Usuario(dto);
            await _unitOfWork.UsuarioRepository.Insert(usuario);
            await _unitOfWork.Complete();

            return ResponseFactory.CreateSuccessResponse(201, "Usuario registrado con éxito");
        }

        /// <summary>
        /// Actualiza un usuario
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns>Devuelve un status code 200 si es que el usuario pudo ser actualizado, de lo contrario
        /// devuelve un error 500.</returns>

        [Authorize(Policy = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarUsuario([FromRoute] int id, UsuarioRegistroDto dto)
        {
            var resultado = await _unitOfWork.UsuarioRepository.Actualizar(new Usuario(dto, id));

            if (!resultado)
            {
                return ResponseFactory.CreateErrorResponse(500, "Hubo un error al actualizar el usuario");
            }
            else
            {
                await _unitOfWork.Complete();
                return ResponseFactory.CreateSuccessResponse(200, "Usuario actualizado con éxito");
            }
        }

        /// <summary>
        /// Elimina un usuario (borrado lógico), actualizando el campo "Eliminado" y haciéndolo equivalente a 1
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Devuelve un status code 200 en el caso de que pueda eliminarse, de lo contrario
        /// devuelve un error 500</returns>

        [Authorize(Policy = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarUsuario([FromRoute] int id)
        {
            var resultado = await _unitOfWork.UsuarioRepository.Eliminar(id);

            if (!resultado)
            {
                return ResponseFactory.CreateErrorResponse(500, "Hubo un error al eliminar el usuario");
            }
            else
            {
                await _unitOfWork.Complete();
                return ResponseFactory.CreateSuccessResponse(200, "Usuario eliminado con éxito");
            }

        }

        /// <summary>
        /// Restaura un usuario que ha sido borrado lógicamente, actualizando el campo "Eliminado" y volviéndolo
        /// equivalente a 0
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna un status code 200 si es que el usuario pudo ser restaurado exitosamente, de lo contrario
        /// retorna un error 500</returns>

        [Authorize(Policy = "Admin")]
        [HttpPut("/Restaurar/{id}")]
        public async Task<IActionResult> RestaurarUsuario([FromRoute] int id)
        {
            var resultado = await _unitOfWork.UsuarioRepository.Restaurar(id);

            if (!resultado)
            {
                return ResponseFactory.CreateErrorResponse(500, "Hubo un error al restaurar el usuario");
            }
            else
            {
                await _unitOfWork.Complete();
                return ResponseFactory.CreateSuccessResponse(200, "Usuario restaurado con éxito");
            }

        }



    }
}

