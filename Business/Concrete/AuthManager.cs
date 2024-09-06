using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Security.TokenCreators;
using Core.Utilities.Security.TokenEntities;
using Entities.Concrete;
using Entities.Dtos;
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

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            throw new NotImplementedException();
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            throw new NotImplementedException();
        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto)
        {
            throw new NotImplementedException();
        }

        public IResult UserExist(string email)
        {
            throw new NotImplementedException();
        }
    }
}
