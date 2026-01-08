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
    public class TipoEdificacaoBarragemService
    {
        HttpClient http;
        public TipoEdificacaoBarragemService(HttpClient _http)
        {
            this.http = _http;
        }
        private readonly IJSRuntime js;


        public async Task<List<TipoEdificacaoBarragem>> GetTipoEdificacaoBarragem()
        {
            var objTipoEdificacaoBarragem = await http.GetFromJsonAsync<List<TipoEdificacaoBarragem>>( Infra.Constantes.URI +"/API/ListarTipoEdificacaoBarragem");

            return objTipoEdificacaoBarragem.ToList();

        }
        public async Task<TipoEdificacaoBarragem> GetTipoEdificacaoBarragemId(int id)
        {
            TipoEdificacaoBarragem objTipoEdificacaoBarragem = new TipoEdificacaoBarragem();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdTipoEdificacaoBarragem?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                     objTipoEdificacaoBarragem = await response.Content.ReadFromJsonAsync<TipoEdificacaoBarragem>();
                    return objTipoEdificacaoBarragem;

                }
            }
            return objTipoEdificacaoBarragem;
        }
        //public async Task<TipoEdificacaoBarragem> EditTipoCondicaoFundacao(int id)
        //{
        //    var objTipoEdificacaoBarragem = await http.GetFromJsonAsync<List<TipoEdificacaoBarragem>>("/API/ListarTipoEdificacaoBarragem");

        //    return objTipoEdificacaoBarragem.Where(i => i.Id == id).FirstOrDefault();

        //}
        //public async Task<TipoEdificacaoBarragem> VerTipoCondicaoFundacao(int id)
        //{
        //    var objTipoEdificacaoBarragem = await http.GetFromJsonAsync<List<TipoEdificacaoBarragem>>("/API/ListarTipoEdificacaoBarragem");

        //    return objTipoEdificacaoBarragem.Where(i => i.Id == id).FirstOrDefault();

        //}
        //public async Task<TipoEdificacaoBarragem> Delete(int id)
        //{
        //    var objTipoEdificacaoBarragem = await http.GetFromJsonAsync<List<TipoEdificacaoBarragem>>("/API/ListarTipoEdificacaoBarragem");

        //    return objTipoEdificacaoBarragem.Where(i => i.Id == id).FirstOrDefault();

        //}

        public static async Task<bool> CadastrarTipoEdificacaoBarragem(TipoEdificacaoBarragemModel model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/ListarTipoEdificacaoBarragem/" + model.Id);
                if (response.IsSuccessStatusCode)
                {
                    TipoEdificacaoBarragem objTipoEdificacaoBarragem = await response.Content.ReadFromJsonAsync<TipoEdificacaoBarragem>();


                }
                // HTTP POST - define o produto
                var TipoEdificacaoBarragem = new TipoEdificacaoBarragemModel()
                {
                    BarragemId = model.BarragemId,
                    TipoEdificacaoId = model.TipoEdificacaoId
                    

                };
                response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/AdicionarTipoEdificacaoBarragem", TipoEdificacaoBarragem);
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
        public static async Task<bool> EditarTipoEdificacaoBarragem(TipoEdificacaoBarragemModel model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                //HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdTipoEdificacaoBarragem?id=" + model.Id);

                // HTTP POST - define o produto
                // HTTP POST - define o produto
                var TipoEdificacaoBarragem = new TipoEdificacaoBarragemModel()
                {
                    BarragemId = model.BarragemId,
                    TipoEdificacaoId = model.TipoEdificacaoId,
                    Id = model.Id
                    


                };
                HttpResponseMessage response = await cliente.PutAsJsonAsync(Infra.Constantes.URI + "/API/AtualizaTipoEdificacaoBarragem/", TipoEdificacaoBarragem);

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
        public async Task<TipoEdificacaoBarragem> EditTipoEdificacaoBarragem(int id)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdTipoEdificacaoBarragem?id=" + id);

                return await response.Content.ReadFromJsonAsync<TipoEdificacaoBarragem>();

            }
        }
        public static async Task<bool> DeletarTipoEdificacaoBarragem(TipoEdificacaoBarragem model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdTipoEdificacaoBarragem?id=" + model.Id);

              



                if (response.IsSuccessStatusCode)
                {
                 
                    response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/ExcluirTipoEdificacaoBarragem", model);
                    return true;
                }
                return false;
            }
        }

        public static async Task<List<TipoEdificacaoBarragemModel>> ListarTipoEdificacaoBarragemBarragemId(int idBarragem)
        {
            List<TipoEdificacaoBarragemModel> objTipoEdificacaoBarragem = new List<TipoEdificacaoBarragemModel>();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/ListarTipoEdificacaoBarragemBarragemId", idBarragem);
                if (response.IsSuccessStatusCode)
                {
                    objTipoEdificacaoBarragem = await response.Content.ReadFromJsonAsync<List<TipoEdificacaoBarragemModel>>();

                    return objTipoEdificacaoBarragem;
                }
            }
            return objTipoEdificacaoBarragem;
        }

    }
}

