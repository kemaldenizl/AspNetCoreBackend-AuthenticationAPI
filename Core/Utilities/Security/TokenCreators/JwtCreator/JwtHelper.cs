using Core.Entities.Abstract;
using Core.Utilities.Security.TokenEntities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
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

            
        }

        public AccessToken CreateToken(TUser user, List<TOperationClaim> operationClaims)
        {
            throw new NotImplementedException();
        }
    }
}
