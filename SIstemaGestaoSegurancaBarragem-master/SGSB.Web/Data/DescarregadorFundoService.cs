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
    public class DescarregadorFundoService
    {

        HttpClient http;
        public DescarregadorFundoService(HttpClient _http)
        {
            this.http = _http;
        }
        private readonly IJSRuntime js;

        public async Task<List<Model.DescarregadorFundo>> GetDescarregadorFundoAsync()
        {
            var objDescarregadorFundo = await http.GetFromJsonAsync<List<Model.DescarregadorFundo>>(Infra.Constantes.URI + "/API/ListarDescarregadorFundo");

            return objDescarregadorFundo.ToList();

        }

        public async Task<int> GetProximoDescarregadorFundoIdAsync()
        {
            var objDescarregadorFundo = await http.GetFromJsonAsync<List<Model.DescarregadorFundo>>(Infra.Constantes.URI + "/API/ListarDescarregadorFundo");

            int proximoId = 0;

            if (objDescarregadorFundo.ToList().Count == 0)
            {
                proximoId = 1;
            }
            else
            {
                proximoId = objDescarregadorFundo.ToList().LastOrDefault().Id + 1;
            }

            return proximoId;

        }
 


        public static async Task<Model.DescarregadorFundo> GetDescarregadorFundoId(int id)
        {
            Model.DescarregadorFundo objDescarregadorFundo = new Model.DescarregadorFundo();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdDescarregadorFundo?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                    objDescarregadorFundo = await response.Content.ReadFromJsonAsync<Model.DescarregadorFundo>();
                    return objDescarregadorFundo;

                }
            }
            return objDescarregadorFundo;
        }
        public async Task<Model.DescarregadorFundo> VerDescarregadorFundo(int id)
        {
            Model.DescarregadorFundo objDescarregadorFundo = new Model.DescarregadorFundo();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdDescarregadorFundo?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                    objDescarregadorFundo = await response.Content.ReadFromJsonAsync<Model.DescarregadorFundo>();
                    return objDescarregadorFundo;

                }
            }
            return objDescarregadorFundo;
        }
        public static async Task<bool> EditarDescarregadorFundo(DescarregadorFundoModel model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdDescarregadorFundo?id=" + model.Id);

                // HTTP POST - define o produto
         


                if (response.IsSuccessStatusCode)
                {
                    response = await cliente.PutAsJsonAsync(Infra.Constantes.URI + "/API/AtualizaDescarregadorFundo/", model);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        public async Task<Model.DescarregadorFundo> EditDescarregadorFundo(int id)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdDescarregadorFundo?id=" + id);

                return await response.Content.ReadFromJsonAsync<Model.DescarregadorFundo>();

            }
        }

        public static async Task<bool> DeletarDescarregadorFundo(Model.DescarregadorFundo model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdDescarregadorFundo?id=" + model.Id);
                
                if (response.IsSuccessStatusCode)
                {

                    response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/ExcluirDescarregadorFundo", model);
                    return true;
                }
                return false;
            }
        }
        public static async Task<bool> CadastrarDescarregadorFundo(DescarregadorFundoModel model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
               
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/ListarDescarregadorFundo/");

                // HTTP POST - define o produto
                var cotaAreaVolume = new DescarregadorFundoModel()
                {
                    CotaSoleiraEntrada = model.CotaSoleiraEntrada,
                    ComandoDistancia = model.ComandoDistancia,

                    BarragemId = model.BarragemId,
                    Comprimento= model.Comprimento,
                    DataAlteracao= model.DataAlteracao,
                    DataCadastro= model.DataCadastro,
                    Dimensao= model.Dimensao,
            
                    FonteAlternativaEnergia = model.FonteAlternativaEnergia ,
                    Localizacao= model.Localizacao, 
                    Tipo=model.Tipo,
                    TipoAcionamentoComporta = model.TipoAcionamentoComporta ,
                    TipoComporta = model.TipoComporta
                    
                    


                };
                response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/AdicionarDescarregadorFundo/", cotaAreaVolume);
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


        public static async Task<DescarregadorFundoModel> ListarDescarregadorFundoBarragemId(int idBarragem)
        {
            List<DescarregadorFundoModel> objDescarregadorFundo = new List<DescarregadorFundoModel>();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/ListarDescarregadorFundoBarragemId", idBarragem);
                if (response.IsSuccessStatusCode)
                {

                    objDescarregadorFundo = await response.Content.ReadFromJsonAsync<List<DescarregadorFundoModel>>();

                    return objDescarregadorFundo.FirstOrDefault();
                }
            }
            return objDescarregadorFundo.FirstOrDefault();
        }
    }
}
