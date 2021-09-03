using DrinkDotComDb;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkDotCom.Services 
{

    public class DrinkDotComRoleManager : RoleManager<IdentityRole>
    {

        public DrinkDotComRoleManager(IRoleStore<IdentityRole, string> roleStore) : base(roleStore)
        {
        }

        public static DrinkDotComRoleManager Create(IdentityFactoryOptions<DrinkDotComRoleManager> options, IOwinContext context)
        {
            return new DrinkDotComRoleManager(new RoleStore<IdentityRole>(context.Get<DrinkDotComContext>()));
        }
    }
    }
