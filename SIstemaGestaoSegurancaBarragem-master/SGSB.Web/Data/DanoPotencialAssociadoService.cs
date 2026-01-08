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
    public class DanoPotencialAssociadoService
    {
        HttpClient http;
        public DanoPotencialAssociadoService(HttpClient _http)
        {
            this.http = _http;
        }
        private readonly IJSRuntime js;


        public async Task<List<DanoPotencialAssociado>> GetDanoPotencialAssociado()
        {
            var objDanoPotencialAssociado = await http.GetFromJsonAsync<List<DanoPotencialAssociado>>( Infra.Constantes.URI +"/API/ListarDanoPotencialAssociado");

            return objDanoPotencialAssociado.ToList();

        }

      
        public async Task<DanoPotencialAssociado> GetDanoPotencialAssociadoId(int id)
        {
            DanoPotencialAssociado objDanoPotencialAssociado = new DanoPotencialAssociado();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdDanoPotencialAssociado?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                     objDanoPotencialAssociado = await response.Content.ReadFromJsonAsync<DanoPotencialAssociado>();
                    return objDanoPotencialAssociado;

                }
            }
            return objDanoPotencialAssociado;
        }
        public async Task<DanoPotencialAssociado> GetDanoPotencialAssociadoBarragemId(int id)
        {
            DanoPotencialAssociado objDanoPotencialAssociado = new DanoPotencialAssociado();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/GetDanoPotencialAssociadoBarragemId?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
                    {
                        objDanoPotencialAssociado = await response.Content.ReadFromJsonAsync<DanoPotencialAssociado>();
                    }
                  
                    return objDanoPotencialAssociado;

                }
            }
            return objDanoPotencialAssociado;
        }
        //public async Task<DanoPotencialAssociado> EditTipoCondicaoFundacao(int id)
        //{
        //    var objDanoPotencialAssociado = await http.GetFromJsonAsync<List<DanoPotencialAssociado>>("/API/ListarDanoPotencialAssociado");

        //    return objDanoPotencialAssociado.Where(i => i.Id == id).FirstOrDefault();

        //}
        //public async Task<DanoPotencialAssociado> VerTipoCondicaoFundacao(int id)
        //{
        //    var objDanoPotencialAssociado = await http.GetFromJsonAsync<List<DanoPotencialAssociado>>("/API/ListarDanoPotencialAssociado");

        //    return objDanoPotencialAssociado.Where(i => i.Id == id).FirstOrDefault();

        //}
        //public async Task<DanoPotencialAssociado> Delete(int id)
        //{
        //    var objDanoPotencialAssociado = await http.GetFromJsonAsync<List<DanoPotencialAssociado>>("/API/ListarDanoPotencialAssociado");

        //    return objDanoPotencialAssociado.Where(i => i.Id == id).FirstOrDefault();

        //}

        public static async Task<bool> CadastrarDanoPotencialAssociado(DanoPotencialAssociado danoPotencialAssociadoModel)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/ListarDanoPotencialAssociado/" + danoPotencialAssociadoModel.Id);
                if (response.IsSuccessStatusCode)
                {
                    DanoPotencialAssociado objDanoPotencialAssociado = await response.Content.ReadFromJsonAsync<DanoPotencialAssociado>();


                }
                // HTTP POST - define o produto
                var danoPotencialAssociado = new DanoPotencialAssociado()
                {
                    Id = danoPotencialAssociadoModel.Id,
                    IA_Q2 = danoPotencialAssociadoModel.IA_Q2,

                    BarragemId = danoPotencialAssociadoModel.BarragemId,
                    DpaTotal = danoPotencialAssociadoModel.DpaTotal,
                    IA_Q1 = danoPotencialAssociadoModel.IA_Q1,
                    ISE_Q1 = danoPotencialAssociadoModel.ISE_Q1,
                    IA_Resposta = danoPotencialAssociadoModel.IA_Resposta,
                    ISE_Q2 = danoPotencialAssociadoModel.ISE_Q2,
                    ISE_Q3 = danoPotencialAssociadoModel.ISE_Q3,
                    ISE_Resposta = danoPotencialAssociadoModel.ISE_Resposta,
                    PPV_Q1 = danoPotencialAssociadoModel.PPV_Q1,
                    PPV_Q2 = danoPotencialAssociadoModel.PPV_Q2,
                    PPV_Q3 = danoPotencialAssociadoModel.PPV_Q3,
                    PPV_Q4 = danoPotencialAssociadoModel.PPV_Q4,
                    PPV_Resposta = danoPotencialAssociadoModel.PPV_Resposta,
                    VTR_Q1 = danoPotencialAssociadoModel.VTR_Q1,
                    VTR_Q2 = danoPotencialAssociadoModel.VTR_Q2,
                    VTR_Q3 = danoPotencialAssociadoModel.VTR_Q3,
                    VTR_Q4 = danoPotencialAssociadoModel.VTR_Q4,

                    VTR_Resposta = danoPotencialAssociadoModel.ISE_Resposta,


                };
                response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/AdicionarDanoPotencialAssociado", danoPotencialAssociado);
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
        public static async Task<bool> EditarDanoPotencialAssociado(DanoPotencialAssociado danoPotencialAssociadoModel)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdDanoPotencialAssociado?id=" + danoPotencialAssociadoModel.Id);

                // HTTP POST - define o produto
                var danoPotencialAssociado = new DanoPotencialAssociado()
                {
                    Id = danoPotencialAssociadoModel.Id,
                    IA_Q2 = danoPotencialAssociadoModel.IA_Q2,

                    BarragemId = danoPotencialAssociadoModel.BarragemId,
                    DpaTotal = danoPotencialAssociadoModel.DpaTotal,
                    IA_Q1 = danoPotencialAssociadoModel.IA_Q1,
                    ISE_Q1 = danoPotencialAssociadoModel.ISE_Q1,
                    IA_Resposta = danoPotencialAssociadoModel.IA_Resposta,
                    ISE_Q2 = danoPotencialAssociadoModel.ISE_Q2,
                    ISE_Q3 = danoPotencialAssociadoModel.ISE_Q3,
                    ISE_Resposta = danoPotencialAssociadoModel.ISE_Resposta,
                    PPV_Q1 = danoPotencialAssociadoModel.PPV_Q1,
                    PPV_Q2 = danoPotencialAssociadoModel.PPV_Q2,
                    PPV_Q3 = danoPotencialAssociadoModel.PPV_Q3,
                    PPV_Q4 = danoPotencialAssociadoModel.PPV_Q4,
                    PPV_Resposta = danoPotencialAssociadoModel.PPV_Resposta,
                    VTR_Q1 = danoPotencialAssociadoModel.VTR_Q1,
                    VTR_Q2 = danoPotencialAssociadoModel.VTR_Q2,
                    VTR_Q3 = danoPotencialAssociadoModel.VTR_Q3,
                    VTR_Q4 = danoPotencialAssociadoModel.VTR_Q4,

                    VTR_Resposta = danoPotencialAssociadoModel.ISE_Resposta,


                };

                if (response.IsSuccessStatusCode)
                {
                    response = await cliente.PutAsJsonAsync(Infra.Constantes.URI + "/API/AtualizaDanoPotencialAssociado/", danoPotencialAssociado);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public async Task<DanoPotencialAssociado> EditDanoPotencialAssociado(int id)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdDanoPotencialAssociado?id=" + id);

                return await response.Content.ReadFromJsonAsync<DanoPotencialAssociado>();

            }
        }
        public static async Task<bool> DeletarDanoPotencialAssociado(DanoPotencialAssociado model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdDanoPotencialAssociado?id=" + model.Id);

              



                if (response.IsSuccessStatusCode)
                {
                 
                    response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/ExcluirDanoPotencialAssociado", model);
                    return true;
                }
                return false;
            }
        }


    }
}

