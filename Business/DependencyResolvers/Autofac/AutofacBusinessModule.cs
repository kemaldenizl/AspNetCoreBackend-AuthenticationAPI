using Autofac;
using Business.Abstract;
using Business.Concrete;
using Core.Utilities.Security.TokenCreators;
using Core.Utilities.Security.TokenCreators.JwtCreator;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<JwtHelper<User,OperationClaim>>().As<ITokenHelper<User,OperationClaim>>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();
            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<AuthManager>().As<IAuthService>();
        }
    }
}
