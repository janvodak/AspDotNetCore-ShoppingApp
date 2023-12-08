using System.Net;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Services.Authentication.API.Models.DataTransferObjects;
using ShoppingApp.Services.Authentication.API.Models.DataTransferObjects.Requests;
using ShoppingApp.Services.Authentication.API.Models.DataTransferObjects.Responses;
using ShoppingApp.Services.Authentication.API.Services;

namespace ShoppingApp.Services.Authentication.API.Controllers
{
	[ApiController]
	[Route("api/v1/Authentication")]
	[Produces("application/json")]
	public class RegistrationController : ControllerBase
	{
		private readonly IRegistrationService _registrationService;

		public RegistrationController(IRegistrationService registrationService)
		{
			_registrationService = registrationService;
		}

		[HttpPost("Registration")]
		[ProducesResponseType(typeof(ResponseDataTransferObject), (int)HttpStatusCode.OK)]
		[ProducesResponseType(typeof(ResponseDataTransferObject), (int)HttpStatusCode.BadRequest)]
		public async Task<IActionResult> Register([FromBody] RegistrationRequestDataTransferObject registrationRequestDataTransferObject)
		{
			UserDataTransferObject userDataTransferObject;

			try
			{
				userDataTransferObject = await _registrationService.Execute(registrationRequestDataTransferObject);
			}
			catch (Exception ex)
			{
				ResponseDataTransferObject response = new(
					false,
					ex.Message);

				return BadRequest(response);
			}

			ResponseDataTransferObject respone = new()
			{
				Result = userDataTransferObject
			};

			return Ok(respone);
		}
	}
}
