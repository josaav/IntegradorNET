using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntegradorNET.DTOs;
using IntegradorNET.Entities;
using IntegradorNET.Infrastructure;
using IntegradorNET.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        [Authorize(Policy = "Admin")]
        [HttpGet]
        public async Task<IActionResult> ObtenerUsuarios()
        {
            var usuarios = await _unitOfWork.UsuarioRepository.ObtenerTodos();

            return ResponseFactory.CreateSuccessResponse(200, usuarios);
        }

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

