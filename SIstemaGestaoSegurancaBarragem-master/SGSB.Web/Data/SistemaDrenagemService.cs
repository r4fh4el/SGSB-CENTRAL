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
    public class SistemaDrenagemService
    {
        HttpClient http;
        public SistemaDrenagemService(HttpClient _http)
        {
            this.http = _http;
        }
        private readonly IJSRuntime js;


        public async Task<List<SistemaDrenagem>> GetSistemaDrenagem()
        {
            var objSistemaDrenagem = await http.GetFromJsonAsync<List<SistemaDrenagem>>( Infra.Constantes.URI +"/API/ListarSistemaDrenagem");

            return objSistemaDrenagem.ToList();

        }
        public async Task<SistemaDrenagem> GetSistemaDrenagemId(int id)
        {
            SistemaDrenagem objSistemaDrenagem = new SistemaDrenagem();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdSistemaDrenagem?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                     objSistemaDrenagem = await response.Content.ReadFromJsonAsync<SistemaDrenagem>();
                    return objSistemaDrenagem;

                }
            }
            return objSistemaDrenagem;
        }
        //public async Task<SistemaDrenagem> EditTipoCondicaoFundacao(int id)
        //{
        //    var objSistemaDrenagem = await http.GetFromJsonAsync<List<SistemaDrenagem>>("/API/ListarSistemaDrenagem");

        //    return objSistemaDrenagem.Where(i => i.Id == id).FirstOrDefault();

        //}
        //public async Task<SistemaDrenagem> VerTipoCondicaoFundacao(int id)
        //{
        //    var objSistemaDrenagem = await http.GetFromJsonAsync<List<SistemaDrenagem>>("/API/ListarSistemaDrenagem");

        //    return objSistemaDrenagem.Where(i => i.Id == id).FirstOrDefault();

        //}
        //public async Task<SistemaDrenagem> Delete(int id)
        //{
        //    var objSistemaDrenagem = await http.GetFromJsonAsync<List<SistemaDrenagem>>("/API/ListarSistemaDrenagem");

        //    return objSistemaDrenagem.Where(i => i.Id == id).FirstOrDefault();

        //}
        public async Task<int> GetProximoSistemaDrenagemIdAsync()
        {
            var objSistemaDrenagem = await http.GetFromJsonAsync<List<Model.SistemaDrenagem>>(Infra.Constantes.URI + "/API/ListarSistemaDrenagem");

            int proximoId = 0;

            if (objSistemaDrenagem.ToList().Count == 0)
            {
                proximoId = 1;
            }
            else
            {
                proximoId = objSistemaDrenagem.ToList().LastOrDefault().Id + 1;
            }

            return proximoId;

        }
        public static async Task<bool> CadastrarSistemaDrenagem(SistemaDrenagemModel model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/ListarSistemaDrenagem/" + model.Id);
                if (response.IsSuccessStatusCode)
                {
                    SistemaDrenagem objSistemaDrenagem = await response.Content.ReadFromJsonAsync<SistemaDrenagem>();


                }
                // HTTP POST - define o produto
                var tipoMaterialBarragem = new SistemaDrenagemModel()
                {
                    Nome = model.Nome


                };
                response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/AdicionarSistemaDrenagem", tipoMaterialBarragem);
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
        public static async Task<bool> EditarSistemaDrenagem(SistemaDrenagemModel model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdSistemaDrenagem?id=" + model.Id);

                // HTTP POST - define o produto
                var tipoMaterialBarragem = new SistemaDrenagem()
                {
                    Id = model.Id,
                    Nome = model.Nome,
                    DataCadastro = DateTime.Now,
                    DataAlteracao = DateTime.Now


                };


                if (response.IsSuccessStatusCode)
                {
                    response = await cliente.PutAsJsonAsync(Infra.Constantes.URI + "/API/AtualizaSistemaDrenagem/", tipoMaterialBarragem);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public async Task<SistemaDrenagem> EditSistemaDrenagem(int id)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdSistemaDrenagem?id=" + id);

                return await response.Content.ReadFromJsonAsync<SistemaDrenagem>();

            }
        }
        public static async Task<bool> DeletarSistemaDrenagem(SistemaDrenagem model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdSistemaDrenagem?id=" + model.Id);

              



                if (response.IsSuccessStatusCode)
                {
                 
                    response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/ExcluirSistemaDrenagem", model);
                    return true;
                }
                return false;
            }
        }


    }
}

