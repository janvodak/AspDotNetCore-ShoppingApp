using System.Net;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Services.Authentication.API.Models.DataTransferObjects.Requests;
using ShoppingApp.Services.Authentication.API.Models.DataTransferObjects.Responses;
using ShoppingApp.Services.Authentication.API.Services;

namespace ShoppingApp.Services.Authentication.API.Controllers
{
	[ApiController]
	[Route("api/v1/Authentication")]
	[Produces("application/json")]
	public class LoginController : ControllerBase
	{
		private readonly ILoginService _loginService;

		public LoginController(ILoginService loginService)
		{
			_loginService = loginService;
		}

		[HttpPost("Login")]
		[ProducesResponseType(typeof(ResponseDataTransferObject), (int)HttpStatusCode.OK)]
		[ProducesResponseType(typeof(ResponseDataTransferObject), (int)HttpStatusCode.BadRequest)]
		public async Task<IActionResult> Register([FromBody] LoginRequestDataTransferObject registrationRequestDataTransferObject)
		{
			LoginResponseDataTransferObject loginResponseDataTransferObject;

			try
			{
				loginResponseDataTransferObject = await _loginService.Execute(registrationRequestDataTransferObject);
			}
			catch (Exception ex)
			{
				ResponseDataTransferObject response = new(
					false,
					ex.Message);

				return BadRequest(response);
			}

			if (loginResponseDataTransferObject.User == null)
			{
				ResponseDataTransferObject response = new(
					false,
					"Username or password is incorrect.");

				return BadRequest(response);
			}

			ResponseDataTransferObject respone = new()
			{
				Result = loginResponseDataTransferObject
			};

			return Ok(respone);
		}
	}
}
