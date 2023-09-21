using System;
using IntegradorNET.DTOs;
using IntegradorNET.Helpers;
using IntegradorNET.Services;
using Microsoft.AspNetCore.Mvc;

namespace IntegradorNET.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class LoginController : ControllerBase
	{
		private TokenJWTHelper _tokenJWTHelper;
		private readonly IUnitOfWork _unitOfWork;

		public LoginController(IUnitOfWork unitOfWork, IConfiguration configuration)
		{
			_tokenJWTHelper = new TokenJWTHelper(configuration);
			_unitOfWork = unitOfWork;
		}

		[HttpPost]
		public async Task<IActionResult> Login(AuthenticateDto dto)
		{
			var userCredentials = await _unitOfWork.UsuarioRepository.AuthenticateCredentials(dto);
			if (userCredentials is null) return Unauthorized("Las credenciales son incorrectas o el usuario fue eliminado");

			var token = _tokenJWTHelper.GenerateToken(userCredentials);
			var usuario = new UsuarioLoginDto()
			{
				Nombre = userCredentials.Nombre,
				Dni = userCredentials.Dni,
				Email = userCredentials.Email,
				Tipo = new TipoUsuarioLoginDto
				{
					Id = (int)userCredentials.Tipo,
					Nombre = userCredentials.Tipo.ToString()
				},
				Token = token
			};

			return Ok(usuario);
		}
	}
}

