using Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Linq;
using System;
using SGSB.Web.Models;
using Entidades.Entidades;

namespace SGSB.Web.Data
{
    public class TomadaDaguaService
    {

        HttpClient http;
        public TomadaDaguaService(HttpClient _http)
        {
            this.http = _http;
        }
        private readonly IJSRuntime js;

        public async Task<List<Model.TomadaDagua>> GetTomadaDaguaAsync()
        {
            var objTomadaDagua = await http.GetFromJsonAsync<List<Model.TomadaDagua>>(Infra.Constantes.URI + "/API/ListarTomadaDagua");

            return objTomadaDagua.ToList();

        }

            public async Task<int> GetProximoTomadaDaguaIdAsync()
            {
                var objTomadaDagua = await http.GetFromJsonAsync<List<Model.TomadaDagua>>(Infra.Constantes.URI + "/API/ListarTomadaDagua");

                int proximoId = 0;

                if (objTomadaDagua.ToList().Count == 0)
                {
                    proximoId = 1;
                }
                else
                {
                    proximoId = objTomadaDagua.ToList().LastOrDefault().Id + 1;
                }

                return proximoId;

            }
 


        public static async Task<Model.TomadaDagua> GetTomadaDaguaId(int id)
        {
            Model.TomadaDagua objTomadaDagua = new Model.TomadaDagua();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdTomadaDagua?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                    objTomadaDagua = await response.Content.ReadFromJsonAsync<Model.TomadaDagua>();
                    return objTomadaDagua;

                }
            }
            return objTomadaDagua;
        }
        public async Task<Model.TomadaDagua> VerTomadaDagua(int id)
        {
            Model.TomadaDagua objTomadaDagua = new Model.TomadaDagua();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdTomadaDagua?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                    objTomadaDagua = await response.Content.ReadFromJsonAsync<Model.TomadaDagua>();
                    return objTomadaDagua;

                }
            }
            return objTomadaDagua;
        }
        public static async Task<bool> EditarTomadaDagua(TomadaDaguaModel model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdTomadaDagua?id=" + model.Id);

                // HTTP POST - define o produto
                var tomadaDaguaModel = new TomadaDaguaModel()
                {
                    BarragemId = model.BarragemId,
                    Localizacao = model.Localizacao,
                    ComandoDistancia = model.ComandoDistancia,
                    ControleSaida = model.ControleSaida,
                    Comprimento = model.Comprimento,
                    DataCadastro = DateTime.Now,
                    DataAlteracao = DateTime.Now,
                    ControleEntrada = model.ControleEntrada,
                    CotasTomadasDaguaEntrada = model.CotasTomadasDaguaEntrada,
                    FonteAlternativaEnergia = model.FonteAlternativaEnergia,
                    PossibilidadeManobraManual = model.PossibilidadeManobraManual,
                    Id = model.Id

                };


                if (response.IsSuccessStatusCode)
                {
                    response = await cliente.PutAsJsonAsync(Infra.Constantes.URI + "/API/AtualizaTomadaDagua/", model);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        public async Task<Model.TomadaDagua> EditTomadaDagua(int id)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdTomadaDagua?id=" + id);

                return await response.Content.ReadFromJsonAsync<Model.TomadaDagua>();

            }
        }

        public static async Task<bool> DeletarTomadaDagua(Model.TomadaDagua model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdTomadaDagua?id=" + model.Id);
                
                if (response.IsSuccessStatusCode)
                {

                    response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/ExcluirTomadaDagua", model);
                    return true;
                }
                return false;
            }
        }
        public static async Task<bool> CadastrarTomadaDagua(TomadaDaguaModel model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
               
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/ListarTomadaDagua/");

                // HTTP POST - define o produto
                var tomadaDaguaModel = new TomadaDaguaModel()
                {
                    BarragemId = model.BarragemId,
                    Localizacao = model.Localizacao,
                    ComandoDistancia = model.ComandoDistancia,
                    ControleSaida = model.ControleSaida,
                    Comprimento = model.Comprimento,
                    DataCadastro = DateTime.Now,
                    DataAlteracao = DateTime.Now,
                    ControleEntrada = model.ControleEntrada,    
                    CotasTomadasDaguaEntrada =  model.CotasTomadasDaguaEntrada,
                    FonteAlternativaEnergia =  model.FonteAlternativaEnergia,
                    PossibilidadeManobraManual =  model.PossibilidadeManobraManual


                };
                response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/AdicionarTomadaDagua/", tomadaDaguaModel);
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

        public static async Task<TomadaDaguaModel> ListarTomadaDaguaBarragemId(int idBarragem)
        {
            List<TomadaDaguaModel> objTomadaDagua= new List<TomadaDaguaModel>();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/ListarTomadaDaguaBarragemId", idBarragem);
                if (response.IsSuccessStatusCode)
                {

                    objTomadaDagua= await response.Content.ReadFromJsonAsync<List<TomadaDaguaModel>>();

                    return objTomadaDagua.FirstOrDefault();
                }
            }
            return objTomadaDagua.FirstOrDefault();
        }

    }
}
