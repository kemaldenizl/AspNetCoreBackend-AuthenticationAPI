using Business.Abstract;
using Core.Utilities.Security.TokenCreators;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager:IAuthService
    {
        private IUserService _userService;
        private ITokenHelper<User,OperationClaim> _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper<User, OperationClaim> tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }
    }
}
