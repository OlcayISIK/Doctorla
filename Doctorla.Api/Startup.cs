using Doctorla.Api.Helpers;
using Doctorla.Business.Helpers;
using Doctorla.Core;
using Doctorla.Core.Enums;
using Doctorla.Core.InternalDtos;
using Doctorla.Data;
using Doctorla.Data.EF;
using Doctorla.Data.Entities;
using Doctorla.Data.Members;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Constants = Doctorla.Core.Constants;

namespace Doctorla.Api
{
    public class Startup
    {
        private AppSettings _appSettings;

        /// 
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// 
        public IConfiguration Configuration { get; }

        /// This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // handle settings from appsettings.json
            _appSettings = new AppSettings();
            Configuration.Bind(_appSettings);
            services.AddSingleton(_appSettings);

            PagingExtensions.InjectAppSettings(_appSettings);

            // handle database connection
            services.AddDbContext<Context>(options => { options.UseSqlServer(_appSettings.DatabaseConnectionString, x => x.MigrationsAssembly("Doctorla.Data.EF")); });
            services.AddTransient(typeof(CustomMapper));
            services.AddAutoMapper(typeof(AutoMapperMappingProfile));

            // this is added for getting user claims from token when checking permissions.
            // it says this is removed from default services since it creates a performance issue.
            // if that would be the case,this could be removed and claims(or other info) could be
            // obtained at controller and passed down all the way to the permission.
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpContextAccessor();
            services.AddCors();
            services.AddSignalR();
            // handle authentication
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(Constants.AuthenticationSchemes.User, x =>
            {          
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.TokenOptions.UserSecretKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            }).AddJwtBearer(Constants.AuthenticationSchemes.Doctor, x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.TokenOptions.DoctorSecretKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            }).AddJwtBearer(Constants.AuthenticationSchemes.Hospital, x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.TokenOptions.HospitalSecretKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            }).AddJwtBearer(Constants.AuthenticationSchemes.Admin, x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.TokenOptions.AdminSecretKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddAuthorization();

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc(Constants.AuthenticationSchemes.User, new OpenApiInfo { Title = "Doctorla User Api", Version = "v1" });
                x.SwaggerDoc(Constants.AuthenticationSchemes.Doctor, new OpenApiInfo { Title = "Doctorla Doctor Api", Version = "v1" });
                x.SwaggerDoc(Constants.AuthenticationSchemes.Admin, new OpenApiInfo { Title = "Doctorla Admin Api", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //x.IncludeXmlComments(xmlPath);
                x.DocumentFilter<SwaggerAddEnumDescriptions>();

            });

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Doctorla.Api", Version = "v1" });
            });

            services.AddDistributedRedisCache(options => { options.Configuration = _appSettings.RedisConnectionString; });
            ConfigureDependencies(services);

        }

        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Doctorla.Api v1"));
            }

            if (env.IsProduction())
            {
                ServiceLocator.SetProductionEnvironment();
            }

            //app.UseHttpsRedirection();
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware(typeof(RequestRewindMiddleware));
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseWebSockets();
            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapHub<RefreshHub>("/refreshhub");
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(endpoints =>
            {
                endpoints.SwaggerEndpoint($"/swagger/{Constants.AuthenticationSchemes.User}/swagger.json", "Doctorla User API");
                endpoints.SwaggerEndpoint($"/swagger/{Constants.AuthenticationSchemes.Doctor}/swagger.json", "Doctorla Doctor API");
                endpoints.SwaggerEndpoint($"/swagger/{Constants.AuthenticationSchemes.Admin}/swagger.json", "Doctorla Admin API");
                endpoints.RoutePrefix = string.Empty;
            });

            ServiceLocator.Initialize(app.ApplicationServices, _appSettings);
            UpdateDatabase(_appSettings.DatabaseConnectionString);
            CreateFileSystemDirectories();
        }

        /// <summary>
        /// Registers all types to be used in dependency injection
        /// </summary>
        private void ConfigureDependencies(IServiceCollection services)
        {
            AutomaticInjection(services, "Doctorla.Repository");
            AutomaticInjection(services, "Doctorla.Business");
        }

        /// <summary>
        /// Searches given assembly and automatically injects found dependencies.
        /// </summary>
        private void AutomaticInjection(IServiceCollection services, string assemblyName)
        {
            var types = Assembly.Load(assemblyName).DefinedTypes.ToList();
            foreach (var type in types)
            {
                if (type.IsGenericType)
                    continue;
                var implementedInterfaces = type.ImplementedInterfaces.ToList();
                if (implementedInterfaces.Count != 0)
                {
                    foreach (var implementedInterface in implementedInterfaces)
                    {
                        if (implementedInterface.Name[0] == 'I' && type.Name.StartsWith(implementedInterface.Name.Substring(1)))
                        {
                            services.AddScoped(Type.GetType(implementedInterface + ", " + assemblyName),
                                Type.GetType(type + ", " + assemblyName));
                            break;
                        }
                    }
                }
            }
        }

        private void UpdateDatabase(string connectionString)
        {
            var builder = new DbContextOptionsBuilder<Context>();
            builder.UseSqlServer(connectionString);
            using var context = new Context(builder.Options);
            context.Database.Migrate();
            var firstAdminExists = context.Admins.Any();
            if (!firstAdminExists)
            {
                context.Admins.Add(new Admin
                {
                    Email = "admin",
                    HashedPassword = new CustomPasswordHasher().HashPassword("testpass"),
                    AccountStatus = AccountStatus.Approved
                });
                context.SaveChanges();
            }
        }

        private void CreateFileSystemDirectories()
        {
            Directory.CreateDirectory(Path.Combine(_appSettings.FileSystemImageBasePath));
            Directory.CreateDirectory(Path.Combine(_appSettings.FileSystemAudioBasePath));
        }
    }
}
