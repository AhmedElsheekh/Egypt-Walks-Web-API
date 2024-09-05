
using AutoMapper;
using EgyptWalks.API.Helper;
using EgyptWalks.API.MiddleWares;
using EgyptWalks.Core;
using EgyptWalks.Core.Models.Identity;
using EgyptWalks.Core.Repositories;
using EgyptWalks.Core.Services;
using EgyptWalks.Repository;
using EgyptWalks.Repository.Data;
using EgyptWalks.Repository.Identity;
using EgyptWalks.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;

namespace EgyptWalks.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            var logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("Logs/EgyptWalks_Log.txt", rollingInterval:RollingInterval.Minute)
                .MinimumLevel.Error()
                .CreateLogger();

            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Egypt Walks API", Version = "v1" });

                // Add security definition for Bearer (JWT) tokens
                c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });

                // Add a requirement for Bearer token in all controllers
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            },
                            Scheme = "Oauth2",
                            Name = JwtBearerDefaults.AuthenticationScheme,
                            In = ParameterLocation.Header
                        },
                        new string[] {}
                    }
                });
            });
       

            builder.Services.AddDbContext<EgyptWalksDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("EgyptWalksConnectionString"));
            });

            builder.Services.AddDbContext<EgyptWalksAuthDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("EgyptWalksAuthConnectionString"));
            });

            builder.Services.AddScoped<IUnitOfWork, SQLUnitOfWork>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            //builder.Services.AddScoped<IImageRepository>(options =>
            //{
            //    var env = options.GetRequiredService<IWebHostEnvironment>();
            //    string imagePath = Path.Combine(env.WebRootPath, "Files", "Images");
            //    var dbContext = options.GetRequiredService<EgyptWalksDbContext>();
            //    var httpContextAccessor = options.GetRequiredService<IHttpContextAccessor>();
            //    return new LocalImageRepository(imagePath, httpContextAccessor, dbContext);
            //});

            builder.Services.AddIdentityCore<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>("EgyptWalks")
                .AddEntityFrameworkStores<EgyptWalksAuthDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 6;
                
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            #region Seed Database
            using(var scope = app.Services.CreateScope())
            {
                var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();

                try
                {
                    var egyptWalksDbContext = scope.ServiceProvider.GetRequiredService<EgyptWalksDbContext>();
                    await egyptWalksDbContext.Database.MigrateAsync();
                    await EgyptWalksDbContextSeed.SeedDataAsync(egyptWalksDbContext);

                    var egyptWalksAuthDbContext = scope.ServiceProvider.GetRequiredService<EgyptWalksAuthDbContext>();
                    await egyptWalksAuthDbContext.Database.MigrateAsync();
                    //var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                    await EgyptWalksAuthDbContextSeed.SeedDataAsync(egyptWalksAuthDbContext);

                }
                catch(Exception ex)
                {
                    var currentLogger = loggerFactory.CreateLogger<Program>();
                    currentLogger.LogError(ex, "Error While Seeding Database");
                }
                
            }
            #endregion

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", "Images")),
                RequestPath = "/wwwroot/Files/Images"

            });


            app.MapControllers();

            app.Run();
        }

    
    }
}
