
namespace API.Cliente.Utilidades
{
    internal class Configuracoes
    {
        private IConfiguration configuracaoAppSettigs;

        public IConfiguration ConfiguracaoAppSettings
        {
            get { return this.configuracaoAppSettigs; }
        }

        internal Configuracoes()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            
            if(string.IsNullOrEmpty(environment))
            {
                throw new Exception("Problemas ao configurar a variável de ambiente");
            }

            try
            {
                var builder = new ConfigurationBuilder()
                    .AddJsonFile($"appsettings.json", optional: true, reloadOnChange: false)
                    .AddJsonFile($"appsettings.{environment}.json", optional:true, reloadOnChange:false)
                    .AddEnvironmentVariables();
                this.configuracaoAppSettigs = builder.Build();
            }
            catch(FormatException ex)
            {
                throw new Exception("Arquivo de configuração não configurado corretamente", ex);
            }
            catch(Exception ex)
            {
                throw new Exception("Erro ao carregar os arquivos", ex);
            }
        }
    }
}
