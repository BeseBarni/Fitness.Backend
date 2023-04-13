using Fitness.Backend.Application.BusinessLogic;
using Fitness.Backend.Application.Contracts.BusinessLogic;
using Fitness.Backend.Application.Contracts.Repositories;
using Fitness.Backend.Application.Contracts.Services;
using Fitness.Backend.Application.DataContracts.Entity;
using Fitness.Backend.Application.Seeders;
using Fitness.Backend.Domain.DbContexts;
using Fitness.Backend.Repositories;
using Fitness.Backend.Services.Auth;
using Fitness.Backend.WebApi.Filters;
using Fitness.Backend.WebApi.HostedServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.


builder.Services.AddScoped<ILessonRepository, LessonRepository>();
builder.Services.AddScoped<ISportRepository, SportRepository>();
builder.Services.AddScoped<IInstructorRepository, InstructorRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IAuthBusinessLogic, AuthBusinessLogic>();
builder.Services.AddScoped<ISportBusinessLogic, SportBusinessLogic>();
builder.Services.AddScoped<IInstructorBusinessLogic, InstructorBusinessLogic>();
builder.Services.AddScoped<IUserBusinessLogic, UserBusinessLogic>();
builder.Services.AddScoped<ILessonBusinessLogic, LessonBusinessLogic>();


builder.Services.AddScoped<IAuthTokenService, AuthTokenService>();

builder.Services.AddTransient<AuthSeeder>();
builder.Services.AddTransient<AppDataSeeder>();
builder.Services.AddTransient<AuthDbContext>();

builder.Services.AddCors();
builder.Services.AddControllers(options =>
{
    options.Filters.Add<HttpResponseExceptionFilter>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = configuration.GetConnectionString("Redis") ?? throw new InvalidOperationException("Connection string 'Redis' not found");
    options.InstanceName = "fitness:";
   
});

//Itt át kell írni a connection string-et
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            };
        });

builder.Services.Configure<IdentityOptions>(options =>
{
    //...
    options.SignIn.RequireConfirmedEmail = false;
    //...
});
builder.Services.AddHostedService<DatabaseSeederService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DisplayRequestDuration();
    });

app.UseCors(options =>
{
    options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();
