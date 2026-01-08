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
    public class InspecoesService
    {

        HttpClient http;
        public InspecoesService(HttpClient _http)
        {
            this.http = _http;
        }
        private readonly IJSRuntime js;

        public async Task<List<Model.Inspecoes>> GetInspecoesAsync()
        {
            var objInspecoes = await http.GetFromJsonAsync<List<Model.Inspecoes>>(Infra.Constantes.URI + "/API/ListarInspecoes");

            return objInspecoes.ToList();

        }

        public async Task<int> GetProximoInspecoesIdAsync()
        {
            var objInspecoes = await http.GetFromJsonAsync<List<Model.Inspecoes>>(Infra.Constantes.URI + "/API/ListarInspecoes");

            int proximoId = 0;

            if (objInspecoes.ToList().Count == 0)
            {
                proximoId = 1;
            }
            else
            {
                proximoId = objInspecoes.ToList().LastOrDefault().Id + 1;
            }

            return proximoId;

        }
 


        public static async Task<Model.Inspecoes> GetInspecoesId(int id)
        {
            Model.Inspecoes objInspecoes = new Model.Inspecoes();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdInspecoes?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                    objInspecoes = await response.Content.ReadFromJsonAsync<Model.Inspecoes>();
                    return objInspecoes;

                }
            }
            return objInspecoes;
        }
        public async Task<Model.Inspecoes> VerInspecoes(int id)
        {
            Model.Inspecoes objInspecoes = new Model.Inspecoes();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdInspecoes?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                    objInspecoes = await response.Content.ReadFromJsonAsync<Model.Inspecoes>();
                    return objInspecoes;

                }
            }
            return objInspecoes;
        }
        public static async Task<bool> EditarInspecoes(InspecoesModel model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdInspecoes?id=" + model.Id);

                // HTTP POST - define o produto
         


                if (response.IsSuccessStatusCode)
                {
                    response = await cliente.PutAsJsonAsync(Infra.Constantes.URI + "/API/AtualizaInspecoes/", model);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        public async Task<Model.Inspecoes> EditInspecoes(int id)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdInspecoes?id=" + id);

                return await response.Content.ReadFromJsonAsync<Model.Inspecoes>();

            }
        }

        public static async Task<bool> DeletarInspecoes(Model.Inspecoes model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdInspecoes?id=" + model.Id);
                
                if (response.IsSuccessStatusCode)
                {

                    response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/ExcluirInspecoes", model);
                    return true;
                }
                return false;
            }
        }
        public static async Task<bool> CadastrarInspecoes(InspecoesModel model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
               
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/ListarInspecoes/");

                // HTTP POST - define o produto
                var inspecoes = new InspecoesModel()
                {
                  

                    DataAlteracao = model.DataAlteracao,

                DataCadastro= model.DataCadastro,   
                BarragemId= model.BarragemId,   
                DataEstudoRompimento = model.DataEstudoRompimento,
                DataPlanoAcaoEmergencia = model.DataPlanoAcaoEmergencia,
                DataRevisaoPeriodicaRecente = model.DataRevisaoPeriodicaRecente,
                DataUltimaInspecaoEspecial = model.DataUltimaInspecaoEspecial,
                EnumFrequencia = model.EnumFrequencia,
                PossuiEstudoRompimento= model.PossuiEstudoRompimento,
                Nome_Setor = model.Nome_Setor,
                PossuiPae =model.PossuiPae
              

                };
                response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/AdicionarInspecoes/", inspecoes);
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

        public static async Task<InspecoesModel> ListarInspecoesBarragemId(int idBarragem)
        {
            List<InspecoesModel> objInspecoes = new List<InspecoesModel>();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/ListarInspecoesBarragemId", idBarragem);
                if (response.IsSuccessStatusCode)
                {

                    objInspecoes = await response.Content.ReadFromJsonAsync<List<InspecoesModel>>();

                    return objInspecoes.FirstOrDefault();
                }
            }
            return objInspecoes.FirstOrDefault();
        }
    }
}
