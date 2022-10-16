using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Mobile.BFF.API.Application;
using Mobile.BFF.API.Application.Command.AutenticarUsuario;
using Mobile.BFF.API.Application.Command.GerarTokenJwt;
using Mobile.BFF.API.Application.Command.LerTokenJwt;
using Mobile.BFF.API.Filters;
using Mobile.BFF.API.Services.Agendamento;
using Mobile.BFF.API.Services.GestaoAcessos;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMvc(options => options.Filters.Add(new DefaultExceptionFilterAttribute()));

#region Configurar Mediator

builder.Services.AddMediatR(typeof(AutenticarUsuarioCommand));
builder.Services.AddMediatR(typeof(GerarTokenJwtCommand));
builder.Services.AddMediatR(typeof(LerPayloadTokenJwtCommand));

#endregion

#region Configurar Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mobile BFF", Version = "v1", Description = "API responsável por realizar a interface entre o aplicativo Mobile e o backend" });

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

#endregion

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

#region Adicionar Clients

builder.Services.AddScoped<IGestaoAcessoClient, GestaoAcessoClient>();
builder.Services.AddScoped<IAgendamentoClient, AgendamentoClient>();

#endregion

#region Configurar HTTP

builder.Services.AddHttpClient<IGestaoAcessoClient, GestaoAcessoClient>();

#endregion

var app = builder.Build();

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
