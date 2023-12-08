namespace ShoppingApp.Services.Authentication.API.Models.DataTransferObjects.Requests
{
	public class AssignRoleRequestDataTransferObject
	{
		public AssignRoleRequestDataTransferObject(
			string email,
			string role)
		{
			Email = email;
			Role = role;
		}

		public string Email { get; set; }
		public string Role { get; set; }
	}
}
