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
namespace SGSB.Web.Data
{
    public  class TempoRupturaService
    {
        HttpClient http;
        public TempoRupturaService(HttpClient _http)
        {
            this.http = _http;
        }
        private readonly IJSRuntime js;
        public async Task<List<Barragem>> GetBarragemAsync()
        {
            var objBarragem = await http.GetFromJsonAsync<List<Barragem>>(Infra.Constantes.URI + "/API/ListarBarragem");

            return objBarragem.ToList();

        }


        public static async Task<List<TempoRuptura>> GetTempoRuptura()
        {

            using (var http = new HttpClient())
            {
                var objTempoRuptura = await http.GetFromJsonAsync<List<TempoRuptura>>(Infra.Constantes.URI + "/API/ListarTempoRuptura");
            
            return objTempoRuptura.ToList();
            }

        }
        public async Task<TempoRuptura> GetTempoRupturaId(int id)
        {
            TempoRuptura objTempoRuptura = new TempoRuptura();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdTempoRuptura?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                     objTempoRuptura = await response.Content.ReadFromJsonAsync<TempoRuptura>();
                    return objTempoRuptura;

                }
            }
            return objTempoRuptura;
        }
        //public async Task<TempoRuptura> EditTipoCondicaoFundacao(int id)
        //{
        //    var objTempoRuptura = await http.GetFromJsonAsync<List<TempoRuptura>>("/API/ListarTempoRuptura");

        //    return objTempoRuptura.Where(i => i.Id == id).FirstOrDefault();

        //}
        //public async Task<TempoRuptura> VerTipoCondicaoFundacao(int id)
        //{
        //    var objTempoRuptura = await http.GetFromJsonAsync<List<TempoRuptura>>("/API/ListarTempoRuptura");

        //    return objTempoRuptura.Where(i => i.Id == id).FirstOrDefault();

        //}
        //public async Task<TempoRuptura> Delete(int id)
        //{
        //    var objTempoRuptura = await http.GetFromJsonAsync<List<TempoRuptura>>("/API/ListarTempoRuptura");

        //    return objTempoRuptura.Where(i => i.Id == id).FirstOrDefault();

        //}

        public  static async Task<bool> CadastrarTempoRuptura(TempoRuptura model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/ListarTempoRuptura/" + model.Id);
                if (response.IsSuccessStatusCode)
                {
                    TempoRuptura objTempoRuptura = await response.Content.ReadFromJsonAsync<TempoRuptura>();


                }
                // HTTP POST - define o produto
                var TempoRuptura = new TempoRuptura()
                {
                    NomePropriedade = model.NomePropriedade,
                    BarragemId = model.BarragemId,
                    valorTempoRuptura = model.valorTempoRuptura,
                    Id = model.Id,
                    Barragem = model.Barragem,
                    DataCadastro = DateTime.Now,
                    DataAlteracao = DateTime.Now


                };
                response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/AdicionarTempoRuptura", TempoRuptura);
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
        public  async Task<bool> EditarTempoRuptura(TempoRuptura model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdTempoRuptura?id=" + model.Id);

                // HTTP POST - define o produto
                var TempoRuptura = new TempoRuptura()
                {
                    NomePropriedade = model.NomePropriedade,
                    BarragemId = model.BarragemId,
                    valorTempoRuptura = model.valorTempoRuptura,
                    Id = model.Id,
                    Barragem = model.Barragem,
                    DataAlteracao = DateTime.Now


                };


                if (response.IsSuccessStatusCode)
                {
                    response = await cliente.PutAsJsonAsync(Infra.Constantes.URI + "/API/AtualizaTempoRuptura/", TempoRuptura);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public async Task<TempoRuptura> EditTempoRuptura(int id)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdTempoRuptura?id=" + id);

                return await response.Content.ReadFromJsonAsync<TempoRuptura>();

            }
        }
        public  async Task<bool> DeletarTempoRuptura(TempoRuptura model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdTempoRuptura?id=" + model.Id);

              



                if (response.IsSuccessStatusCode)
                {
                 
                    response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/ExcluirTempoRuptura", model);
                    return true;
                }
                return false;
            }
        }


    }
}

