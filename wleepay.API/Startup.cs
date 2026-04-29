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
using System.Linq;
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
using wleepay.API.Middleware;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using System.IO;
using wleepay.API.SignalRHub;
using wleepay.Core.Configurations;
using Microsoft.Win32;
using wleepay.Services.Account;
using wleepay.Services.Auth;

namespace wleepay.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wleepay-84b9f-firebase-adminsdk-rimd0-a5dbd3a972.json")),
            });
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));
            services.AddDbContext<WPContext>(options => options.UseSqlServer(Configuration.GetConnectionString("WleePayDataBase"), o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

            //Register Authorize.Net settings here
            services.Configure<AuthorizeNetSettings>(Configuration.GetSection("AuthorizeNet"));

            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.Zero;
                options.Lockout.MaxFailedAccessAttempts = 3;
            })
                   .AddEntityFrameworkStores<WPContext>()
                   .AddDefaultTokenProviders();

            services.Configure<DataProtectionTokenProviderOptions>(opt =>

                opt.TokenLifespan = TimeSpan.FromMinutes(5));

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
                    RequireSignedTokens = true,
                    ClockSkew = TimeSpan.Zero
                };


            });

            services.AddSignalR();

            //services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowAllHeaders",
            //        builder => builder.AllowAnyMethod()
            //        .AllowAnyHeader()
            //        .AllowCredentials()
            //        .SetIsOriginAllowed((hosts) => true));
            //});



            // server
            services.AddCors(options =>
            {
                options.AddPolicy("RestrictedCors", builder =>
                {
                    var allowedOrigins = Configuration.GetSection("Cors:AllowedOrigins")
                        .Get<string[]>()?
                        .Where(origin => !string.IsNullOrWhiteSpace(origin))
                        .Select(origin => origin.Trim().TrimEnd('/'))
                        .Distinct(StringComparer.OrdinalIgnoreCase)
                        .ToArray() ?? Array.Empty<string>();

                    if (allowedOrigins.Length > 0)
                    {
                        builder.WithOrigins(allowedOrigins)
                               .AllowAnyHeader()
                               .AllowAnyMethod()
                               .AllowCredentials();
                    }
                });
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
            services.AddTransient<IWleeSendUserDetailService, WleeSendUserDetailService>();


            services.AddTransient<ILookupService, LookupService>();
            services.AddTransient<ISmileIdentityJobService, SmileIdentityJobService>();

            services.AddTransient<IUploadDocument, StorageService>();
            services.AddTransient<ISmileIdentityService, SmileIdentityService>();
            services.AddTransient<IKYCLevelService, KYCLevelService>();
            services.AddTransient<ITransferFeeService, TransferFeeService>();

            services.AddSingleton<IHTTPService, HTTPService>();

            services.AddScoped<IEmailSender, EmailService>();
            services.AddTransient<ISystemActivityLogsService, SystemActivityLogService>();
            services.AddTransient<ICustomerTicketsService, CustomerTicketsService>();
            services.AddTransient<INotesService, NotesService>();
            services.AddScoped<IWhatsAppService, WhatsAppService>();
            services.AddTransient<IUsersStaffRoleService, UsersStaffRoleService>();
            services.AddTransient<IVerificationService, VerificationService>();
            services.AddTransient<ICashFlowService, CashFlowHistoryService>();
            services.AddTransient<ISystemStatusService, SystemStatusService>();
            services.AddTransient<IFirebaseNotification, FirebaseMessageService>();
            services.AddTransient<IUserDeviceService, UserDeviceService>();
            services.AddTransient<IExternalApiService, ExternalApiService>();
            services.AddTransient<IStripeService, StripeService>();
            services.AddTransient<ISochitelTransactionService, SochitelTransactionService>();
            services.AddTransient<ICreditCardTransactionService, CreditCardTransactionService>();
            services.AddTransient<IDashboardService, DashboardService>();
            services.AddTransient<IReportService, ReportService>();
            services.AddTransient<IWleeSendUserDetailService, WleeSendUserDetailService>();

            services.AddScoped<IAuthorizeNetService, AuthorizeNetService>();

            services.AddScoped<IPaymentTransactionService, PaymentTransactionService>();
            services.AddScoped<IPaymentTransactionRepository, PaymentTransactionRepository>();

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IJwtTokenService, JwtTokenService>();

            services.AddScoped<IValidationService, ValidationService>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();

            //swagger config
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "WleeSend API",
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

            //services.AddControllers().AddNewtonsoftJson(options =>
            //{
            //    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            //});

            services.AddControllers(options =>
            {
                options.Filters.Add<OtpExceptionFilter>();

            });
            services.AddControllers(options =>
            {
                options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
            })

            .ConfigureApiBehaviorOptions(options =>
         {
             options.SuppressModelStateInvalidFilter = true;
         })
        .AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        });
            services.AddMemoryCache();
            services.AddHttpClient();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "wleepay.API v1"));

            app.Use(async (context, next) =>
            {
                context.Response.Headers["Strict-Transport-Security"] = "max-age=31536000; includeSubDomains; preload";
                context.Response.Headers["X-Content-Type-Options"] = "nosniff";
                context.Response.Headers["Referrer-Policy"] = "no-referrer";
                context.Response.Headers["Permissions-Policy"] = "geolocation=()";
                context.Response.Headers["Cross-Origin-Opener-Policy"] = "same-origin";
                context.Response.Headers["Cross-Origin-Embedder-Policy"] = "unsafe-none";
                context.Response.Headers["X-Frame-Options"] = "SAMEORIGIN";
                context.Response.Headers["X-XSS-Protection"] = "0";
                context.Response.Headers["Content-Security-Policy"] =
                    "default-src 'self'; " +
                    "script-src 'self' https://ajax.googleapis.com https://cdnjs.cloudflare.com; " +
                    "style-src 'self' 'unsafe-inline' https://fonts.googleapis.com https://cdnjs.cloudflare.com; " +
                    "font-src 'self' https://fonts.gstatic.com https://cdnjs.cloudflare.com; " +
                    "img-src 'self' data:; " +
                    "connect-src 'self' https://wleesend.arielsoftwares.in https://wleepapi.arielsoftwares.in wss://wleepapi.arielsoftwares.in; " +
                    "object-src 'none'; " +
                    // if you never embed the site in frames, prefer 'none'; if you need same origin use 'self'
                    "frame-ancestors 'self'; " +
                    "upgrade-insecure-requests;";

                await next();
            });



            // server
            //app.Use(async (context, next) =>
            //{
            //    context.Response.Headers.Add("Strict-Transport-Security", "max-age=31536000; includeSubDomains; preload");
            //    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
            //    context.Response.Headers.Add("X-Frame-Options", "DENY");
            //    context.Response.Headers.Add("X-XSS-Protection", "0");
            //    context.Response.Headers.Add("Referrer-Policy", "no-referrer");
            //    context.Response.Headers.Add("Content-Security-Policy",
            //        "default-src 'self'; " +
            //        "script-src 'self'; " +
            //        "style-src 'self'; " +
            //        "img-src 'self' data:; " +
            //        "connect-src 'self' https://wleesend.arielsoftwares.in https://wleepapi.arielsoftwares.in wss://wleepapi.arielsoftwares.in;");
            //    await next();
            //});



            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(
            //    Path.Combine(Directory.GetCurrentDirectory(), @"KYCDocuments")),
            //    RequestPath = new PathString("/Documents")
            //});

            app.UseSession();
            if (Configuration.GetValue<bool?>("EnableHttpsRedirection") ?? true)
            {
                app.UseHttpsRedirection();
            }
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseRouting();
            app.UseCors("RestrictedCors");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<StatusHubClient>("/status");
                endpoints.MapHub<TicketsNotificationHubClient>("/notification");
                endpoints.MapHub<PortalNotificationHubClient>("/portalNotification");  // SignalR Hub Endpoint
                endpoints.MapControllerRoute(
                       name: "areas",
                       pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                     );

            });
        }
    }
}
