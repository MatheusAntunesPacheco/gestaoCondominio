namespace Agendamento.API.Application
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

        public static class BancoDeDados
        {
            private readonly static string StringBaseConexao = Environment.GetEnvironmentVariable("BD_STRING_CONEXAO_BANCO_DE_DADOS");
            private readonly static string NomeBanco = Environment.GetEnvironmentVariable("BD_NOME_BANCO_DE_DADOS");
            private readonly static string Usuario = Environment.GetEnvironmentVariable("BD_USUARIO_BANCO_DE_DADOS");
            private readonly static string Senha = Environment.GetEnvironmentVariable("BD_SENHA_BANCO_DE_DADOS");

            public readonly static string StringConexao = StringBaseConexao.
                                    Replace("{DB}", NomeBanco)
                                   .Replace("{USR}", Usuario)
                                   .Replace("{PWD}", Senha);
        }
    }
}
