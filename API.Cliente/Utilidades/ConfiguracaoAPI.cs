namespace API.Cliente.Utilidades
{
    public static class ConfiguracaoAPI
    {
        public static string BuscaStringConexao()
        {
            var configuracoes = new Configuracoes();
            return configuracoes.ConfiguracaoAppSettings["ConnectionStrings"];
        }
    }
}
