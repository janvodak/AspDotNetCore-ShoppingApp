namespace ShoppingApp.Services.Authentication.API.Services
{
	public interface IAssignRoleService
	{
		Task Execute(string email, string roleName);
	}
}
