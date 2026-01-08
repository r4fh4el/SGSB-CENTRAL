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
    public class VazaPicoService
    {
        HttpClient http;
        public VazaPicoService(HttpClient _http)
        {
            this.http = _http;
        }
        private readonly IJSRuntime js;
        public async Task<List<Barragem>> GetBarragemAsync()
        {
            var objBarragem = await http.GetFromJsonAsync<List<Barragem>>(Infra.Constantes.URI + "/API/ListarBarragem");

            return objBarragem.ToList();

        }


        public async Task<List<VazaPico>> GetVazaPico()
        {
            var objVazaPico = await http.GetFromJsonAsync<List<VazaPico>>( Infra.Constantes.URI +"/API/ListarVazaPico");

            return objVazaPico.ToList();

        }
        public async Task<VazaPico> GetVazaPicoId(int id)
        {
            VazaPico objVazaPico = new VazaPico();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdVazaPico?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                     objVazaPico = await response.Content.ReadFromJsonAsync<VazaPico>();
                    return objVazaPico;

                }
            }
            return objVazaPico;
        }
        //public async Task<VazaPico> EditTipoCondicaoFundacao(int id)
        //{
        //    var objVazaPico = await http.GetFromJsonAsync<List<VazaPico>>("/API/ListarVazaPico");

        //    return objVazaPico.Where(i => i.Id == id).FirstOrDefault();

        //}
        //public async Task<VazaPico> VerTipoCondicaoFundacao(int id)
        //{
        //    var objVazaPico = await http.GetFromJsonAsync<List<VazaPico>>("/API/ListarVazaPico");

        //    return objVazaPico.Where(i => i.Id == id).FirstOrDefault();

        //}
        //public async Task<VazaPico> Delete(int id)
        //{
        //    var objVazaPico = await http.GetFromJsonAsync<List<VazaPico>>("/API/ListarVazaPico");

        //    return objVazaPico.Where(i => i.Id == id).FirstOrDefault();

        //}

        public  async Task<bool> CadastrarVazaPico(VazaPico model)
        {
            using (var cliente = new HttpClient())
            {
                //cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/ListarVazaPico/" + model.Id);
                if (response.IsSuccessStatusCode)
                {
                    VazaPico objVazaPico = await response.Content.ReadFromJsonAsync<VazaPico>();


                }
                // HTTP POST - define o produto
                var VazaPico = new VazaPico()
                {
                    NomePropriedade = model.NomePropriedade,
                    valorVhid = model.valorVhid,
                    valorAS = model.valorAS,
                    valorHhid = model.valorHhid,
                    valorYmed = model.valorYmed,
                    BarragemId = model.BarragemId,
                    HidrogramaRupturaTriangularId = model.HidrogramaRupturaTriangularId,
                    valorHbarr = model.valorHbarr,  
                    valorLarguraBarragem = model.valorLarguraBarragem,
                    valorTempoRuptura = model.valorTempoRuptura,
                    DataCadastro = DateTime.Now,
                    DataAlteracao = DateTime.Now


                };
                response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/AdicionarVazaPico", VazaPico);
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
        public   async Task<bool> EditarVazaPico(VazaPico model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdVazaPico?id=" + model.Id);

                // HTTP POST - define o produto
                var VazaPico = new VazaPico()
                {
                    NomePropriedade = model.NomePropriedade,
                    valorVhid = model.valorVhid,
                    valorAS = model.valorAS,
                    valorHhid = model.valorHhid,
                    valorYmed = model.valorYmed,
                    BarragemId = model.BarragemId,
                    HidrogramaRupturaTriangularId = model.HidrogramaRupturaTriangularId,
                    valorHbarr = model.valorHbarr,
                    valorLarguraBarragem = model.valorLarguraBarragem,
                    valorTempoRuptura = model.valorTempoRuptura,
                    DataCadastro = DateTime.Now,
                    DataAlteracao = DateTime.Now



                };


                if (response.IsSuccessStatusCode)
                {
                    response = await cliente.PutAsJsonAsync(Infra.Constantes.URI + "/API/VerificarBarragemId/", VazaPico);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public async Task<VazaPico> EditVazaPico(int id)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdVazaPico?id=" + id);

                return await response.Content.ReadFromJsonAsync<VazaPico>();

            }
        }
        public  async Task<bool> DeletarVazaPico(VazaPico model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdVazaPico?id=" + model.Id);

              



                if (response.IsSuccessStatusCode)
                {
                 
                    response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/ExcluirVazaPico", model);
                    return true;
                }
                return false;
            }
        }


    }
}

