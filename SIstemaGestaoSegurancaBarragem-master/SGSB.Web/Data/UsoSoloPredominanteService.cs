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
    public class UsoSoloPredominanteService
    {
        HttpClient http;
        public UsoSoloPredominanteService(HttpClient _http)
        {
            this.http = _http;
        }
        private readonly IJSRuntime js;


        public async Task<List<UsoSoloPredominante>> GetUsoSoloPredominante()
        {
            var objUsoSoloPredominante = await http.GetFromJsonAsync<List<UsoSoloPredominante>>( Infra.Constantes.URI +"/API/ListarUsoSoloPredominante");

            return objUsoSoloPredominante.ToList();

        }
        public async Task<UsoSoloPredominante> GetUsoSoloPredominanteId(int id)
        {
            UsoSoloPredominante objUsoSoloPredominante = new UsoSoloPredominante();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdUsoSoloPredominante?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                     objUsoSoloPredominante = await response.Content.ReadFromJsonAsync<UsoSoloPredominante>();
                    return objUsoSoloPredominante;

                }
            }
            return objUsoSoloPredominante;
        }
        //public async Task<UsoSoloPredominante> EditTipoCondicaoFundacao(int id)
        //{
        //    var objUsoSoloPredominante = await http.GetFromJsonAsync<List<UsoSoloPredominante>>("/API/ListarUsoSoloPredominante");

        //    return objUsoSoloPredominante.Where(i => i.Id == id).FirstOrDefault();

        //}
        //public async Task<UsoSoloPredominante> VerTipoCondicaoFundacao(int id)
        //{
        //    var objUsoSoloPredominante = await http.GetFromJsonAsync<List<UsoSoloPredominante>>("/API/ListarUsoSoloPredominante");

        //    return objUsoSoloPredominante.Where(i => i.Id == id).FirstOrDefault();

        //}
        //public async Task<UsoSoloPredominante> Delete(int id)
        //{
        //    var objUsoSoloPredominante = await http.GetFromJsonAsync<List<UsoSoloPredominante>>("/API/ListarUsoSoloPredominante");

        //    return objUsoSoloPredominante.Where(i => i.Id == id).FirstOrDefault();

        //}

        public static async Task<bool> CadastrarUsoSoloPredominante(UsoSoloPredominante model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/ListarUsoSoloPredominante/" + model.Id);
                if (response.IsSuccessStatusCode)
                {
                    UsoSoloPredominante objUsoSoloPredominante = await response.Content.ReadFromJsonAsync<UsoSoloPredominante>();


                }
                // HTTP POST - define o produto
                var tipoMaterialBarragem = new UsoSoloPredominante()
                {
                    Nome = model.Nome,
                    DataCadastro = DateTime.Now,
                    DataAlteracao = DateTime.Now


                };
                response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/AdicionarUsoSoloPredominante", tipoMaterialBarragem);
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
        public static async Task<bool> EditarUsoSoloPredominante(UsoSoloPredominante model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdUsoSoloPredominante?id=" + model.Id);

                // HTTP POST - define o produto
                var tipoMaterialBarragem = new UsoSoloPredominante()
                {
                    Id = model.Id,
                    Nome = model.Nome,
                    DataCadastro = DateTime.Now,
                    DataAlteracao = DateTime.Now


                };


                if (response.IsSuccessStatusCode)
                {
                    response = await cliente.PutAsJsonAsync(Infra.Constantes.URI + "/API/AtualizaUsoSoloPredominante/", tipoMaterialBarragem);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public async Task<UsoSoloPredominante> EditUsoSoloPredominante(int id)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdUsoSoloPredominante?id=" + id);

                return await response.Content.ReadFromJsonAsync<UsoSoloPredominante>();

            }
        }
        public static async Task<bool> DeletarUsoSoloPredominante(UsoSoloPredominante model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdUsoSoloPredominante?id=" + model.Id);

              



                if (response.IsSuccessStatusCode)
                {
                 
                    response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/ExcluirUsoSoloPredominante", model);
                    return true;
                }
                return false;
            }
        }


    }
}

