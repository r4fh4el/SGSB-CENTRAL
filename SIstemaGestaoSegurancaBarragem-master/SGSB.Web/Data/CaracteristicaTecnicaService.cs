using Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;

using System.Net.Http.Headers;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Linq;
using System;

namespace SGSB.Web.Data
{
    public class CaracteristicaTecnicaService
    {
        HttpClient http;
        public CaracteristicaTecnicaService(HttpClient _http)
        {
            this.http = _http;
        }
        private readonly IJSRuntime js;


        public async Task<List<CaracteristicaTecnica>> GetCaracteristicaTecnica()
        {
            var objCaracteristicaTecnica = await http.GetFromJsonAsync<List<CaracteristicaTecnica>>( Infra.Constantes.URI +"/API/ListarCaracteristicaTecnica");

            return objCaracteristicaTecnica.ToList();

        }
        public async Task<CaracteristicaTecnica> GetCaracteristicaTecnicaId(int id)
        {
            CaracteristicaTecnica objCaracteristicaTecnica = new CaracteristicaTecnica();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdCaracteristicaTecnica?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                     objCaracteristicaTecnica = await response.Content.ReadFromJsonAsync<CaracteristicaTecnica>();
                    return objCaracteristicaTecnica;

                }
            }
            return objCaracteristicaTecnica;
        }
        //public async Task<CaracteristicaTecnica> EditTipoCondicaoFundacao(int id)
        //{
        //    var objCaracteristicaTecnica = await http.GetFromJsonAsync<List<CaracteristicaTecnica>>("/API/ListarCaracteristicaTecnica");

        //    return objCaracteristicaTecnica.Where(i => i.Id == id).FirstOrDefault();

        //}
        //public async Task<CaracteristicaTecnica> VerTipoCondicaoFundacao(int id)
        //{
        //    var objCaracteristicaTecnica = await http.GetFromJsonAsync<List<CaracteristicaTecnica>>("/API/ListarCaracteristicaTecnica");

        //    return objCaracteristicaTecnica.Where(i => i.Id == id).FirstOrDefault();

        //}
        //public async Task<CaracteristicaTecnica> Delete(int id)
        //{
        //    var objCaracteristicaTecnica = await http.GetFromJsonAsync<List<CaracteristicaTecnica>>("/API/ListarCaracteristicaTecnica");

        //    return objCaracteristicaTecnica.Where(i => i.Id == id).FirstOrDefault();

        //}

        public static async Task<bool> CadastrarCaracteristicaTecnica(CaracteristicaTecnica caracteristicaTecnicaModel)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/ListarCaracteristicaTecnica/" + caracteristicaTecnicaModel.Id);
                if (response.IsSuccessStatusCode)
                {
                    CaracteristicaTecnica objCaracteristicaTecnica = await response.Content.ReadFromJsonAsync<CaracteristicaTecnica>();


                }
                // HTTP POST - define o produto
                var caracteristicaTecnica = new CaracteristicaTecnica()
                {
                    Id = caracteristicaTecnicaModel.Id,
                    ALT_Q1 = caracteristicaTecnicaModel.ALT_Q1,
                    ALT_Q2 = caracteristicaTecnicaModel.ALT_Q2,
                    ALT_Q3 = caracteristicaTecnicaModel.ALT_Q3,
                    ALT_Q4 = caracteristicaTecnicaModel.ALT_Q4,
                    ALT_Resposta = caracteristicaTecnicaModel.ALT_Resposta,
                    BarragemId = caracteristicaTecnicaModel.BarragemId,
                    COMP_Q1 = caracteristicaTecnicaModel.COMP_Q1,
                    COMP_Q2 = caracteristicaTecnicaModel.COMP_Q2,
                    COMP_Resposta = caracteristicaTecnicaModel.COMP_Resposta,
                    CTTotal = caracteristicaTecnicaModel.CTTotal,
                    DataAlteracao = DateTime.Now,
                    DataCadastro = DateTime.Now,
                    IB_Q1 = caracteristicaTecnicaModel.IB_Q1,
                    IB_Q2 = caracteristicaTecnicaModel.IB_Q2,
                    IB_Q3 = caracteristicaTecnicaModel.IB_Q3,
                    IB_Q4 = caracteristicaTecnicaModel.IB_Q4,
                    IB_Resposta = caracteristicaTecnicaModel.IB_Resposta,
                    TBMC_Q1 = caracteristicaTecnicaModel.TBMC_Q1,
                    TBMC_Q2 = caracteristicaTecnicaModel.TBMC_Q2,
                    TBMC_Q3 = caracteristicaTecnicaModel.TBMC_Q3,
                    TBMC_Q4 = caracteristicaTecnicaModel.TBMC_Q4,
                    TBMC_Resposta = caracteristicaTecnicaModel.TBMC_Resposta,
                    TF_Q1 = caracteristicaTecnicaModel.TF_Q1,
                    TF_Q2 = caracteristicaTecnicaModel.TF_Q2,
                    TF_Q3 = caracteristicaTecnicaModel.TF_Q3,
                    TF_Q4 = caracteristicaTecnicaModel.TF_Q4,
                    TF_Q5 = caracteristicaTecnicaModel.TF_Q5,
                    VP_Q1 = caracteristicaTecnicaModel.VP_Q1,
                    VP_Q2 = caracteristicaTecnicaModel.VP_Q2,
                    VP_Q3 = caracteristicaTecnicaModel.VP_Q3,
                    VP_Q4 = caracteristicaTecnicaModel.VP_Q4,
                    TF_Resposta = caracteristicaTecnicaModel.TF_Resposta,
                    VP_Resposta = caracteristicaTecnicaModel.VP_Resposta


                };
                response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/AdicionarCaracteristicaTecnica", caracteristicaTecnica);
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
        public static async Task<bool> EditarCaracteristicaTecnica(CaracteristicaTecnica caracteristicaTecnicaModel)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdCaracteristicaTecnica?id=" + caracteristicaTecnicaModel.Id);

                // HTTP POST - define o produto
                var caracteristicaTecnica = new CaracteristicaTecnica()
                {
                    Id = caracteristicaTecnicaModel.Id,
                    ALT_Q1 = caracteristicaTecnicaModel.ALT_Q1,
                    ALT_Q2 = caracteristicaTecnicaModel.ALT_Q2,
                    ALT_Q3 = caracteristicaTecnicaModel.ALT_Q3,
                    ALT_Q4 = caracteristicaTecnicaModel.ALT_Q4,
                    ALT_Resposta = caracteristicaTecnicaModel.ALT_Resposta,
                    BarragemId = caracteristicaTecnicaModel.BarragemId,
                    COMP_Q1 = caracteristicaTecnicaModel.COMP_Q1,
                    COMP_Q2 = caracteristicaTecnicaModel.COMP_Q2,
                    COMP_Resposta = caracteristicaTecnicaModel.COMP_Resposta,
                    CTTotal = caracteristicaTecnicaModel.CTTotal,
                    DataAlteracao = DateTime.Now,
                    DataCadastro = DateTime.Now,
                    IB_Q1 = caracteristicaTecnicaModel.IB_Q1,
                    IB_Q2 = caracteristicaTecnicaModel.IB_Q2,
                    IB_Q3 = caracteristicaTecnicaModel.IB_Q3,
                    IB_Q4 = caracteristicaTecnicaModel.IB_Q4,
                    IB_Resposta = caracteristicaTecnicaModel.IB_Resposta,
                    TBMC_Q1 = caracteristicaTecnicaModel.TBMC_Q1,
                    TBMC_Q2 = caracteristicaTecnicaModel.TBMC_Q2,
                    TBMC_Q3 = caracteristicaTecnicaModel.TBMC_Q3,
                    TBMC_Q4 = caracteristicaTecnicaModel.TBMC_Q4,
                    TBMC_Resposta = caracteristicaTecnicaModel.TBMC_Resposta,
                    TF_Q1 = caracteristicaTecnicaModel.TF_Q1,
                    TF_Q2 = caracteristicaTecnicaModel.TF_Q2,
                    TF_Q3 = caracteristicaTecnicaModel.TF_Q3,
                    TF_Q4 = caracteristicaTecnicaModel.TF_Q4,
                    TF_Q5 = caracteristicaTecnicaModel.TF_Q5,
                    VP_Q1 = caracteristicaTecnicaModel.VP_Q1,
                    VP_Q2 = caracteristicaTecnicaModel.VP_Q2,
                    VP_Q3 = caracteristicaTecnicaModel.VP_Q3,
                    VP_Q4 = caracteristicaTecnicaModel.VP_Q4,
                    TF_Resposta = caracteristicaTecnicaModel.TF_Resposta,
                    VP_Resposta = caracteristicaTecnicaModel.VP_Resposta


                };

                if (response.IsSuccessStatusCode)
                {
                    response = await cliente.PutAsJsonAsync(Infra.Constantes.URI + "/API/AtualizaCaracteristicaTecnica/", caracteristicaTecnica);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public async Task<CaracteristicaTecnica> EditCaracteristicaTecnica(int id)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdCaracteristicaTecnica?id=" + id);

                return await response.Content.ReadFromJsonAsync<CaracteristicaTecnica>();

            }
        }
        public static async Task<bool> DeletarCaracteristicaTecnica(CaracteristicaTecnica model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdCaracteristicaTecnica?id=" + model.Id);

              



                if (response.IsSuccessStatusCode)
                {
                 
                    response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/ExcluirCaracteristicaTecnica", model);
                    return true;
                }
                return false;
            }
        }


    }
}

