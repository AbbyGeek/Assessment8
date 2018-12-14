﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.Cookies;

[assembly: OwinStartup(typeof(Assessment7.Startup1))]

namespace Assessment7
{
    public class Startup1
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            const string connectionString = @"Data Source=(LocalDb)\MSSQLLocalDb;Initial Catalog=IdentityUsersEntities;Integrated Security=True;Pooling=False";

            //instance of db context
            app.CreatePerOwinContext(() => new IdentityDbContext(connectionString));

            //pass that db context to the user store
            app.CreatePerOwinContext<UserStore<IdentityUser>>((opt, cont) => new UserStore<IdentityUser>(cont.Get<IdentityDbContext>()));

            //pass the user store to the dbmanager
            app.CreatePerOwinContext<UserManager<IdentityUser>>((opt, cont) => new UserManager<IdentityUser>(cont.Get<UserStore<IdentityUser>>()));

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new Microsoft.Owin.PathString("/Identity/Login"),

            });
        }
    }
}

