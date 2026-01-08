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
    public class HidrogramaParabolicoService
    {
        HttpClient http;
        public HidrogramaParabolicoService(HttpClient _http)
        {
            this.http = _http;
        }
        private readonly IJSRuntime js;
        public async Task<List<Barragem>> GetBarragemAsync()
        {
            var objBarragem = await http.GetFromJsonAsync<List<Barragem>>(Infra.Constantes.URI + "/API/ListarBarragem");

            return objBarragem.ToList();

        }


        public async Task<List<HidrogramaParabolico>> GetHidrogramaParabolico()
        {
            var objHidrogramaParabolico = await http.GetFromJsonAsync<List<HidrogramaParabolico>>( Infra.Constantes.URI +"/API/ListarHidrogramaParabolico");

            return objHidrogramaParabolico.ToList();

        }
        public async Task<HidrogramaParabolico> GetHidrogramaParabolicoId(int id)
        {
            HidrogramaParabolico objHidrogramaParabolico = new HidrogramaParabolico();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdHidrogramaParabolico?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                     objHidrogramaParabolico = await response.Content.ReadFromJsonAsync<HidrogramaParabolico>();
                    return objHidrogramaParabolico;

                }
            }
            return objHidrogramaParabolico;
        }

        public  async Task<bool> CadastrarHidrogramaParabolico(HidrogramaParabolico model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/ListarHidrogramaParabolico/" + model.Id);
                if (response.IsSuccessStatusCode)
                {
                    HidrogramaParabolico objHidrogramaParabolico = await response.Content.ReadFromJsonAsync<HidrogramaParabolico>();


                }
                // HTTP POST - define o produto
                var HidrogramaParabolico = new HidrogramaParabolico()
                {
                    NomePropriedade = model.NomePropriedade,
                    valorQp = model.valorQp,
                    valorTempoHora = model.valorTempoHora,
                    valorVazao = model.valorVazao,
                    BarragemId = model.BarragemId,
                    Id = model.Id,
                    DataCadastro = DateTime.Now,
                    DataAlteracao = DateTime.Now


                };
                response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/AdicionarHidrogramaParabolico", HidrogramaParabolico);
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
        public  async Task<bool> EditarHidrogramaParabolico(HidrogramaParabolico model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdHidrogramaParabolico?id=" + model.Id);

                // HTTP POST - define o produto
                var HidrogramaParabolico = new HidrogramaParabolico()
              {
                    NomePropriedade = model.NomePropriedade,
                    valorQp = model.valorQp,
                    valorTempoHora = model.valorTempoHora,
                    valorVazao = model.valorVazao,
                    BarragemId = model.BarragemId,
                    Id = model.Id,
                    DataAlteracao = DateTime.Now


                };


                if (response.IsSuccessStatusCode)
                {
                    response = await cliente.PutAsJsonAsync(Infra.Constantes.URI + "/API/AtualizaHidrogramaParabolico/", HidrogramaParabolico);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public async Task<HidrogramaParabolico> EditHidrogramaParabolico(int id)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdHidrogramaParabolico?id=" + id);

                return await response.Content.ReadFromJsonAsync<HidrogramaParabolico>();

            }
        }
        public  async Task<bool> DeletarHidrogramaParabolico(HidrogramaParabolico model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdHidrogramaParabolico?id=" + model.Id);

              



                if (response.IsSuccessStatusCode)
                {
                 
                    response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/ExcluirHidrogramaParabolico", model);
                    return true;
                }
                return false;
            }
        }


    }
}

