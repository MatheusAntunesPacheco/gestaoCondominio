using FluentValidation;
using GestaoAcesso.API.Application.Command.AssociarUsuarioPerfil;
using GestaoAcesso.API.Application.Command.AutenticarUsuario;
using GestaoAcesso.API.Application.Command.CadastrarUsuario;
using GestaoAcesso.API.Application.Command.CriptografarTexto;
using GestaoAcesso.API.Application.Command.GerarTokenJwt;
using GestaoAcesso.API.Application.Command.LerTokenJwt;
using GestaoAcesso.API.Filters;
using GestaoAcesso.API.Infrastructure;
using GestaoAcesso.API.Infrastructure.Interfaces;
using GestaoAcesso.API.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMvc(options => options.Filters.Add(new DefaultExceptionFilterAttribute()));
builder.Services.AddHealthChecks();
builder.Services.AddEndpointsApiExplorer();

#region Configurando Mediator

builder.Services.AddMediatR(typeof(CadastrarUsuarioCommand));
builder.Services.AddMediatR(typeof(CriptografarTextoCommand));
builder.Services.AddMediatR(typeof(AutenticarUsuarioCommand));
builder.Services.AddMediatR(typeof(AssociarUsuarioPerfilCommand));
builder.Services.AddMediatR(typeof(GerarTokenJwtCommand));
builder.Services.AddMediatR(typeof(LerPayloadTokenJwtCommand));

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

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
