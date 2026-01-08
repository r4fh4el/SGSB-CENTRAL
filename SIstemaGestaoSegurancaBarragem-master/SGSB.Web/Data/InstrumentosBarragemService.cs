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
    public class InstrumentosBarragemService
    {
        HttpClient http;
        public InstrumentosBarragemService(HttpClient _http)
        {
            this.http = _http;
        }
        private readonly IJSRuntime js;


        public async Task<List<InstrumentosBarragem>> GetInstrumentosBarragem()
        {
            var objInstrumentosBarragem = await http.GetFromJsonAsync<List<InstrumentosBarragem>>( Infra.Constantes.URI +"/API/ListarInstrumentosBarragem");

            return objInstrumentosBarragem.ToList();

        }

        public async Task<int> GetProximoInstrumentosBarragemIdAsync()
        {
            var objInstrumentosBarragem = await http.GetFromJsonAsync<List<Model.InstrumentosBarragem>>(Infra.Constantes.URI + "/API/ListarInstrumentosBarragem");

            int proximoId = 0;

            if (objInstrumentosBarragem.ToList().Count == 0)
            {
                proximoId = 1;
            }
            else
            {
                proximoId = objInstrumentosBarragem.ToList().LastOrDefault().Id + 1;
            }

            return proximoId;

        }

        public async Task<InstrumentosBarragem> GetInstrumentosBarragemId(int id)
        {
            InstrumentosBarragem objInstrumentosBarragem = new InstrumentosBarragem();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdInstrumentosBarragem?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                     objInstrumentosBarragem = await response.Content.ReadFromJsonAsync<InstrumentosBarragem>();
                    return objInstrumentosBarragem;

                }
            }
            return objInstrumentosBarragem;
        }
        //public async Task<InstrumentosBarragem> EditTipoCondicaoFundacao(int id)
        //{
        //    var objInstrumentosBarragem = await http.GetFromJsonAsync<List<InstrumentosBarragem>>("/API/ListarInstrumentosBarragem");

        //    return objInstrumentosBarragem.Where(i => i.Id == id).FirstOrDefault();

        //}
        //public async Task<InstrumentosBarragem> VerTipoCondicaoFundacao(int id)
        //{
        //    var objInstrumentosBarragem = await http.GetFromJsonAsync<List<InstrumentosBarragem>>("/API/ListarInstrumentosBarragem");

        //    return objInstrumentosBarragem.Where(i => i.Id == id).FirstOrDefault();

        //}
        //public async Task<InstrumentosBarragem> Delete(int id)
        //{
        //    var objInstrumentosBarragem = await http.GetFromJsonAsync<List<InstrumentosBarragem>>("/API/ListarInstrumentosBarragem");

        //    return objInstrumentosBarragem.Where(i => i.Id == id).FirstOrDefault();

        //}

        public static async Task<bool> CadastrarInstrumentosBarragem(InstrumentosBarragemModel model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
             
                // HTTP POST - define o produto
                var instrumentosBarragem = new InstrumentosBarragemModel()
                {
                    BarragemId = model.BarragemId,
                    DataAlteracao = model.DataAlteracao, 
                    DataCadastro = model.DataCadastro,
                    InstrumentosId = model.InstrumentosId


                };
                HttpResponseMessage response  = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/AdicionarInstrumentosBarragem", instrumentosBarragem);
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
        public static async Task<bool> EditarInstrumentosBarragem(InstrumentosBarragemModel model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                //HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdInstrumentosBarragem?id=" + model.Id);

                // HTTP POST - define o produto
                var tipoMaterialBarragem = new InstrumentosBarragem()
                {
                    BarragemId = model.BarragemId,
                    DataAlteracao = model.DataAlteracao,
                    DataCadastro = model.DataCadastro,
                    InstrumentosId = model.InstrumentosId,
                    Id = model.Id


                };
                HttpResponseMessage response = await cliente.PutAsJsonAsync(Infra.Constantes.URI + "/API/AtualizaInstrumentosBarragem/", tipoMaterialBarragem);


                if (response.IsSuccessStatusCode)
                {
                        return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public async Task<InstrumentosBarragem> EditInstrumentosBarragem(int id)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdInstrumentosBarragem?id=" + id);

                return await response.Content.ReadFromJsonAsync<InstrumentosBarragem>();

            }
        }
        public static async Task<bool> DeletarInstrumentosBarragem(InstrumentosBarragem model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdInstrumentosBarragem?id=" + model.Id);

              



                if (response.IsSuccessStatusCode)
                {
                 
                    response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/ExcluirInstrumentosBarragem", model);
                    return true;
                }
                return false;
            }
        }

        public static async Task<List<InstrumentosBarragemModel>> ListarInstrumentosBarragemBarragemId(int idBarragem)
        {
            List<InstrumentosBarragemModel> objInstrumentosBarragem = new List<InstrumentosBarragemModel>();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/ListarInstrumentosBarragemBarragemId", idBarragem);
                if (response.IsSuccessStatusCode)
                {

                    objInstrumentosBarragem = await response.Content.ReadFromJsonAsync<List<InstrumentosBarragemModel>>();

                    return objInstrumentosBarragem;
                }
            }
            return objInstrumentosBarragem;
        }

    }
}

