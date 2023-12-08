using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ShoppingApp.Services.Authentication.API.Models;

namespace ShoppingApp.Services.Authentication.API.Services
{
	public class JwtTokenGenerator : IJwtTokenGenerator
	{
		private const string SECURITY_ALGORITHM = SecurityAlgorithms.HmacSha256Signature;

		private readonly JwtOptions _jwtOptions;

		public JwtTokenGenerator(IOptions<JwtOptions> jwtOptions)
		{
			_jwtOptions = jwtOptions.Value;
		}

		public JwtToken Generate(AuthenticationUser authenticationUser, IEnumerable<string> roles)
		{
			SymmetricSecurityKey symmetricSecurityKey = new(Encoding.ASCII.GetBytes(_jwtOptions.Secret));

			SigningCredentials signingCredentials = new(
				symmetricSecurityKey,
				SECURITY_ALGORITHM);

			List<Claim> claimList = new()
			{
				new Claim(JwtRegisteredClaimNames.Email, authenticationUser.Email ?? ""),
				new Claim(JwtRegisteredClaimNames.Sub, authenticationUser.Id),
				new Claim(JwtRegisteredClaimNames.Name, authenticationUser.UserName ?? "")
			};

			claimList.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

			SecurityTokenDescriptor tokenDescriptor = new()
			{
				Audience = _jwtOptions.Audience,
				Issuer = _jwtOptions.Issuer,
				Subject = new ClaimsIdentity(claimList),
				Expires = DateTime.UtcNow.AddSeconds(_jwtOptions.Expiration),
				SigningCredentials = signingCredentials
			};

			JwtSecurityTokenHandler tokenHandler = new();
			SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

			return new(tokenHandler.WriteToken(token));
		}
	}
}
