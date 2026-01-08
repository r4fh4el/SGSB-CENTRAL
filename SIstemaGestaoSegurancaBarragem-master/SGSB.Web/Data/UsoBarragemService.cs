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
    public class UsoBarragemService
    {
        HttpClient http;
        public UsoBarragemService(HttpClient _http)
        {
            this.http = _http;
        }
        private readonly IJSRuntime js;


        public async Task<List<UsoBarragem>> GetUsoBarragem()
        {
            var objUsoBarragem = await http.GetFromJsonAsync<List<UsoBarragem>>( Infra.Constantes.URI +"/API/ListarUsoBarragem");

            return objUsoBarragem.ToList();

        }
        public async Task<UsoBarragem> GetUsoBarragemId(int id)
        {
            UsoBarragem objUsoBarragem = new UsoBarragem();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdUsoBarragem?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                     objUsoBarragem = await response.Content.ReadFromJsonAsync<UsoBarragem>();
                    return objUsoBarragem;

                }
            }
            return objUsoBarragem;
        }
        //public async Task<UsoBarragem> EditTipoCondicaoFundacao(int id)
        //{
        //    var objUsoBarragem = await http.GetFromJsonAsync<List<UsoBarragem>>("/API/ListarUsoBarragem");

        //    return objUsoBarragem.Where(i => i.Id == id).FirstOrDefault();

        //}
        //public async Task<UsoBarragem> VerTipoCondicaoFundacao(int id)
        //{
        //    var objUsoBarragem = await http.GetFromJsonAsync<List<UsoBarragem>>("/API/ListarUsoBarragem");

        //    return objUsoBarragem.Where(i => i.Id == id).FirstOrDefault();

        //}
        //public async Task<UsoBarragem> Delete(int id)
        //{
        //    var objUsoBarragem = await http.GetFromJsonAsync<List<UsoBarragem>>("/API/ListarUsoBarragem");

        //    return objUsoBarragem.Where(i => i.Id == id).FirstOrDefault();

        //}
        public async Task<int> GetProximoUsoBarragemAsync()
        {
            var objUsoBarragem = await http.GetFromJsonAsync<List<Model.UsoBarragem>>(Infra.Constantes.URI + "/API/ListarUsoBarragem");

            int proximoId = 0;

            if (objUsoBarragem.ToList().Count == 0)
            {
                proximoId = 1;
            }
            else
            {
                proximoId = objUsoBarragem.ToList().LastOrDefault().Id + 1;
            }

            return proximoId;

        }
        public static async Task<bool> CadastrarUsoBarragem(UsoBarragemModel model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/ListarUsoBarragem/" + model.Id);
                if (response.IsSuccessStatusCode)
                {
                    UsoBarragem objUsoBarragem = await response.Content.ReadFromJsonAsync<UsoBarragem>();


                }
                // HTTP POST - define o produto
                var tipoEmpreendedor = new UsoBarragemModel()
                {
                    Nome = model.Nome


                };
                response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/AdicionarUsoBarragem", tipoEmpreendedor);
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
        public static async Task<bool> EditarUsoBarragem(UsoBarragemModel model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdUsoBarragem?id=" + model.Id);

                // HTTP POST - define o produto
                var tipoEmpreendedor = new UsoBarragem()
                {
                    Id = model.Id,
                    Nome = model.Nome,
                    DataCadastro = DateTime.Now,
                    DataAlteracao = DateTime.Now


                };


                if (response.IsSuccessStatusCode)
                {
                    response = await cliente.PutAsJsonAsync(Infra.Constantes.URI + "/API/AtualizaUsoBarragem/", tipoEmpreendedor);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public async Task<UsoBarragem> EditUsoBarragem(int id)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdUsoBarragem?id=" + id);

                return await response.Content.ReadFromJsonAsync<UsoBarragem>();

            }
        }
        public static async Task<bool> DeletarUsoBarragem(UsoBarragem model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdUsoBarragem?id=" + model.Id);

              



                if (response.IsSuccessStatusCode)
                {
                 
                    response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/ExcluirUsoBarragem", model);
                    return true;
                }
                return false;
            }
        }


    }
}

