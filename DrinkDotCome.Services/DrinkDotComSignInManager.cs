using DrinkDotCom.Entities;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DrinkDotCom.Services
{ 
    public class DrinkDotComSignInManager : SignInManager<DrinkDotComUser, string>
    {
        public DrinkDotComSignInManager(DrinkDotComUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(DrinkDotComUser user)
        {
            return user.GenerateUserIdentityAsync((DrinkDotComUserManager)UserManager);
        }

        public static DrinkDotComSignInManager Create(IdentityFactoryOptions<DrinkDotComSignInManager> options, IOwinContext context)
        {
            return new DrinkDotComSignInManager(context.GetUserManager<DrinkDotComUserManager>(), context.Authentication);
        }
    }
}
