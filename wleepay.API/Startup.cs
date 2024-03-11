using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Text;
using wleepay.Data.Interface;
using wleepay.Data.Context;
using wleepay.Services;
using wleepay.Communication.Services;
using wleepay.Communication.Interface;
using Microsoft.Extensions.Logging;
using wleepay.Storage.Interface;
using wleepay.Storage.Services;
using wleepay.Core;
using wleepay.Data;
using wleepay.Core.Services;
using System;
using wleepay.Core.Models;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity.UI.Services;
using IEmailSender = wleepay.Communication.Interface.IEmailSender;
using Microsoft.AspNet.Identity;
using wleepay.Core.Repositories;
using wleepay.Data.Repositories;
using wleepay.API.Hub;
using wleepay.API.Middleware;

namespace wleepay.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));
            services.AddDbContext<WPContext>(options => options.UseSqlServer(Configuration.GetConnectionString("WleePayDataBase")));

            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.Zero;
                options.Lockout.MaxFailedAccessAttempts = 3;
            })
                   .AddEntityFrameworkStores<WPContext>()
                   .AddDefaultTokenProviders();

            services.AddSession(options =>
            {
                // Set session timeout to 30 minutes
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true; // Make the session cookie essential
            });

            services.AddDistributedMemoryCache(); // Register the distributed memory cache

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            // Adding Jwt Bearer  
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    //ValidateIssuer = false,
                    //ValidateAudience = false,
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    RequireSignedTokens = true
                };

            });

            services.AddSignalR();

            services.AddCors(options => {
                options.AddPolicy("AllowAllHeaders",
                    builder =>  builder.AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .SetIsOriginAllowed((hosts) => true));
        });

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;

                // Lockout settings
              
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.Zero;
                options.Lockout.MaxFailedAccessAttempts = 3;

                // User settings
                options.User.RequireUniqueEmail = false;
                options.SignIn.RequireConfirmedAccount = false;

            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Register services
            services.AddTransient<ICountyService, wleepay.Services.CountyService>();
            services.AddTransient<IDocumentTypeService, DocumentTypeService>();
            services.AddTransient<IKYCDocumentsService, KYCDocumentsService>();
            services.AddTransient<IUserDetailService, UserDetailsService>();
            services.AddTransient<IAddressService, AddressService>();
            services.AddTransient<ISMSService, SMSService>();

            services.AddTransient<IWalletService, WalletService>();

            services.AddTransient<ITransactionService, TransactionService>();
            services.AddTransient<ITransferTypeService, TransferTypeService>();

            services.AddTransient<ILookupService, LookupService>();
            services.AddTransient<ISmileIdentityJobService, SmileIdentityJobService>();

            services.AddTransient<IUploadDocument, StorageService>();
            services.AddTransient<ISmileIdentityService, SmileIdentityService>();
            services.AddTransient<IKYCLevelService, KYCLevelService>();
            services.AddTransient<ITransferFeeService, TransferFeeService>();

            services.AddSingleton<IHTTPService, HTTPService>();

            services.AddScoped<IEmailSender, EmailSenderService>();
            services.AddTransient<ITokenStorageService, TokenStorageService>();
            services.AddTransient<ISystemActivityLogsService, SystemActivityLogService>();
            services.AddTransient<ICustomerTicketsService, CustomerTicketsService>();
            services.AddTransient<INotesService, NotesService>();
            services.AddScoped<IWhatsAppService, WhatsAppService>();
            services.AddTransient<IUsersStaffRoleService, UsersStaffRoleService>();
            services.AddTransient<IVerificationService, VerificationService>();
            services.AddTransient<ICashFlowService, CashFlowHistoryService>();
            services.AddTransient<ISystemStatusService, SystemStatusService>();



            //swagger config
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "WleePay API",
                    Version = "v1"
                });
                //c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",

                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                           },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            });

            // Setup Auto Mapper
            services.AddAutoMapper(typeof(Startup));

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }

            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(
            //    Path.Combine(Directory.GetCurrentDirectory(), @"KYCDocuments")),
            //    RequestPath = new PathString("/Documents")
            //});

            app.UseSession();

            // app.UseHttpsRedirection();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();
            //add cors
            app.UseCors("AllowAllHeaders");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<StatusHubClient>("/status");

            });

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "wleepay.API v1"));
        }
    }
}
