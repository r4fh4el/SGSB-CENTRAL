using Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;

using System.Net.Http.Headers;
using System.Security.Policy;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Linq;
using SGSB.Web.Models;

namespace SGSB.Web.Data
{
    public class IndiceCaracterizacaoBHService
    {
        HttpClient http;
        public IndiceCaracterizacaoBHService(HttpClient _http)
        {
            this.http = _http;
        }
        private readonly IJSRuntime js;


        public async Task<List<IndiceCaracterizacaoBH>> GetIndiceCaracterizacaoBH()
        {
            var objIndiceCaracterizacaoBH = await http.GetFromJsonAsync<List<IndiceCaracterizacaoBH>>( Infra.Constantes.URI +"/API/ListarIndiceCaracterizacaoBH");

            return objIndiceCaracterizacaoBH.ToList();

        }
        public async Task<IndiceCaracterizacaoBH> GetIndiceCaracterizacaoBHId(int id)
        {
            IndiceCaracterizacaoBH objIndiceCaracterizacaoBH = new IndiceCaracterizacaoBH();

            using (var cliente = new HttpClient())
            {
                //cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdIndiceCaracterizacaoBH?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                     objIndiceCaracterizacaoBH = await response.Content.ReadFromJsonAsync<IndiceCaracterizacaoBH>();
                    return objIndiceCaracterizacaoBH;

                }
            }
            return objIndiceCaracterizacaoBH;
        }

        public static async Task<bool> CadastrarIndiceCaracterizacaoBH(IndiceCaracterizacaoBHModel IndiceCaracterizacaoBHModel)
        {
            using (var cliente = new HttpClient())
            {
                //cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/ListarIndiceCaracterizacaoBH/" + IndiceCaracterizacaoBHModel.Id);
                if (response.IsSuccessStatusCode)
                {
                    IndiceCaracterizacaoBH objIndiceCaracterizacaoBH = await response.Content.ReadFromJsonAsync<IndiceCaracterizacaoBH>();


                }
                // HTTP POST - define o produto
              
                response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/AdicionarIndiceCaracterizacaoBH", IndiceCaracterizacaoBHModel);
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
        public static async Task<bool> EditarIndiceCaracterizacaoBH(IndiceCaracterizacaoBHModel IndiceCaracterizacaoBHModel)
        {
            using (var cliente = new HttpClient())
            {
                //cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdIndiceCaracterizacaoBH?id=" + IndiceCaracterizacaoBHModel.Id);

                // HTTP POST - define o produto
          
                if (response.IsSuccessStatusCode)
                {
                    response = await cliente.PutAsJsonAsync(Infra.Constantes.URI + "/API/AtualizaIndiceCaracterizacaoBH/", IndiceCaracterizacaoBHModel);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public async Task<IndiceCaracterizacaoBH> EditIndiceCaracterizacaoBH(int id)
        {
            using (var cliente = new HttpClient())
            {
                //cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdIndiceCaracterizacaoBH?id=" + id);

                return await response.Content.ReadFromJsonAsync<IndiceCaracterizacaoBH>();

            }
        }
        public static async Task<bool> DeletarIndiceCaracterizacaoBH(IndiceCaracterizacaoBH model)
        {
            using (var cliente = new HttpClient())
            {
                //cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdIndiceCaracterizacaoBH?id=" + model.Id);

              



                if (response.IsSuccessStatusCode)
                {
                 
                    response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/ExcluirIndiceCaracterizacaoBH", model);
                    return true;
                }
                return false;
            }
        }


    }
}

