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
    public class TipoEdificacaoService
    {
        HttpClient http;
        public TipoEdificacaoService(HttpClient _http)
        {
            this.http = _http;
        }
        private readonly IJSRuntime js;


        public async Task<List<TipoEdificacao>> GetTipoEdificacao()
        {
            var objTipoEdificacao = await http.GetFromJsonAsync<List<TipoEdificacao>>( Infra.Constantes.URI +"/API/ListarTipoEdificacao");

            return objTipoEdificacao.ToList();

        }
        public async Task<TipoEdificacao> GetTipoEdificacaoId(int id)
        {
            TipoEdificacao objTipoEdificacao = new TipoEdificacao();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdTipoEdificacao?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                     objTipoEdificacao = await response.Content.ReadFromJsonAsync<TipoEdificacao>();
                    return objTipoEdificacao;

                }
            }
            return objTipoEdificacao;
        }
        //public async Task<TipoEdificacao> EditTipoCondicaoFundacao(int id)
        //{
        //    var objTipoEdificacao = await http.GetFromJsonAsync<List<TipoEdificacao>>("/API/ListarTipoEdificacao");

        //    return objTipoEdificacao.Where(i => i.Id == id).FirstOrDefault();

        //}
        //public async Task<TipoEdificacao> VerTipoCondicaoFundacao(int id)
        //{
        //    var objTipoEdificacao = await http.GetFromJsonAsync<List<TipoEdificacao>>("/API/ListarTipoEdificacao");

        //    return objTipoEdificacao.Where(i => i.Id == id).FirstOrDefault();

        //}
        //public async Task<TipoEdificacao> Delete(int id)
        //{
        //    var objTipoEdificacao = await http.GetFromJsonAsync<List<TipoEdificacao>>("/API/ListarTipoEdificacao");

        //    return objTipoEdificacao.Where(i => i.Id == id).FirstOrDefault();

        //}

        public static async Task<bool> CadastrarTipoEdificacao(TipoEdificacaoModel model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/ListarTipoEdificacao/" + model.Id);
                if (response.IsSuccessStatusCode)
                {
                    TipoEdificacao objTipoEdificacao = await response.Content.ReadFromJsonAsync<TipoEdificacao>();


                }
                // HTTP POST - define o produto
                var tipoEdificacao = new TipoEdificacaoModel()
                {
                    Nome = model.Nome


                };
                response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/AdicionarTipoEdificacao", tipoEdificacao);
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
        public static async Task<bool> EditarTipoEdificacao(TipoEdificacaoModel model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdTipoEdificacao?id=" + model.Id);

                // HTTP POST - define o produto
                var tipoEdificacao = new TipoEdificacao()
                {
                    Id = model.Id,
                    Nome = model.Nome,
                    DataCadastro = DateTime.Now,
                    DataAlteracao = DateTime.Now


                };


                if (response.IsSuccessStatusCode)
                {
                    response = await cliente.PutAsJsonAsync(Infra.Constantes.URI + "/API/AtualizaTipoEdificacao/", tipoEdificacao);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public async Task<TipoEdificacao> EditTipoEdificacao(int id)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdTipoEdificacao?id=" + id);

                return await response.Content.ReadFromJsonAsync<TipoEdificacao>();

            }
        }
        public static async Task<bool> DeletarTipoEdificacao(TipoEdificacao model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdTipoEdificacao?id=" + model.Id);

              



                if (response.IsSuccessStatusCode)
                {
                 
                    response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/ExcluirTipoEdificacao", model);
                    return true;
                }
                return false;
            }
        }


    }
}

