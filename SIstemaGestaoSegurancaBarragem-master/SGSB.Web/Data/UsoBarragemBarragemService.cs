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
    public class UsoBarragemBarragemService
    {
        HttpClient http;
        public UsoBarragemBarragemService(HttpClient _http)
        {
            this.http = _http;
        }
        private readonly IJSRuntime js;


        public async Task<List<UsoBarragemBarragem>> GetUsoBarragemBarragem()
        {
            var objUsoBarragemBarragem = await http.GetFromJsonAsync<List<UsoBarragemBarragem>>( Infra.Constantes.URI +"/API/ListarUsoBarragemBarragem");

            return objUsoBarragemBarragem.ToList();

        }
        public async Task<UsoBarragemBarragem> GetUsoBarragemBarragemId(int id)
        {
            UsoBarragemBarragem objUsoBarragemBarragem = new UsoBarragemBarragem();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdUsoBarragemBarragem?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                     objUsoBarragemBarragem = await response.Content.ReadFromJsonAsync<UsoBarragemBarragem>();
                    return objUsoBarragemBarragem;

                }
            }
            return objUsoBarragemBarragem;
        }
        //public async Task<UsoBarragemBarragem> EditTipoCondicaoFundacao(int id)
        //{
        //    var objUsoBarragemBarragem = await http.GetFromJsonAsync<List<UsoBarragemBarragem>>("/API/ListarUsoBarragemBarragem");

        //    return objUsoBarragemBarragem.Where(i => i.Id == id).FirstOrDefault();

        //}
        //public async Task<UsoBarragemBarragem> VerTipoCondicaoFundacao(int id)
        //{
        //    var objUsoBarragemBarragem = await http.GetFromJsonAsync<List<UsoBarragemBarragem>>("/API/ListarUsoBarragemBarragem");

        //    return objUsoBarragemBarragem.Where(i => i.Id == id).FirstOrDefault();

        //}
        //public async Task<UsoBarragemBarragem> Delete(int id)
        //{
        //    var objUsoBarragemBarragem = await http.GetFromJsonAsync<List<UsoBarragemBarragem>>("/API/ListarUsoBarragemBarragem");

        //    return objUsoBarragemBarragem.Where(i => i.Id == id).FirstOrDefault();

        //}
        public async Task<int> GetProximoUsoBarragemBarragemAsync()
        {
            var objUsoBarragemBarragem = await http.GetFromJsonAsync<List<Model.UsoBarragemBarragem>>(Infra.Constantes.URI + "/API/ListarUsoBarragemBarragem");

            int proximoId = 0;

            if (objUsoBarragemBarragem.ToList().Count == 0)
            {
                proximoId = 1;
            }
            else
            {
                proximoId = objUsoBarragemBarragem.ToList().LastOrDefault().Id + 1;
            }

            return proximoId;

        }
        public static async Task<bool> CadastrarUsoBarragemBarragem(UsoBarragemBarragemModel model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                //HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/ListarUsoBarragemBarragem/" + model.Id);
                //if (response.IsSuccessStatusCode)
                //{
                //    UsoBarragemBarragem objUsoBarragemBarragem = await response.Content.ReadFromJsonAsync<UsoBarragemBarragem>();


                //}
                // HTTP POST - define o produto
                var usoBarragemBarragemModel = new UsoBarragemBarragemModel()
                {
                    BarragemId = model.BarragemId,
                    UsoBarragemId = model.UsoBarragemId,
                    DataAlteracao = DateTime.Now,
                    DataCadastro = DateTime.Now,


                };
                HttpResponseMessage response =  await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/AdicionarUsoBarragemBarragem", usoBarragemBarragemModel);
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
        public static async Task<bool> EditarUsoBarragemBarragem(UsoBarragemBarragemModel model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdUsoBarragemBarragem?id=" + model.Id);

                // HTTP POST - define o produto
                var usoBarragemBarragemModel = new UsoBarragemBarragemModel()
                {
                    BarragemId = model.BarragemId,
                    UsoBarragemId = model.UsoBarragemId,
                    DataAlteracao = DateTime.Now,
                


                };

                if (response.IsSuccessStatusCode)
                {
                    response = await cliente.PutAsJsonAsync(Infra.Constantes.URI + "/API/AtualizaUsoBarragemBarragem/", usoBarragemBarragemModel);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public async Task<UsoBarragemBarragem> EditUsoBarragemBarragem(int id)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdUsoBarragemBarragem?id=" + id);

                return await response.Content.ReadFromJsonAsync<UsoBarragemBarragem>();

            }
        }
        public static async Task<bool> DeletarUsoBarragemBarragem(UsoBarragemBarragem model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdUsoBarragemBarragem?id=" + model.Id);

              



                if (response.IsSuccessStatusCode)
                {
                 
                    response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/ExcluirUsoBarragemBarragem", model);
                    return true;
                }
                return false;
            }
        }
        public static async Task<List<UsoBarragemBarragemModel>> ListarUsoBarragemBarragemBarragemId(int idBarragem)
        {
            List<UsoBarragemBarragemModel> objUsoBarragemBarragem = new List<UsoBarragemBarragemModel>();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/ListarUsoBarragemBarragemBarragemId", idBarragem);
                if (response.IsSuccessStatusCode)
                {

                    objUsoBarragemBarragem = await response.Content.ReadFromJsonAsync<List<UsoBarragemBarragemModel>>();

                    return objUsoBarragemBarragem;
                }
            }
            return objUsoBarragemBarragem;
        }



    }
}

