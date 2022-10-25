using Agendamento.API.Application.Command.AgendarEvento;
using Agendamento.API.Application.Command.AlterarEvento;
using Agendamento.API.Application.Command.CancelarEvento;
using Agendamento.API.Infrastructure.Repositories;
using Agendamento.Infrastructure;
using Agendamento.Infrastructure.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Agendamentos", Version = "v1", Description = "API responsável por gerenciar os agendamentos de eventos e areas comuns do predio" });
});

#region Configurar Mediator

builder.Services.AddMediatR(typeof(AgendarEventoCommand));
builder.Services.AddMediatR(typeof(CancelarEventoCommand));
builder.Services.AddMediatR(typeof(AlterarEventoCommand));

#endregion

#region Configurar Banco de dados

builder.Services.AddDbContext<AgendamentoContext>(options =>
{
    string nomeBancoDeDados = Environment.GetEnvironmentVariable("BD_NOME_BANCO_DE_DADOS");
    string usuarioBanco = Environment.GetEnvironmentVariable("BD_USUARIO_BANCO_DE_DADOS");
    string senhaBanco = Environment.GetEnvironmentVariable("BD_SENHA_BANCO_DE_DADOS");

    string connectionString = Environment.GetEnvironmentVariable("BD_STRING_CONEXAO_BANCO_DE_DADOS")
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

builder.Services.AddScoped<IAgendamentosRepository, AgendamentosRepository>();

#endregion

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
