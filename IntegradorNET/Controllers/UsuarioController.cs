using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntegradorNET.Entities;
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
        public async Task<ActionResult<IEnumerable<Usuario>>> ObtenerUsuarios()
        {
            var usuarios = await _unitOfWork.UsuarioRepository.ObtenerTodos();

            return usuarios;
        }
    }
}

