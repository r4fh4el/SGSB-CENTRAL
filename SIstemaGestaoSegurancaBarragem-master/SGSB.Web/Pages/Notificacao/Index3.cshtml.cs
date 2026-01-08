using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace SGSB.Web.Pages.Notificacao
{
    public class Index3Model : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public Index3Model(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task OnPostAsync()
        {
            // Substitua com suas próprias chaves do OneSignal
            string appId = Infra.Constantes.ONESIGNAL_APP_ID;
            string restApiKey = Infra.Constantes.ONE_SIGNAL_API_KEY;

            // URL da API do OneSignal
            string apiUrl = "https://onesignal.com/api/v1/notifications";

            // Dados da notificação
            var payload = new
            {
                app_id = appId,
                included_segments = new[] { "All" },  // Segmento de destinatários (aqui, todos os usuários)
                contents = new { en = "Esta é uma notificação de teste do OneSignal" },
                headings = new { en = "Título da Notificação" }
            };

            // Converte o payload para JSON
            var jsonPayload = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

            // Cabeçalhos da solicitação
            var headers = new Dictionary<string, string>
            {
                { "Authorization", "Basic " + restApiKey }
               
            };

            // Envia a notificação push
            using (var httpClient = _clientFactory.CreateClient())
            {
                foreach (var header in headers)
                {
                    httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                }

                var response = await httpClient.PostAsync(apiUrl, jsonPayload);

                // Verifica a resposta
                if (response.IsSuccessStatusCode)
                {
                    ViewData["Message"] = "Notificação enviada com sucesso!";
                }
                else
                {
                    ViewData["Message"] = $"Erro ao enviar a notificação: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}";
                }
            }
        }
    }
}