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
    public class InstrumentosService
    {
        HttpClient http;
        public InstrumentosService(HttpClient _http)
        {
            this.http = _http;
        }
        private readonly IJSRuntime js;


        public async Task<List<Instrumentos>> GetInstrumentos()
        {
            var objInstrumentos = await http.GetFromJsonAsync<List<Instrumentos>>( Infra.Constantes.URI +"/API/ListarInstrumentos");

            return objInstrumentos.ToList();

        }

        public async Task<int> GetProximoInstrumentosIdAsync()
        {
            var objInstrumentos = await http.GetFromJsonAsync<List<Model.Instrumentos>>(Infra.Constantes.URI + "/API/ListarInstrumentos");

            int proximoId = 0;

            if (objInstrumentos.ToList().Count == 0)
            {
                proximoId = 1;
            }
            else
            {
                proximoId = objInstrumentos.ToList().LastOrDefault().Id + 1;
            }

            return proximoId;

        }

        public async Task<Instrumentos> GetInstrumentosId(int id)
        {
            Instrumentos objInstrumentos = new Instrumentos();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdInstrumentos?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                     objInstrumentos = await response.Content.ReadFromJsonAsync<Instrumentos>();
                    return objInstrumentos;

                }
            }
            return objInstrumentos;
        }
        //public async Task<Instrumentos> EditTipoCondicaoFundacao(int id)
        //{
        //    var objInstrumentos = await http.GetFromJsonAsync<List<Instrumentos>>("/API/ListarInstrumentos");

        //    return objInstrumentos.Where(i => i.Id == id).FirstOrDefault();

        //}
        //public async Task<Instrumentos> VerTipoCondicaoFundacao(int id)
        //{
        //    var objInstrumentos = await http.GetFromJsonAsync<List<Instrumentos>>("/API/ListarInstrumentos");

        //    return objInstrumentos.Where(i => i.Id == id).FirstOrDefault();

        //}
        //public async Task<Instrumentos> Delete(int id)
        //{
        //    var objInstrumentos = await http.GetFromJsonAsync<List<Instrumentos>>("/API/ListarInstrumentos");

        //    return objInstrumentos.Where(i => i.Id == id).FirstOrDefault();

        //}

        public static async Task<bool> CadastrarInstrumentos(Entidades.Entidades.Instrumentos model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/ListarInstrumentos/" + model.Id);
                if (response.IsSuccessStatusCode)
                {
                    Instrumentos objInstrumentos = await response.Content.ReadFromJsonAsync<Instrumentos>();


                }
                // HTTP POST - define o produto
                var tipoMaterialBarragem = new InstrumentosModel()
                {
                    Nome = model.Nome


                };
                response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/AdicionarInstrumentos", tipoMaterialBarragem);
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
        public static async Task<bool> EditarInstrumentos(Instrumentos model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdInstrumentos?id=" + model.Id);

                // HTTP POST - define o produto
                var tipoMaterialBarragem = new Instrumentos()
                {
                    Id = model.Id,
                    Nome = model.Nome,
                    DataCadastro = DateTime.Now,
                    DataAlteracao = DateTime.Now


                };


                if (response.IsSuccessStatusCode)
                {
                    response = await cliente.PutAsJsonAsync(Infra.Constantes.URI + "/API/AtualizaInstrumentos/", tipoMaterialBarragem);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public async Task<Instrumentos> EditInstrumentos(int id)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdInstrumentos?id=" + id);

                return await response.Content.ReadFromJsonAsync<Instrumentos>();

            }
        }
        public static async Task<bool> DeletarInstrumentos(Instrumentos model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdInstrumentos?id=" + model.Id);

              



                if (response.IsSuccessStatusCode)
                {
                 
                    response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/ExcluirInstrumentos", model);
                    return true;
                }
                return false;
            }
        }


    }
}

