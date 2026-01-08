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
    public class HidrogramaTriangulaService
    {
        HttpClient http;
        public HidrogramaTriangulaService(HttpClient _http)
        {
            this.http = _http;
        }
        private readonly IJSRuntime js;
        public async Task<List<Barragem>> GetBarragemAsync()
        {
            var objBarragem = await http.GetFromJsonAsync<List<Barragem>>(Infra.Constantes.URI + "/API/ListarBarragem");

            return objBarragem.ToList();

        }


        public async Task<List<HidrogramaTriangula>> GetHidrogramaTriangula()
        {
            var objHidrogramaTriangula = await http.GetFromJsonAsync<List<HidrogramaTriangula>>( Infra.Constantes.URI +"/API/ListarHidrogramaTriangula");

            return objHidrogramaTriangula.ToList();

        }
        public async Task<HidrogramaTriangula> GetHidrogramaTriangulaId(int id)
        {
            HidrogramaTriangula objHidrogramaTriangula = new HidrogramaTriangula();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdHidrogramaTriangula?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                     objHidrogramaTriangula = await response.Content.ReadFromJsonAsync<HidrogramaTriangula>();
                    return objHidrogramaTriangula;

                }
            }
            return objHidrogramaTriangula;
        }

        public  async Task<bool> CadastrarHidrogramaTriangula(HidrogramaRupturaTriangular model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/ListarHidrogramaTriangula/" + model.Id);
                if (response.IsSuccessStatusCode)
                {
                    HidrogramaTriangula objHidrogramaTriangula = await response.Content.ReadFromJsonAsync<HidrogramaTriangula>();


                }
                // HTTP POST - define o produto
                var HidrogramaTriangula = new HidrogramaTriangula()
                {
                    NomePropriedade = model.NomePropriedade,
                    valorQp = model.valorQP,
                    valorTempoPico = model.valorTempoPico,
                    volumesReservatorio = model.valorVolumeReservatorio,
                    BarragemId = model.BarragemId,
                    Id = model.Id,
                    DataCadastro = DateTime.Now,
                    DataAlteracao = DateTime.Now


                };
                response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/AdicionarHidrogramaTriangula", HidrogramaTriangula);
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
        public  async Task<bool> EditarHidrogramaTriangula(HidrogramaRupturaTriangular model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdHidrogramaTriangula?id=" + model.Id);

                // HTTP POST - define o produto
                var HidrogramaTriangula = new HidrogramaTriangula()
                {
                    NomePropriedade = model.NomePropriedade,
                    valorQp = model.valorQP,
                    valorTempoPico = model.valorTempoPico,
                    volumesReservatorio = model.valorVolumeReservatorio,
                    BarragemId = model.BarragemId,
                    Id = model.Id,
                    DataAlteracao = DateTime.Now


                };


                if (response.IsSuccessStatusCode)
                {
                    response = await cliente.PutAsJsonAsync(Infra.Constantes.URI + "/API/AtualizaHidrogramaTriangula/", HidrogramaTriangula);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public async Task<HidrogramaTriangula> EditHidrogramaTriangula(int id)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdHidrogramaTriangula?id=" + id);

                return await response.Content.ReadFromJsonAsync<HidrogramaTriangula>();

            }
        }
        public  async Task<bool> DeletarHidrogramaTriangula(HidrogramaRupturaTriangular model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdHidrogramaTriangula?id=" + model.Id);

              



                if (response.IsSuccessStatusCode)
                {
                 
                    response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/ExcluirHidrogramaTriangula", model);
                    return true;
                }
                return false;
            }
        }


    }
}

