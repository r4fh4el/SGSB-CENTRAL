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
    public class CaracterizacaoAreaJusanteBarragemService
    {

        HttpClient http;
        public CaracterizacaoAreaJusanteBarragemService(HttpClient _http)
        {
            this.http = _http;
        }
        private readonly IJSRuntime js;

        public async Task<List<Model.CaracterizacaoAreaJusanteBarragem>> GetCaracterizacaoAreaJusanteBarragemAsync()
        {
            var objCaracterizacaoAreaJusanteBarragem = await http.GetFromJsonAsync<List<Model.CaracterizacaoAreaJusanteBarragem>>(Infra.Constantes.URI + "/API/ListarCaracterizacaoAreaJusanteBarragem");

            return objCaracterizacaoAreaJusanteBarragem.ToList();

        }

        public async Task<int> GetProximoCaracterizacaoAreaJusanteBarragemIdAsync()
        {
            var objCaracterizacaoAreaJusanteBarragem = await http.GetFromJsonAsync<List<Model.CaracterizacaoAreaJusanteBarragem>>(Infra.Constantes.URI + "/API/ListarCaracterizacaoAreaJusanteBarragem");

            int proximoId = 0;

            if (objCaracterizacaoAreaJusanteBarragem.ToList().Count == 0)
            {
                proximoId = 1;
            }
            else
            {
                proximoId = objCaracterizacaoAreaJusanteBarragem.ToList().LastOrDefault().Id + 1;
            }

            return proximoId;

        }
        public async Task<int> GetProximoCaracterizacaoAreaJusanteBarragemAsync()
        {
            var objCaracterizacaoAreaJusanteBarragem = await http.GetFromJsonAsync<List<Model.CaracterizacaoAreaJusanteBarragem>>(Infra.Constantes.URI + "/API/ListarCaracterizacaoAreaJusanteBarragem");

            int proximoId = 0;

            if (objCaracterizacaoAreaJusanteBarragem.ToList().Count == 0)
            {
                proximoId = 1;
            }
            else
            {
                proximoId = objCaracterizacaoAreaJusanteBarragem.ToList().LastOrDefault().Id + 1;
            }

            return proximoId;

        }


        public static async Task<Model.CaracterizacaoAreaJusanteBarragem> GetCaracterizacaoAreaJusanteBarragemId(int id)
        {
            Model.CaracterizacaoAreaJusanteBarragem objCaracterizacaoAreaJusanteBarragem = new Model.CaracterizacaoAreaJusanteBarragem();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdCaracterizacaoAreaJusanteBarragem?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                    objCaracterizacaoAreaJusanteBarragem = await response.Content.ReadFromJsonAsync<Model.CaracterizacaoAreaJusanteBarragem>();
                    return objCaracterizacaoAreaJusanteBarragem;

                }
            }
            return objCaracterizacaoAreaJusanteBarragem;
        }
        public async Task<Model.CaracterizacaoAreaJusanteBarragem> VerCaracterizacaoAreaJusanteBarragem(int id)
        {
            Model.CaracterizacaoAreaJusanteBarragem objCaracterizacaoAreaJusanteBarragem = new Model.CaracterizacaoAreaJusanteBarragem();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdCaracterizacaoAreaJusanteBarragem?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                    objCaracterizacaoAreaJusanteBarragem = await response.Content.ReadFromJsonAsync<Model.CaracterizacaoAreaJusanteBarragem>();
                    return objCaracterizacaoAreaJusanteBarragem;

                }
            }
            return objCaracterizacaoAreaJusanteBarragem;
        }
        public static async Task<bool> EditarCaracterizacaoAreaJusanteBarragem(CaracterizacaoAreaJusanteBarragemModel model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdCaracterizacaoAreaJusanteBarragem?id=" + model.Id);

                // HTTP POST - define o produto



                if (response.IsSuccessStatusCode)
                {
                    response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/AtualizaCaracterizacaoAreaJusanteBarragem/", model);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        public async Task<Model.CaracterizacaoAreaJusanteBarragem> EditCaracterizacaoAreaJusanteBarragem(int id)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdCaracterizacaoAreaJusanteBarragem?id=" + id);

                return await response.Content.ReadFromJsonAsync<Model.CaracterizacaoAreaJusanteBarragem>();

            }
        }

        public static async Task<bool> DeletarCaracterizacaoAreaJusanteBarragem(Model.CaracterizacaoAreaJusanteBarragem model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdCaracterizacaoAreaJusanteBarragem?id=" + model.Id);

                if (response.IsSuccessStatusCode)
                {

                    response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/ExcluirCaracterizacaoAreaJusanteBarragem", model);
                    return true;
                }
                return false;
            }
        }
        public static async Task<bool> CadastrarCaracterizacaoAreaJusanteBarragem(CaracterizacaoAreaJusanteBarragemModel model)
        {
            using (var cliente = new HttpClient())
            {
                //cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                //HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/ListarCaracterizacaoAreaJusanteBarragem/");

                // HTTP POST - define o produto
                var caracterizacaoAreaJusanteBarragem = new CaracterizacaoAreaJusanteBarragemModel()
                {
                    BarragemId = model.BarragemId,
                    DistantciaKm = model.DistantciaKm,
                
                    TipoEdificacaoId = model.TipoEdificacaoId
               
                };
                HttpResponseMessage response =   await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/AdicionaCaracterizacaoAreaJusanteBarragem/", caracterizacaoAreaJusanteBarragem);
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

        public static async Task<CaracterizacaoAreaJusanteBarragemModel> ListarCaracterizacaoAreaJusanteBarragemBarragemId(int idBarragem)
        {
            List<CaracterizacaoAreaJusanteBarragemModel> objCaracterizacaoAreaJusanteBarragem = new List<CaracterizacaoAreaJusanteBarragemModel>();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/ListarCaracterizacaoAreaJusanteBarragemBarragemId", idBarragem);
                if (response.IsSuccessStatusCode)
                {

                    objCaracterizacaoAreaJusanteBarragem = await response.Content.ReadFromJsonAsync<List<CaracterizacaoAreaJusanteBarragemModel>>();

                    return objCaracterizacaoAreaJusanteBarragem.FirstOrDefault();
                }
            }
            return objCaracterizacaoAreaJusanteBarragem.FirstOrDefault();
        }
    }
}
