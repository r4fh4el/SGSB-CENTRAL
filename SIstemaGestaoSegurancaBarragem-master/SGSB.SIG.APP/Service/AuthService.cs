

using System.Net.Http.Headers;
using System.Security.Policy;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using SGSB.SIG.APP.Model;
using System.Text;
using Newtonsoft.Json;

namespace SGSB.SIG.APP.Service
{
    public class AuthService
    {
        public static string URI = @"https://api.sgsb.com.br";
        //public static string URI = @"https://10.0.2.2:7042";
        //public static string URI = @"http://10.0.2.2:7042";
        HttpClient http;
        public AuthService(HttpClient _http)
        {
            this.http = _http;
        }
        private static HttpClient CreateInsecureHttpClient()
        {
            // Crie um manipulador de HttpClient personalizado que aceita todos os certificados
            var handler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };

            // Configure o HttpClient para usar o handler personalizado
            return new HttpClient(handler);
        }
        public static async Task<bool> Logar(AuthModel model)
        {
            List<AuthModel> lstTicket = new List<AuthModel>();

            using (HttpClient cliente = new HttpClient())
            {
                var httpClient = CreateInsecureHttpClient();
                ////MUDAR O LOCALHOST SEU ANIMAL
                //cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                //HttpResponseMessage response = await cliente.GetAsync(URI + @"/API/ListarVertedouro/");

                // HTTP POST - define o produto
                //var aAthModel = new AuthModel()
                //{
                //    login = model.login,
                //    senha = model.senha,
                //};

                // Use PostAsync instead of PostAsJsonAsync
                string queryString = $"?login={Uri.EscapeDataString(model.login)}&senha={Uri.EscapeDataString(model.senha)}";

                // Use PostAsync with the modified URL
                HttpResponseMessage response = await httpClient.PostAsync($"{URI}/API/Logar/{queryString}", null);


                if (response.IsSuccessStatusCode)
                {
                    Uri produtoUrl = response.Headers.Location;
                    Infra.Constantes.USUARIO_AUTENTICADO = model;
                    return true;

                }
                else
                {
                    return false;
                }
            }

        }
        private async void Logarr(AuthModel model)
        {
            // Substitua a URL abaixo pela URL real da sua API
            string apiUrl = "https://exemplo.com/api/dados";

            // Substitua 'seuUsuario' e 'suaSenha' pelos dados reais de autenticação
            var aAthModel = new AuthModel()
            {
                login = model.login,
                senha = model.senha,
            };

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Configura as credenciais de autenticação no cabeçalho da solicitação
                    string credenciais = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{aAthModel.login}:{aAthModel.senha}"));
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credenciais);

                    // Faz uma requisição GET à API
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    // Verifica se a requisição foi bem-sucedida (código de status 200 OK)
                    if (response.IsSuccessStatusCode)
                    {
                        // Converte a resposta para uma string (pode variar dependendo do formato da resposta)
                        string conteudo = await response.Content.ReadAsStringAsync();

                        // Agora você pode processar os dados recebidos da API (por exemplo, exibir em um controle de interface do usuário)
                        // Exemplo: MeuLabel.Text = conteudo;
                    }
                    else
                    {
                        // Se a resposta não foi bem-sucedida, você pode tratar de acordo (exibir uma mensagem de erro, etc.)
                        // Exemplo: MeuLabel.Text = "Erro ao carregar dados da API";
                    }
                }
            }
            catch (Exception ex)
            {
                // Tratar exceções, se necessário
                // Exemplo: MeuLabel.Text = "Erro: " + ex.Message;
            }
        }
    }
}

