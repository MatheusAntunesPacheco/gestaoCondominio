namespace Mobile.BFF.API.Config
{
    public static class UrlsConfig
    {
        public static class GestaoAcesso
        {
            public static readonly string UrlBasePath = Environment.GetEnvironmentVariable("URL_API_GESTAO_ACESSO");
            public static readonly string AutenticarUsuario = "usuarios/autenticacao";
        }
    }
}
