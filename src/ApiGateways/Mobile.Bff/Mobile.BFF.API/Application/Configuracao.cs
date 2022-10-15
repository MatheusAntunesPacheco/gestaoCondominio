namespace Mobile.BFF.API.Application
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

        public static class Url
        {
            public static class ApiGestaoAcesso
            {
                public static readonly string UrlBasePath = Environment.GetEnvironmentVariable("URL_API_GESTAO_ACESSO");
                public static readonly string CriacaoUsuario = "usuarios";
                public static readonly string AutenticacaoUsuario = "usuarios/autenticacao";
                public static readonly string AssociacaoUsuario = "usuarios/perfil";
            }
        }
    }
}
