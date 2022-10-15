using FluentValidation;
using GestaoAcesso.API.Application;
using GestaoAcesso.API.Application.Command.AssociarUsuarioPerfil;
using GestaoAcesso.API.Application.Command.AutenticarUsuario;
using GestaoAcesso.API.Application.Command.CadastrarUsuario;
using GestaoAcesso.API.Application.Command.CriptografarTexto;
using GestaoAcesso.API.Application.Command.ObterDadosUsuario;
using GestaoAcesso.API.Infrastructure;
using GestaoAcesso.API.Infrastructure.Interfaces;
using GestaoAcesso.API.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHealthChecks();
builder.Services.AddEndpointsApiExplorer();

#region Configurando Mediator

builder.Services.AddMediatR(typeof(CadastrarUsuarioCommand));
builder.Services.AddMediatR(typeof(CriptografarTextoCommand));
builder.Services.AddMediatR(typeof(ObterDadosUsuarioCommand));
builder.Services.AddMediatR(typeof(AutenticarUsuarioCommand));
builder.Services.AddMediatR(typeof(AssociarUsuarioPerfilCommand));

AssemblyScanner.FindValidatorsInAssembly(typeof(CadastrarUsuarioCommand).Assembly)
  .ForEach(item => builder.Services.AddScoped(item.InterfaceType, item.ValidatorType));

AssemblyScanner.FindValidatorsInAssembly(typeof(AutenticarUsuarioCommand).Assembly)
  .ForEach(item => builder.Services.AddScoped(item.InterfaceType, item.ValidatorType));

#endregion

#region Configurar Banco de dados

builder.Services.AddDbContext<GestaoAcessoContext>(options =>
{
    string nomeBancoDeDados = Environment.GetEnvironmentVariable("DB_CHECKLIST_SQL_DATABASE");
    string usuarioBanco = Environment.GetEnvironmentVariable("DB_CHECKLIST_SQL_USER");
    string senhaBanco = Environment.GetEnvironmentVariable("DB_CHECKLIST_SQL_PASSWORD");

    string connectionString = Environment.GetEnvironmentVariable("SQL_SERVER_CONNECTION_STRING")
                                   .Replace("{DB}", nomeBancoDeDados)
                                   .Replace("{USR}", usuarioBanco)
                                   .Replace("{PWD}", senhaBanco);

    options.UseSqlServer(connectionString,
        sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
        });
},
ServiceLifetime.Transient);

#endregion

#region Adicionar Repositorios

builder.Services.AddScoped<IUsuariosRepository, UsuariosRepository>();
builder.Services.AddScoped<IPerfisUsuariosRepository, PerfisUsuariosRepository>();

#endregion

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "apiagenda", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = @"JWT Authorization header using the Bearer scheme.
                   \r\n\r\n Enter 'Bearer'[space] and then your token in the text input below.
                    \r\n\r\nExample: 'Bearer 12345abcdef'",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              }
                          },
                         new string[] {}
                    }
                });
});

#region Configurar Autenticação JWT

builder.Services.AddAuthentication(authOptions =>
    {
        authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(bearerOptions =>
    {
        var paramsValidation = bearerOptions.TokenValidationParameters;
        paramsValidation.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuracao.Jwt.ChaveSecreta));
        paramsValidation.ValidAudience = Configuracao.Jwt.Audience;
        paramsValidation.ValidIssuer = Configuracao.Jwt.Issuer;
        paramsValidation.ValidateIssuerSigningKey = true;
        paramsValidation.ValidateLifetime = true;
        paramsValidation.ClockSkew = TimeSpan.Zero;

        paramsValidation.LifetimeValidator = (DateTime? notBefore, DateTime? expires, SecurityToken securityToken,
                                                TokenValidationParameters validationParameters) =>
                                            {
                                                return notBefore <= DateTime.UtcNow && expires >= DateTime.UtcNow;
                                            };
    });

builder.Services.AddAuthorization(auth =>
{
    auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
        .RequireAuthenticatedUser().Build());
});

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();