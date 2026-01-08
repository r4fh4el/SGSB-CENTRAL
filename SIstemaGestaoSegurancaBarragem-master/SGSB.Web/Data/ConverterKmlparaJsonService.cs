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
    public class ConverterKmlparaJsonService
    {
        HttpClient http;
        public ConverterKmlparaJsonService(HttpClient _http)
        {
            this.http = _http;
        }
        private readonly IJSRuntime js;


        public async Task<List<BarragemKml>> GetBarragemKml()
        {
            var objBarragemKml = await http.GetFromJsonAsync<List<BarragemKml>>( Infra.Constantes.URI +"/API/ListarBarragemKml");

            return objBarragemKml.ToList();

        }
        public async Task<BarragemKml> GetBarragemKmlId(int id)
        {
            BarragemKml objBarragemKml = new BarragemKml();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdBarragemKml?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                     objBarragemKml = await response.Content.ReadFromJsonAsync<BarragemKml>();
                    return objBarragemKml;

                }
            }
            return objBarragemKml;
        }
        //public async Task<BarragemKml> EditTipoCondicaoFundacao(int id)
        //{
        //    var objBarragemKml = await http.GetFromJsonAsync<List<BarragemKml>>("/API/ListarBarragemKml");

        //    return objBarragemKml.Where(i => i.Id == id).FirstOrDefault();

        //}
        //public async Task<BarragemKml> VerTipoCondicaoFundacao(int id)
        //{
        //    var objBarragemKml = await http.GetFromJsonAsync<List<BarragemKml>>("/API/ListarBarragemKml");

        //    return objBarragemKml.Where(i => i.Id == id).FirstOrDefault();

        //}
        //public async Task<BarragemKml> Delete(int id)
        //{
        //    var objBarragemKml = await http.GetFromJsonAsync<List<BarragemKml>>("/API/ListarBarragemKml");

        //    return objBarragemKml.Where(i => i.Id == id).FirstOrDefault();

        //}

        public static async Task<bool> CadastrarBarragemKml(BarragemKmlModel model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/ListarBarragemKml/" + model.Id);
                if (response.IsSuccessStatusCode)
                {
                    BarragemKml objBarragemKml = await response.Content.ReadFromJsonAsync<BarragemKml>();


                }
                // HTTP POST - define o produto
                var BarragemKml = new BarragemKmlModel()
                {
                    Id = model.Id,
                    Coordenadas = model.Coordenadas,
                    BarragemId = model.BarragemId,
                    DataCadastro = DateTime.Now,
                    DataAlteracao = DateTime.Now

                };
                response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/AdicionarBarragemKml", BarragemKml);
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
        public static async Task<bool> EditarBarragemKml(BarragemKmlModel model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdBarragemKml?id=" + model.Id);

                // HTTP POST - define o produto
                var BarragemKml = new BarragemKmlModel()
                {
                    Id = model.Id,
                    Coordenadas = model.Coordenadas,
                    BarragemId = model.BarragemId,
                    DataCadastro = DateTime.Now,
                    DataAlteracao = DateTime.Now


                };


                if (response.IsSuccessStatusCode)
                {
                    response = await cliente.PutAsJsonAsync(Infra.Constantes.URI + "/API/AtualizaBarragemKml/", BarragemKml);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public async Task<BarragemKml> EditBarragemKml(int id)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdBarragemKml?id=" + id);

                return await response.Content.ReadFromJsonAsync<BarragemKml>();

            }
        }
        public static async Task<bool> DeletarBarragemKml(BarragemKml model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdBarragemKml?id=" + model.Id);

              



                if (response.IsSuccessStatusCode)
                {
                 
                    response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/ExcluirBarragemKml", model);
                    return true;
                }
                return false;
            }
        }


    }
}

