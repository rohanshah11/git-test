using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using CMS.Core.Data;
using CMS.Core.Entity;
using CMS.Core.Helper;
using CMS.Core.Makers.Implementations;
using CMS.Core.Makers.Interface;
using CMS.Core.Repository.Interface;
using CMS.Core.Repository.Repo;
using CMS.Core.Service.Implementation;
using CMS.Core.Service.Interface;
using CMS.User.Data;
using CMS.User.Library;
using CMS.User.Makers.Implementations;
using CMS.User.Makers.Interface;
using CMS.User.Repository.Interface;
using CMS.User.Repository.Repo;
using CMS.User.Service.Implementations;
using CMS.User.Service.Interface;
using CMS.Web.Helpers;
using CMS.Web.LEPagination;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace CMS.Web
{
    public class Startup
    {
        public IContainer ApplicationContainer { get; private set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<UserDbContext>(options =>
            {
                options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("CMS.User"));
                options.ConfigureWarnings(x => x.Ignore(RelationalEventId.AmbientTransactionWarning));
            });

            services.AddDbContext<AppDbContext>(options => options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("CMS.Core")));

            register(services);
            services.AddAuthentication("userDetails")
               .AddCookie("userDetails", options =>
               {
                   // Cookie settings
                   options.Cookie.HttpOnly = false;
                   //  options.Cookie.Expiration = TimeSpan.FromDays(30);
                   options.LoginPath = "/account/login";
                   options.LogoutPath = "/account/logout";
                   options.AccessDeniedPath = "/error";

               });
            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");
            services.AddMvc(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());

                options.Filters.Add(new RequireHttpsAttribute());

            }).AddControllersAsServices()
              .AddJsonOptions(jsonOptions =>
              {
                  jsonOptions.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;

              }).AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization(options => options.DataAnnotationLocalizerProvider = (t, f) => f.Create(typeof(SharedResource)));

            services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });

            Mapper.Initialize(x =>
            {
                x.AddProfile<Areas.Core.AutomapperProfiles.DomainProfile>();
                x.AddProfile<Areas.User_management.AutomapperProfiles.DomainProfile>();
                x.AddProfile<CMS.Web.AutomapperProfiles.DomainProfile>();
            });

            services.AddAutoMapper();
            services.AddResponseCaching();
            //for compressing data
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "imagejpeg", "png", "jpeg", "jpg" });
            });
            services.AddSession();

            services.Configure<GzipCompressionProviderOptions>(option =>
            option.Level = System.IO.Compression.CompressionLevel.Optimal);

            services.Configure<RequestLocalizationOptions>(
      opts =>
      {
          var supportedCultures = new List<CultureInfo>
          {

                new CultureInfo("en"),
                new CultureInfo("ne-NP"),
          };

          opts.DefaultRequestCulture = new RequestCulture("en");
          // Formatting numbers, dates, etc.
          opts.SupportedCultures = supportedCultures;
          // UI strings that we have localized.
          opts.SupportedUICultures = supportedCultures;
      });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }
            else
            {
                app.UseExceptionHandler("/Error/{0}");
                app.UseHsts();
            }
            app.UseStatusCodePagesWithReExecute("/error/{0}");

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });


            app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions
            {
                ServeUnknownFileTypes = true
            });
            app.UseResponseCaching();
            app.UseAuthentication();
            app.UseResponseCompression();

            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "areaRoute",
                    template: "{area:exists}/{controller=Page}/{action=Index}/{id?}");

            });
        }


        private void register(IServiceCollection services)
        {
            services.Scan(scan => scan
               .FromAssembliesOf(typeof(PageMakerImpl), typeof(UserMaker))
               .AddClasses()
               .AsSelf()
                .AsImplementedInterfaces()
               .WithScopedLifetime());

            services.AddScoped(typeof(User.Repository.Interface.BaseRepository<>), typeof(User.Repository.Repo.BaseRepositoryImpl<>));
            services.AddScoped<PaginatedMetaService,PaginatedMetaServiceImpl>();
            services.AddScoped<FileHelper, FileHelperImpl>();


        }

     

    }
}
