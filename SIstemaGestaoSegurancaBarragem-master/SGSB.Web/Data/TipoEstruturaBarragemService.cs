using Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;

using System.Net.Http.Headers;
using System.Security.Policy;
using System.Net.Http.Json;
using System.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SGSB.Web.Data
{
    public class TipoEstruturaBarragemService
    {
        HttpClient http;
        public TipoEstruturaBarragemService(HttpClient _http)
        {
            this.http = _http;
        }
        private readonly IJSRuntime js;


        public async Task<List<TipoEstruturaBarragem>> GetTipoEstruturaBarragem()
        {
            var objTipoEstruturaBarragem = await http.GetFromJsonAsync<List<TipoEstruturaBarragem>>(Web.Infra.Constantes.URI +"/API/ListarTipoEstruturaBarragem");

            return objTipoEstruturaBarragem.ToList();

        }
        public async Task<TipoEstruturaBarragem> GetTipoEstruturaBarragemId(int id)
        {
            TipoEstruturaBarragem objTipoEstruturaBarragem = new TipoEstruturaBarragem();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Web.Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Web.Infra.Constantes.URI + "/API/BuscarPorIdTipoEstruturaBarragem?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                     objTipoEstruturaBarragem = await response.Content.ReadFromJsonAsync<TipoEstruturaBarragem>();
                    return objTipoEstruturaBarragem;

                }
            }
            return objTipoEstruturaBarragem;
        }
        //public async Task<TipoEstruturaBarragem> EditTipoCondicaoFundacao(int id)
        //{
        //    var objTipoEstruturaBarragem = await http.GetFromJsonAsync<List<TipoEstruturaBarragem>>("/API/ListarTipoEstruturaBarragem");

        //    return objTipoEstruturaBarragem.Where(i => i.Id == id).FirstOrDefault();

        //}
        //public async Task<TipoEstruturaBarragem> VerTipoCondicaoFundacao(int id)
        //{
        //    var objTipoEstruturaBarragem = await http.GetFromJsonAsync<List<TipoEstruturaBarragem>>("/API/ListarTipoEstruturaBarragem");

        //    return objTipoEstruturaBarragem.Where(i => i.Id == id).FirstOrDefault();

        //}
        //public async Task<TipoEstruturaBarragem> Delete(int id)
        //{
        //    var objTipoEstruturaBarragem = await http.GetFromJsonAsync<List<TipoEstruturaBarragem>>("/API/ListarTipoEstruturaBarragem");

        //    return objTipoEstruturaBarragem.Where(i => i.Id == id).FirstOrDefault();

        //}

        public static async Task<bool> CadastrarTipoEstruturaBarragem(TipoEstruturaBarragem model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Web.Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Web.Infra.Constantes.URI + "/API/ListarTipoEstruturaBarragem/" + model.Id);
                if (response.IsSuccessStatusCode)
                {
                    TipoEstruturaBarragem objTipoEstruturaBarragem = await response.Content.ReadFromJsonAsync<TipoEstruturaBarragem>();


                }
                // HTTP POST - define o produto
                var tipoMaterialBarragem = new TipoEstruturaBarragem()
                {
                    Nome = model.Nome,
                    DataCadastro = DateTime.Now,
                    DataAlteracao = DateTime.Now


                };
                response = await cliente.PostAsJsonAsync(Web.Infra.Constantes.URI + "/API/AdicionarTipoEstruturaBarragem", tipoMaterialBarragem);
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
        public static async Task<bool> EditarTipoEstruturaBarragem(TipoEstruturaBarragem model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Web.Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Web.Infra.Constantes.URI + "/API/BuscarPorIdTipoEstruturaBarragem?id=" + model.Id);

                // HTTP POST - define o produto
                var tipoMaterialBarragem = new TipoEstruturaBarragem()
                {
                    Id = model.Id,
                    Nome = model.Nome,
                    DataCadastro = DateTime.Now,
                    DataAlteracao = DateTime.Now


                };


                if (response.IsSuccessStatusCode)
                {
                    response = await cliente.PutAsJsonAsync(Web.Infra.Constantes.URI + "/API/AtualizaTipoEstruturaBarragem/", tipoMaterialBarragem);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public async Task<TipoEstruturaBarragem> EditTipoEstruturaBarragem(int id)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Web.Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await cliente.GetAsync(Web.Infra.Constantes.URI + "/API/BuscarPorIdTipoEstruturaBarragem?id=" + id);

                return await response.Content.ReadFromJsonAsync<TipoEstruturaBarragem>();

            }
        }
        public static async Task<bool> DeletarTipoEstruturaBarragem(TipoEstruturaBarragem model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Web.Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Web.Infra.Constantes.URI + "/API/BuscarPorIdTipoEstruturaBarragem?id=" + model.Id);

              



                if (response.IsSuccessStatusCode)
                {
                 
                    response = await cliente.PostAsJsonAsync(Web.Infra.Constantes.URI + "/API/ExcluirTipoEstruturaBarragem", model);
                    return true;
                }
                return false;
            }
        }


    }
}

