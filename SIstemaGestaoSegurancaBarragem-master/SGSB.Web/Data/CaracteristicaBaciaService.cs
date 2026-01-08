using Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;

using System.Net.Http.Headers;
using System.Security.Policy;

using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Linq;
using System;

namespace SGSB.Web.Data
{
    public class CaracteristicaBaciaService
    {
        HttpClient http;
        public CaracteristicaBaciaService(HttpClient _http)
        {
            this.http = _http;
        }
        private readonly IJSRuntime js;


        public async Task<List<CaracteristicaBacia>> GetCaracteristicaBacia()
        {
            var objCaracteristicaBacia = await http.GetFromJsonAsync<List<CaracteristicaBacia>>( Infra.Constantes.URI +"/API/ListarCaracteristicaBacia");

            return objCaracteristicaBacia.ToList();

        }

        public async Task<List<UsoSoloPredominante>> GetUsoSoloPredominante()
        {
            var objUsoSoloPredominante = await http.GetFromJsonAsync<List<UsoSoloPredominante>>(Infra.Constantes.URI + "/API/ListarUsoSoloPredominante");

            return objUsoSoloPredominante.ToList();

        }

        public async Task<CaracteristicaBacia> GetCaracteristicaBaciaId(int id)
        {
            CaracteristicaBacia objCaracteristicaBacia = new CaracteristicaBacia();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdCaracteristicaBacia?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                     objCaracteristicaBacia = await response.Content.ReadFromJsonAsync<CaracteristicaBacia>();
                    return objCaracteristicaBacia;

                }
            }
            return objCaracteristicaBacia;
        }
        //public async Task<CaracteristicaBacia> EditTipoCondicaoFundacao(int id)
        //{
        //    var objCaracteristicaBacia = await http.GetFromJsonAsync<List<CaracteristicaBacia>>("/API/ListarCaracteristicaBacia");

        //    return objCaracteristicaBacia.Where(i => i.Id == id).FirstOrDefault();

        //}
        //public async Task<CaracteristicaBacia> VerTipoCondicaoFundacao(int id)
        //{
        //    var objCaracteristicaBacia = await http.GetFromJsonAsync<List<CaracteristicaBacia>>("/API/ListarCaracteristicaBacia");

        //    return objCaracteristicaBacia.Where(i => i.Id == id).FirstOrDefault();

        //}
        //public async Task<CaracteristicaBacia> Delete(int id)
        //{
        //    var objCaracteristicaBacia = await http.GetFromJsonAsync<List<CaracteristicaBacia>>("/API/ListarCaracteristicaBacia");

        //    return objCaracteristicaBacia.Where(i => i.Id == id).FirstOrDefault();

        //}

        public static async Task<bool> CadastrarCaracteristicaBacia(CaracteristicaBacia model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/ListarCaracteristicaBacia/" + model.Id);
                if (response.IsSuccessStatusCode)
                {
                    CaracteristicaBacia objCaracteristicaBacia = await response.Content.ReadFromJsonAsync<CaracteristicaBacia>();


                }
     
                response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/AdicionarCaracteristicaBacia", model);
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
        public static async Task<bool> EditarCaracteristicaBacia(CaracteristicaBacia model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdCaracteristicaBacia?id=" + model.Id);

             


                if (response.IsSuccessStatusCode)
                {
                    response = await cliente.PutAsJsonAsync(Infra.Constantes.URI + "/API/AtualizaCaracteristicaBacia/", model);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public async Task<CaracteristicaBacia> EditCaracteristicaBacia(int id)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdCaracteristicaBacia?id=" + id);

                return await response.Content.ReadFromJsonAsync<CaracteristicaBacia>();

            }
        }
        public static async Task<bool> DeletarCaracteristicaBacia(CaracteristicaBacia model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdCaracteristicaBacia?id=" + model.Id);

              



                if (response.IsSuccessStatusCode)
                {
                 
                    response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/ExcluirCaracteristicaBacia", model);
                    return true;
                }
                return false;
            }
        }


    }
}

