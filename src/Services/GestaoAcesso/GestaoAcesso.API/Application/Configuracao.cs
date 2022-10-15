namespace GestaoAcesso.API.Application
{
    public static class Configuracao
    {
        public static class Jwt
        {
            public readonly static TimeSpan TempoExpiracaoToken = TimeSpan.FromHours(24);
            public readonly static string Issuer = "autenticacao-usuarios";
            public readonly static string Audience = "gestao-acessos";
            public readonly static string ChaveSecreta = Environment.GetEnvironmentVariable("JWT_CHAVE_SECRETA");
        }
    }
}
