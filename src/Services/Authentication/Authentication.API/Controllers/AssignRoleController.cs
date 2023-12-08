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
	public class AssignRoleController : ControllerBase
	{
		private readonly IAssignRoleService _assignRoleService;

		public AssignRoleController(IAssignRoleService assignRoleService)
		{
			_assignRoleService = assignRoleService;
		}

		[HttpPost("AssignRole")]
		[ProducesResponseType(typeof(ResponseDataTransferObject), (int)HttpStatusCode.OK)]
		[ProducesResponseType(typeof(ResponseDataTransferObject), (int)HttpStatusCode.BadRequest)]
		public async Task<IActionResult> Register([FromBody] AssignRoleRequestDataTransferObject assignRoleRequestDataTransferObject)
		{
			try
			{
				await _assignRoleService.Execute(
					assignRoleRequestDataTransferObject.Email,
					assignRoleRequestDataTransferObject.Role.ToUpper());
			}
			catch (Exception ex)
			{
				ResponseDataTransferObject response = new(
					false,
					ex.Message);

				return BadRequest(response);
			}

			ResponseDataTransferObject respone = new();

			return Ok(respone);
		}
	}
}
