﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialInitiatives2.Models;
using AutoMapper;

namespace SocialInitiatives2
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public Startup(IConfiguration configuration) => Configuration = configuration;
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppIdentityDbContext>(options =>
                                        options.UseSqlServer(
                                        Configuration["Data:IdentityDb:ConnectionString"]));
            services.AddDbContext<InitiativeDbContext>(options => options.UseSqlServer(Configuration["Data:InitiativesDb:ConnectionString"]));

            services.AddIdentity<AppUser, IdentityRole>(options => 
                                        {
                                            options.User.RequireUniqueEmail = true;
                                            options.Password.RequiredLength = 8;
                                        })
                                        .AddEntityFrameworkStores<AppIdentityDbContext>()
                                        .AddDefaultTokenProviders();
            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
            services.AddAutoMapper();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseSession();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
