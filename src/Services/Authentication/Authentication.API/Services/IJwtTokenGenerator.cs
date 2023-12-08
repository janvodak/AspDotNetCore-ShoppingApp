using ShoppingApp.Services.Authentication.API.Models;

namespace ShoppingApp.Services.Authentication.API.Services
{
	public interface IJwtTokenGenerator
	{
		JwtToken Generate(AuthenticationUser authenticationUser, IEnumerable<string> roles);	
	}
}
