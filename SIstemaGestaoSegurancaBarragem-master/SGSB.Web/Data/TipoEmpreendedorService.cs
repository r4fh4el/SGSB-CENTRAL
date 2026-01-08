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
    public class TipoEmpreendedorService
    {
        HttpClient http;
        public TipoEmpreendedorService(HttpClient _http)
        {
            this.http = _http;
        }
        private readonly IJSRuntime js;


        public async Task<List<TipoEmpreendedor>> GetTipoEmpreendedor()
        {
            var objTipoEmpreendedor = await http.GetFromJsonAsync<List<TipoEmpreendedor>>( Infra.Constantes.URI +"/API/ListarTipoEmpreendedor");

            return objTipoEmpreendedor.ToList();

        }
        public async Task<TipoEmpreendedor> GetTipoEmpreendedorId(int id)
        {
            TipoEmpreendedor objTipoEmpreendedor = new TipoEmpreendedor();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdTipoEmpreendedor?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                     objTipoEmpreendedor = await response.Content.ReadFromJsonAsync<TipoEmpreendedor>();
                    return objTipoEmpreendedor;

                }
            }
            return objTipoEmpreendedor;
        }
        //public async Task<TipoEmpreendedor> EditTipoCondicaoFundacao(int id)
        //{
        //    var objTipoEmpreendedor = await http.GetFromJsonAsync<List<TipoEmpreendedor>>("/API/ListarTipoEmpreendedor");

        //    return objTipoEmpreendedor.Where(i => i.Id == id).FirstOrDefault();

        //}
        //public async Task<TipoEmpreendedor> VerTipoCondicaoFundacao(int id)
        //{
        //    var objTipoEmpreendedor = await http.GetFromJsonAsync<List<TipoEmpreendedor>>("/API/ListarTipoEmpreendedor");

        //    return objTipoEmpreendedor.Where(i => i.Id == id).FirstOrDefault();

        //}
        //public async Task<TipoEmpreendedor> Delete(int id)
        //{
        //    var objTipoEmpreendedor = await http.GetFromJsonAsync<List<TipoEmpreendedor>>("/API/ListarTipoEmpreendedor");

        //    return objTipoEmpreendedor.Where(i => i.Id == id).FirstOrDefault();

        //}

        public static async Task<bool> CadastrarTipoEmpreendedor(TipoEmpreendedor model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/ListarTipoEmpreendedor/" + model.Id);
                if (response.IsSuccessStatusCode)
                {
                    TipoEmpreendedor objTipoEmpreendedor = await response.Content.ReadFromJsonAsync<TipoEmpreendedor>();


                }
                // HTTP POST - define o produto
                var tipoEmpreendedor = new TipoEmpreendedor()
                {
                    Nome = model.Nome,
                    DataCadastro = DateTime.Now,
                    DataAlteracao = DateTime.Now


                };
                response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/AdicionarTipoEmpreendedor", tipoEmpreendedor);
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
        public static async Task<bool> EditarTipoEmpreendedor(TipoEmpreendedor model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdTipoEmpreendedor?id=" + model.Id);

                // HTTP POST - define o produto
                var tipoEmpreendedor = new TipoEmpreendedor()
                {
                    Id = model.Id,
                    Nome = model.Nome,
                    DataCadastro = DateTime.Now,
                    DataAlteracao = DateTime.Now


                };


                if (response.IsSuccessStatusCode)
                {
                    response = await cliente.PutAsJsonAsync(Infra.Constantes.URI + "/API/AtualizaTipoEmpreendedor/", tipoEmpreendedor);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public async Task<TipoEmpreendedor> EditTipoEmpreendedor(int id)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdTipoEmpreendedor?id=" + id);

                return await response.Content.ReadFromJsonAsync<TipoEmpreendedor>();

            }
        }
        public static async Task<bool> DeletarTipoEmpreendedor(TipoEmpreendedor model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdTipoEmpreendedor?id=" + model.Id);

              



                if (response.IsSuccessStatusCode)
                {
                 
                    response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/ExcluirTipoEmpreendedor", model);
                    return true;
                }
                return false;
            }
        }


    }
}

