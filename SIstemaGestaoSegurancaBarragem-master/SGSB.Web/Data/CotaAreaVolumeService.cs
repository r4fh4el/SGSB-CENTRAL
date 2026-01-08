using Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;
using SGSB.Web.Models;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

using System.Net.Http.Json;
using System.Linq;
using System;
using System.Threading;
using System.Text;
using System.Text.Json;

namespace SGSB.Web.Data
{
    public class CotaAreaVolumeService
    {
        HttpClient http;
        public CotaAreaVolumeService(HttpClient _http)
        {
            this.http = _http;
        }
        private readonly IJSRuntime js;


        public async Task<List<CotaAreaVolume>> GetCotaAreaVolume()
        {
            var objCotaAreaVolume = await http.GetFromJsonAsync<List<CotaAreaVolume>>( Infra.Constantes.URI +"/API/ListarCotaAreaVolume");

            return objCotaAreaVolume.ToList();

        }
        public async Task<CotaAreaVolume> GetCotaAreaVolumeId(int id)
        {
            CotaAreaVolume objCotaAreaVolume = new CotaAreaVolume();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdCotaAreaVolume?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                     objCotaAreaVolume = await response.Content.ReadFromJsonAsync<CotaAreaVolume>();
                    return objCotaAreaVolume;

                }
            }
            return objCotaAreaVolume;
        }

        public async Task<int> GetProximoCotaAreaVolumeIdAsync()
        {
            var objCotaAreaVolume = await http.GetFromJsonAsync<List<Model.CotaAreaVolume>>(Infra.Constantes.URI + "/API/ListarCotaAreaVolume");

            int proximoId = 0;

            if (objCotaAreaVolume.ToList().Count == 0)
            {
                proximoId = 1;
            }
            else
            {
                proximoId = objCotaAreaVolume.ToList().LastOrDefault().Id + 1;
            }

            return proximoId;

        }
        //public async Task<CotaAreaVolume> EditTipoCondicaoFundacao(int id)
        //{
        //    var objCotaAreaVolume = await http.GetFromJsonAsync<List<CotaAreaVolume>>("/API/ListarCotaAreaVolume");

        //    return objCotaAreaVolume.Where(i => i.Id == id).FirstOrDefault();

        //}
        //public async Task<CotaAreaVolume> VerTipoCondicaoFundacao(int id)
        //{
        //    var objCotaAreaVolume = await http.GetFromJsonAsync<List<CotaAreaVolume>>("/API/ListarCotaAreaVolume");

        //    return objCotaAreaVolume.Where(i => i.Id == id).FirstOrDefault();

        //}
        //public async Task<CotaAreaVolume> Delete(int id)
        //{
        //    var objCotaAreaVolume = await http.GetFromJsonAsync<List<CotaAreaVolume>>("/API/ListarCotaAreaVolume");

        //    return objCotaAreaVolume.Where(i => i.Id == id).FirstOrDefault();

        //}

        public static async Task<bool> CadastrarCotaAreaVolume(CotaAreaVolumeModel model)
        {
            using (var cliente = new HttpClient())
            {
                //cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //// HTTP GET
                //HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/ListarCotaAreaVolume/" + model.Id);
                //if (response.IsSuccessStatusCode)
                //{
                //    CotaAreaVolume objCotaAreaVolume = await response.Content.ReadFromJsonAsync<CotaAreaVolume>();


                //}
                // HTTP POST - define o produto
                var cotaAreaVolume = new CotaAreaVolumeModel()
                {
                    Cota = model.Cota,
                    AreaSuperficial = model.AreaSuperficial,
                    BarragemId = model.BarragemId,

                    VolumeAcumulado = model.VolumeAcumulado


                };
                HttpResponseMessage response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/AdicionarCotaAreaVolume/", cotaAreaVolume);
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
        public static async Task<bool> EditarCotaAreaVolume(CotaAreaVolumeModel model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdCotaAreaVolume?id=" + model.Id);

                // HTTP POST - define o produto
                var cotaAreaVolume = new CotaAreaVolume()
                {
                    Id = model.Id,
                    Cota = model.Cota,
                    AreaSuperficial = model.AreaSuperficial,
                    VolumeAcumulado = model.VolumeAcumulado,
                    DataAlteracao = DateTime.Now,
                    BarragemId = model.BarragemId


                };


                if (response.IsSuccessStatusCode)
                {
                    response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/AtualizaCotaAreaVolume/", cotaAreaVolume);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public async Task<CotaAreaVolume> EditCotaAreaVolume(int id)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdCotaAreaVolume?id=" + id);

                return await response.Content.ReadFromJsonAsync<CotaAreaVolume>();

            }
        }
        public static async Task<bool> DeletarCotaAreaVolume(CotaAreaVolume model)
        {
            using (var cliente = new HttpClient())
            {
              
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET       
         
                var cotaAreaVolume = new CotaAreaVolumeModel()
                {
                    Id = model.Id,
                    Cota = model.Cota,
                    AreaSuperficial = model.AreaSuperficial,
                    BarragemId = model.BarragemId,

                    VolumeAcumulado = model.VolumeAcumulado


                };
                var jsonPayload = new StringContent(JsonSerializer.Serialize(cotaAreaVolume), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + @"/API/ExcluirCotaAreaVolume", cotaAreaVolume.Id);
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
        public static async Task<CotaAreaVolumeModel> ListarCotaAreaVolumeBarragemId(int idBarragem)
        {
            List<CotaAreaVolumeModel> objCotaAreaVolume = new List<CotaAreaVolumeModel>();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/ListarCotaAreaVolumeBarragemId", idBarragem);
                if (response.IsSuccessStatusCode)
                {

                    objCotaAreaVolume = await response.Content.ReadFromJsonAsync<List<CotaAreaVolumeModel>>();

                    return objCotaAreaVolume.FirstOrDefault();
                }
            }
            return objCotaAreaVolume.FirstOrDefault();
        }

    }
}

