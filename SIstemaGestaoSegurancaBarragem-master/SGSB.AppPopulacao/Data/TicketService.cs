

using System.Net.Http.Headers;
using System.Security.Policy;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using SGSB.AppPopulacao.MVVM.Models;

namespace SGSB.AppPopulacao.Data
{
    public class TicketService
    {
        public static string URI = @"https://api.sgsb.com.br";
        HttpClient http;
        public TicketService(HttpClient _http)
        {
            this.http = _http;
        }

        public static async Task<bool> CadastrarTicket(TicketModel model)
         {
            List<TicketModel> lstTicket = new List<TicketModel>();

            using (var cliente = new HttpClient())
            {
                ////MUDAR O LOCALHOST SEU ANIMAL
                //cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(URI + @"/API/ListarVertedouro/");

                // HTTP POST - define o produto
                var ticketModel = new TicketModel()
                {
                    Descricao = model.Descricao,
                    Titulo = model.Titulo,
                    DataAlteracao = DateTime.Now,
                    DataCadastro = DateTime.Now,
                    Status = 1,
                    IdUsuario = 1,
                    TIcket = Guid.NewGuid().GetHashCode().ToString()
                };
                response = await cliente.PostAsJsonAsync(URI + @"/API/AdicionarTicket/", ticketModel);
                if (response.IsSuccessStatusCode)
                {
                    Uri produtoUrl = response.Headers.Location;

                    return true;

                }
                else
                {
                    return false;
                }
            }

        }
       


    }
}

