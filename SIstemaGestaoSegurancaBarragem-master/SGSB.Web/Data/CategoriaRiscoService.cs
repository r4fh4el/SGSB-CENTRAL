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

namespace SGSB.Web.Data
{
    public class CategoriaRiscoService
    {
        HttpClient http;
        public CategoriaRiscoService(HttpClient _http)
        {
            this.http = _http;
        }
        private readonly IJSRuntime js;


        public async Task<List<CategoriaRisco>> GetCategoriaRisco()
        {
            var objCategoriaRisco = await http.GetFromJsonAsync<List<CategoriaRisco>>( Infra.Constantes.URI +"/API/ListarCategoriaRisco");

            return objCategoriaRisco.ToList();

        }
        public async Task<CategoriaRisco> GetCategoriaRiscoId(int id)
        {
            CategoriaRisco objCategoriaRisco = new CategoriaRisco();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdCategoriaRisco?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                     objCategoriaRisco = await response.Content.ReadFromJsonAsync<CategoriaRisco>();
                    return objCategoriaRisco;

                }
            }
            return objCategoriaRisco;
        }
        //public async Task<CategoriaRisco> EditTipoCondicaoFundacao(int id)
        //{
        //    var objCategoriaRisco = await http.GetFromJsonAsync<List<CategoriaRisco>>("/API/ListarCategoriaRisco");

        //    return objCategoriaRisco.Where(i => i.Id == id).FirstOrDefault();

        //}
        //public async Task<CategoriaRisco> VerTipoCondicaoFundacao(int id)
        //{
        //    var objCategoriaRisco = await http.GetFromJsonAsync<List<CategoriaRisco>>("/API/ListarCategoriaRisco");

        //    return objCategoriaRisco.Where(i => i.Id == id).FirstOrDefault();

        //}
        //public async Task<CategoriaRisco> Delete(int id)
        //{
        //    var objCategoriaRisco = await http.GetFromJsonAsync<List<CategoriaRisco>>("/API/ListarCategoriaRisco");

        //    return objCategoriaRisco.Where(i => i.Id == id).FirstOrDefault();

        //}

        public static async Task<bool> CadastrarCategoriaRisco(CategoriaRisco CategoriaRiscoModel)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/ListarCategoriaRisco/" + CategoriaRiscoModel.Id);
                if (response.IsSuccessStatusCode)
                {
                    CategoriaRisco objCategoriaRisco = await response.Content.ReadFromJsonAsync<CategoriaRisco>();


                }
                // HTTP POST - define o produto
                var CategoriaRisco = new CategoriaRisco()
                {
                    Id = CategoriaRiscoModel.Id,
                    CT_A = CategoriaRiscoModel.CT_A,

                    BarragemId = CategoriaRiscoModel.BarragemId,
                    CT_B = CategoriaRiscoModel.CT_B,
                    CT_C = CategoriaRiscoModel.CT_C,
                    CT_D = CategoriaRiscoModel.CT_D,
                    CT_E = CategoriaRiscoModel.CT_E,
                    EC_H = CategoriaRiscoModel.EC_H,
                    EC_I = CategoriaRiscoModel.EC_I,
                    EC_J = CategoriaRiscoModel.EC_J,
                    EC_L = CategoriaRiscoModel.EC_L,
                    PS_N = CategoriaRiscoModel.PS_N,
                    PS_O = CategoriaRiscoModel.PS_O,
                    PS_P = CategoriaRiscoModel.PS_P,
                    PS_Q = CategoriaRiscoModel.PS_Q,


                    PS_R = CategoriaRiscoModel.PS_R,
                    ValorTotal = CategoriaRiscoModel.ValorTotal,
                    ValorTotalEC = CategoriaRiscoModel.ValorTotalEC,
                    DataAlteracao = DateTime.Now,
                    DataCadastro = DateTime.Now


                };
                response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/AdicionarCategoriaRisco", CategoriaRisco);
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
        public static async Task<bool> EditarCategoriaRisco(CategoriaRisco CategoriaRiscoModel)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdCategoriaRisco?id=" + CategoriaRiscoModel.Id);

                // HTTP POST - define o produto
                var CategoriaRisco = new CategoriaRisco()
                {
                    Id = CategoriaRiscoModel.Id,
                    CT_A = CategoriaRiscoModel.CT_A,

                    BarragemId = CategoriaRiscoModel.BarragemId,
                    CT_B = CategoriaRiscoModel.CT_B,
                    CT_C = CategoriaRiscoModel.CT_C,
                    CT_D = CategoriaRiscoModel.CT_D,
                    CT_E = CategoriaRiscoModel.CT_E,
                    EC_H = CategoriaRiscoModel.EC_H,
                    EC_I = CategoriaRiscoModel.EC_I,
                    EC_J = CategoriaRiscoModel.EC_J,
                    EC_L = CategoriaRiscoModel.EC_L,
                    PS_N = CategoriaRiscoModel.PS_N,
                    PS_O = CategoriaRiscoModel.PS_O,
                    PS_P = CategoriaRiscoModel.PS_P,
                    PS_Q = CategoriaRiscoModel.PS_Q,


                    PS_R = CategoriaRiscoModel.PS_R,
                    ValorTotal = CategoriaRiscoModel.ValorTotal,
                    ValorTotalEC = CategoriaRiscoModel.ValorTotalEC,
                    DataAlteracao = DateTime.Now
         



                };

                if (response.IsSuccessStatusCode)
                {
                    response = await cliente.PutAsJsonAsync(Infra.Constantes.URI + "/API/AtualizaCategoriaRisco/", CategoriaRisco);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public async Task<CategoriaRisco> EditCategoriaRisco(int id)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdCategoriaRisco?id=" + id);

                return await response.Content.ReadFromJsonAsync<CategoriaRisco>();

            }
        }
        public static async Task<bool> DeletarCategoriaRisco(CategoriaRisco model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdCategoriaRisco?id=" + model.Id);

              



                if (response.IsSuccessStatusCode)
                {
                 
                    response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/ExcluirCategoriaRisco", model);
                    return true;
                }
                return false;
            }
        }


    }
}

