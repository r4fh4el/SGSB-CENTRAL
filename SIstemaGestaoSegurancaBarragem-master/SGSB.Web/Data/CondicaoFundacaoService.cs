using Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;

using System.Net.Http.Headers;
using System.Security.Policy;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Linq;
using System;
using SGSB.Web.Models;

namespace SGSB.Web.Data
{
    public class CondicaoFundacaoService
    {
        HttpClient http;
        public CondicaoFundacaoService(HttpClient _http)
        {
            this.http = _http;
        }
        private readonly IJSRuntime js;


        public async Task<List<CondicaoFundacao>> GetCondicaoFundacao()
        {
            var objCondicaoFundacao = await http.GetFromJsonAsync<List<CondicaoFundacao>>( Infra.Constantes.URI +"/API/ListarCondicaoFundacao");

            return objCondicaoFundacao.ToList();

        }
        public async Task<CondicaoFundacao> GetCondicaoFundacaoId(int id)
        {
            CondicaoFundacao objCondicaoFundacao = new CondicaoFundacao();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdCondicaoFundacao?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                     objCondicaoFundacao = await response.Content.ReadFromJsonAsync<CondicaoFundacao>();
                    return objCondicaoFundacao;

                }
            }
            return objCondicaoFundacao;
        }
  
        public static async Task<bool> CadastrarCondicaoFundacao(CondicaoFundacao model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/ListarCondicaoFundacao/" + model.Id);
                if (response.IsSuccessStatusCode)
                {
                    CondicaoFundacao objCondicaoFundacao = await response.Content.ReadFromJsonAsync<CondicaoFundacao>();


                }
                // HTTP POST - define o produto
                var tipoMaterialBarragem = new CondicaoFundacao()
                {
                    Nome = model.Nome,
                    DataCadastro = DateTime.Now,
                    DataAlteracao = DateTime.Now


                };
                response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/AdicionarCondicaoFundacao", tipoMaterialBarragem);
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
        public static async Task<bool> EditarCondicaoFundacao(CondicaoFundacao model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                //HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdCondicaoFundacao?id=" + model.Id);

                // HTTP POST - define o produto
                var tipoMaterialBarragem = new CondicaoFundacaoModel()
                {
                    Id = model.Id,
                    Nome = model.Nome

                };

               HttpResponseMessage response =   await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/AtualizaCondicaoFundacao/", tipoMaterialBarragem);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public async Task<CondicaoFundacao> EditCondicaoFundacao(int id)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdCondicaoFundacao?id=" + id);

                return await response.Content.ReadFromJsonAsync<CondicaoFundacao>();

            }
        }
        public static async Task<bool> DeletarCondicaoFundacao(CondicaoFundacao model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdCondicaoFundacao?id=" + model.Id);

              



                if (response.IsSuccessStatusCode)
                {
                 
                    response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/ExcluirCondicaoFundacao", model);
                    return true;
                }
                return false;
            }
        }


    }
}

