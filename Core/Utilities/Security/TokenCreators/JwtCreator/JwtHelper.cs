using Core.Entities.Abstract;
using Core.Extensions;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.TokenEntities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.TokenCreators.JwtCreator
{
    public class JwtHelper<TUser, TOperationClaim> : ITokenHelper<TUser, TOperationClaim>
        where TUser : class, IEntity, new()
        where TOperationClaim : class, IEntity, new()
    {
        public IConfiguration Configuration { get; }
        private TokenOptions _tokenOptions;
        private DateTime _accessTokenExpiration;
        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
        }

        private IEnumerable<Claim> SetClaims(TUser user, List<TOperationClaim> operationClaims)
        {
            var id = typeof(TUser).GetProperty("Id")?.GetValue(user)?.ToString();
            var email = typeof(TUser).GetProperty("Email")?.GetValue(user)?.ToString();
            var firstname = typeof(TUser).GetProperty("FirstName")?.GetValue(user)?.ToString();
            var lastname = typeof(TUser).GetProperty("LastName")?.GetValue(user)?.ToString();

            var claims = new List<Claim>();
            claims.AddNameIdentifier(id);
            claims.AddEmail(email);
            claims.AddName($"{firstname} {lastname}");
            claims.AddRoles(operationClaims.Select(c => typeof(TOperationClaim).GetProperty("Name")?.GetValue(c)?.ToString()).ToArray());

            return claims;
        }

        private JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, TUser user, SigningCredentials signingCredentials, List<TOperationClaim> operationClaims)
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(user, operationClaims),
                signingCredentials: signingCredentials
            );
            return jwt;
        }

        public AccessToken CreateToken(TUser user, List<TOperationClaim> operationClaims)
        {
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };
        }
    }
}
