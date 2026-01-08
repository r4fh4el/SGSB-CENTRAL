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

namespace SGSB.Web.Data
{
    public class SistemaDrenagemBarragemService
    {
        HttpClient http;
        public SistemaDrenagemBarragemService(HttpClient _http)
        {
            this.http = _http;
        }
        private readonly IJSRuntime js;


        public async Task<List<SistemaDrenagemBarragem>> GetSistemaDrenagemBarragem()
        {
            var objSistemaDrenagemBarragem = await http.GetFromJsonAsync<List<SistemaDrenagemBarragem>>( Infra.Constantes.URI +"/API/ListarSistemaDrenagemBarragem");

            return objSistemaDrenagemBarragem.ToList();

        }
        public async Task<SistemaDrenagemBarragem> GetSistemaDrenagemBarragemId(int id)
        {
            SistemaDrenagemBarragem objSistemaDrenagemBarragem = new SistemaDrenagemBarragem();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdSistemaDrenagemBarragem?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                     objSistemaDrenagemBarragem = await response.Content.ReadFromJsonAsync<SistemaDrenagemBarragem>();
                    return objSistemaDrenagemBarragem;

                }
            }
            return objSistemaDrenagemBarragem;
        }
        //public async Task<SistemaDrenagemBarragem> EditTipoCondicaoFundacao(int id)
        //{
        //    var objSistemaDrenagemBarragem = await http.GetFromJsonAsync<List<SistemaDrenagemBarragem>>("/API/ListarSistemaDrenagemBarragem");

        //    return objSistemaDrenagemBarragem.Where(i => i.Id == id).FirstOrDefault();

        //}
        //public async Task<SistemaDrenagemBarragem> VerTipoCondicaoFundacao(int id)
        //{
        //    var objSistemaDrenagemBarragem = await http.GetFromJsonAsync<List<SistemaDrenagemBarragem>>("/API/ListarSistemaDrenagemBarragem");

        //    return objSistemaDrenagemBarragem.Where(i => i.Id == id).FirstOrDefault();

        //}
        //public async Task<SistemaDrenagemBarragem> Delete(int id)
        //{
        //    var objSistemaDrenagemBarragem = await http.GetFromJsonAsync<List<SistemaDrenagemBarragem>>("/API/ListarSistemaDrenagemBarragem");

        //    return objSistemaDrenagemBarragem.Where(i => i.Id == id).FirstOrDefault();

        //}
        public async Task<int> GetProximoSistemaDrenagemBarragemIdAsync()
        {
            var objSistemaDrenagemBarragem = await http.GetFromJsonAsync<List<Model.SistemaDrenagemBarragem>>(Infra.Constantes.URI + "/API/ListarSistemaDrenagemBarragem");

            int proximoId = 0;

            if (objSistemaDrenagemBarragem.ToList().Count == 0)
            {
                proximoId = 1;
            }
            else
            {
                proximoId = objSistemaDrenagemBarragem.ToList().LastOrDefault().Id + 1;
            }

            return proximoId;

        }
        public static async Task<bool> CadastrarSistemaDrenagemBarragem(SistemaDrenagemBarragemModel model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                //HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/ListarSistemaDrenagemBarragem/" + model.Id);
                //if (response.IsSuccessStatusCode)
                //{
                //    SistemaDrenagemBarragem objSistemaDrenagemBarragem = await response.Content.ReadFromJsonAsync<SistemaDrenagemBarragem>();


                //}
                // HTTP POST - define o produto
                var SistemaDrenagemBarragemModel = new SistemaDrenagemBarragemModel()
                {
                    BarragemId = model.BarragemId,  
                    SistemaDrenagemId = model.SistemaDrenagemId,
                    DataCadastro = DateTime.Now,
                    DataAlteracao = DateTime.Now,


                };
                HttpResponseMessage response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/AdicionarSistemaDrenagemBarragem", SistemaDrenagemBarragemModel);
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
        public static async Task<bool> EditarSistemaDrenagemBarragem(SistemaDrenagemBarragemModel model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
            
                // HTTP POST - define o produto
                var sistemaDrenagemBarragemModel = new SistemaDrenagemBarragemModel()
                {
                    BarragemId = model.BarragemId,
                    SistemaDrenagemId = model.Id,
                    DataCadastro = DateTime.Now,
                    DataAlteracao = DateTime.Now,
                    Id = model.Id


                };

                HttpResponseMessage response = await cliente.PutAsJsonAsync(Infra.Constantes.URI + "/API/AtualizaSistemaDrenagemBarragem/", sistemaDrenagemBarragemModel);

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
        public async Task<SistemaDrenagemBarragem> EditSistemaDrenagemBarragem(int id)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdSistemaDrenagemBarragem?id=" + id);

                return await response.Content.ReadFromJsonAsync<SistemaDrenagemBarragem>();

            }
        }
        public static async Task<bool> DeletarSistemaDrenagemBarragem(SistemaDrenagemBarragem model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdSistemaDrenagemBarragem?id=" + model.Id);

              



                if (response.IsSuccessStatusCode)
                {
                 
                    response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/ExcluirSistemaDrenagemBarragem", model);
                    return true;
                }
                return false;
            }
        }
        public static async Task<List<SistemaDrenagemBarragemModel>> ListarSistemaDrenagemBarragemBarragemId(int idBarragem)
        {
            List<SistemaDrenagemBarragemModel> objSistemaDrenagemBarragem = new List<SistemaDrenagemBarragemModel>();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/ListarSistemaDrenagemBarragemBarragemId", idBarragem);
                if (response.IsSuccessStatusCode)
                {

                    objSistemaDrenagemBarragem = await response.Content.ReadFromJsonAsync<List<SistemaDrenagemBarragemModel>>();

                    return objSistemaDrenagemBarragem;
                }
            }
            return objSistemaDrenagemBarragem;
        }

    }
}

