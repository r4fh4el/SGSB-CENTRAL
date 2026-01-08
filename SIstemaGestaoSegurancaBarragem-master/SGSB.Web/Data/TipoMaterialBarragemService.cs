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
namespace SGSB.Web.Data
{
    public class TipoMaterialBarragemService
    {
        HttpClient http;
        public TipoMaterialBarragemService(HttpClient _http)
        {
            this.http = _http;
        }
        private readonly IJSRuntime js;


        public async Task<List<TipoMaterialBarragem>> GetTipoMaterialBarragem()
        {
            var objTipoMaterialBarragem = await http.GetFromJsonAsync<List<TipoMaterialBarragem>>( Infra.Constantes.URI +"/API/ListarTipoMaterialBarragem");

            return objTipoMaterialBarragem.ToList();

        }
        public async Task<TipoMaterialBarragem> GetTipoMaterialBarragemId(int id)
        {
            TipoMaterialBarragem objTipoMaterialBarragem = new TipoMaterialBarragem();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdTipoMaterialBarragem?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                     objTipoMaterialBarragem = await response.Content.ReadFromJsonAsync<TipoMaterialBarragem>();
                    return objTipoMaterialBarragem;

                }
            }
            return objTipoMaterialBarragem;
        }
        //public async Task<TipoMaterialBarragem> EditTipoCondicaoFundacao(int id)
        //{
        //    var objTipoMaterialBarragem = await http.GetFromJsonAsync<List<TipoMaterialBarragem>>("/API/ListarTipoMaterialBarragem");

        //    return objTipoMaterialBarragem.Where(i => i.Id == id).FirstOrDefault();

        //}
        //public async Task<TipoMaterialBarragem> VerTipoCondicaoFundacao(int id)
        //{
        //    var objTipoMaterialBarragem = await http.GetFromJsonAsync<List<TipoMaterialBarragem>>("/API/ListarTipoMaterialBarragem");

        //    return objTipoMaterialBarragem.Where(i => i.Id == id).FirstOrDefault();

        //}
        //public async Task<TipoMaterialBarragem> Delete(int id)
        //{
        //    var objTipoMaterialBarragem = await http.GetFromJsonAsync<List<TipoMaterialBarragem>>("/API/ListarTipoMaterialBarragem");

        //    return objTipoMaterialBarragem.Where(i => i.Id == id).FirstOrDefault();

        //}

        public static async Task<bool> CadastrarTipoMaterialBarragem(TipoMaterialBarragem model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/ListarTipoMaterialBarragem/" + model.Id);
                if (response.IsSuccessStatusCode)
                {
                    TipoMaterialBarragem objTipoMaterialBarragem = await response.Content.ReadFromJsonAsync<TipoMaterialBarragem>();


                }
                // HTTP POST - define o produto
                var tipoMaterialBarragem = new TipoMaterialBarragem()
                {
                    Nome = model.Nome,
                    DataCadastro = DateTime.Now,
                    DataAlteracao = DateTime.Now


                };
                response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/AdicionarTipoMaterialBarragem", tipoMaterialBarragem);
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
        public static async Task<bool> EditarTipoMaterialBarragem(TipoMaterialBarragem model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdTipoMaterialBarragem?id=" + model.Id);

                // HTTP POST - define o produto
                var tipoMaterialBarragem = new TipoMaterialBarragem()
                {
                    Id = model.Id,
                    Nome = model.Nome,
                    DataCadastro = DateTime.Now,
                    DataAlteracao = DateTime.Now


                };


                if (response.IsSuccessStatusCode)
                {
                    response = await cliente.PutAsJsonAsync(Infra.Constantes.URI + "/API/AtualizaTipoMaterialBarragem/", tipoMaterialBarragem);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public async Task<TipoMaterialBarragem> EditTipoMaterialBarragem(int id)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdTipoMaterialBarragem?id=" + id);

                return await response.Content.ReadFromJsonAsync<TipoMaterialBarragem>();

            }
        }
        public static async Task<bool> DeletarTipoMaterialBarragem(TipoMaterialBarragem model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdTipoMaterialBarragem?id=" + model.Id);

              



                if (response.IsSuccessStatusCode)
                {
                 
                    response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/ExcluirTipoMaterialBarragem", model);
                    return true;
                }
                return false;
            }
        }


    }
}

