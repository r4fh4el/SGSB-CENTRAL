using Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;

using System.Net.Http.Headers;
using System.Security.Policy;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using SGSB.Web.Models;

namespace SGSB.Web.Data
{
    public class TicketService
    {
        HttpClient http;
        public TicketService(HttpClient _http)
        {
            this.http = _http;
        }
        private readonly IJSRuntime js;


        public  async Task<List<Ticket>> GetTicket()
        {
            var objTicket = await http.GetFromJsonAsync<List<Ticket>>( Infra.Constantes.URI +"/API/ListarTicket");

            return objTicket.ToList();

        }
        public async Task<Ticket> GetTicketId(int id)
        {
            Ticket objTicket = new Ticket();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdTicket?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                     objTicket = await response.Content.ReadFromJsonAsync<Ticket>();
                    return objTicket;

                }
            }
            return objTicket;
        }


        public static async Task<bool> CadastrarTicket(Entidades.Entidades.Ticket TicketModel)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/ListarTicket/" + TicketModel.Id);
                if (response.IsSuccessStatusCode)
                {
                    Ticket objTicket = await response.Content.ReadFromJsonAsync<Ticket>();


                }
                // HTTP POST - define o produto
                var objTicketModel = new Ticket()
                {
                    Id = TicketModel.Id,
                    TIcket = TicketModel.TIcket,
                    DataAlteracao = TicketModel.DataAlteracao,
                    Descricao = TicketModel.Descricao,
                    Titulo = TicketModel.Titulo,
                    IdUsuario = TicketModel.IdUsuario,
                    Status = TicketModel.Status,
                    DataCadastro = TicketModel.DataCadastro

                };
                response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/AdicionarTicket", objTicketModel);
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
        public static async Task<bool> EditarTicket(Ticket TicketModel)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdTicket?id=" + TicketModel.Id);

                // HTTP POST - define o produto
                var objTicketModel = new Ticket()
                {
                    Id = TicketModel.Id,
                    TIcket = TicketModel.TIcket,
                    DataAlteracao = TicketModel.DataAlteracao,
                    Descricao = TicketModel.Descricao,
                    Titulo = TicketModel.Titulo,
                    IdUsuario = TicketModel.IdUsuario,
                    Status = TicketModel.Status,
                    DataCadastro = TicketModel.DataCadastro

                };

                if (response.IsSuccessStatusCode)
                {
                    response = await cliente.PutAsJsonAsync(Infra.Constantes.URI + "/API/AtualizaTicket/", objTicketModel);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public async Task<Ticket> EditTicket(int id)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdTicket?id=" + id);

                return await response.Content.ReadFromJsonAsync<Ticket>();

            }
        }
        public static async Task<bool> DeletarTicket(Ticket model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdTicket?id=" + model.Id);

              



                if (response.IsSuccessStatusCode)
                {
                 
                    response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/ExcluirTicket", model);
                    return true;
                }
                return false;
            }
        }


    }
}

