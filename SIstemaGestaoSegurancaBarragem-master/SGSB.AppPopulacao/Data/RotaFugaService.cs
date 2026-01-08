

using System.Net.Http.Headers;
using System.Security.Policy;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using SGSB.AppPopulacao.MVVM.Models;

namespace SGSB.AppPopulacao.Data
{
    public class RotaFugaService
    {
        HttpClient http;
        public RotaFugaService(HttpClient _http)
        {
            this.http = _http;
        }
  

        public static async Task<List<RotaFugaModel>> GetRotaFuga()
        {
            List<RotaFugaModel> lstRotaFuga = new List<RotaFugaModel>();

            using (var cliente = new HttpClient())
            {
               
                //cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Make a GET request to the API
                //HttpResponseMessage response = await cliente.GetAsync("https://localhost:7042/API/ListarRotaFugaApp");

                //// Check if the request was successful
                //if (response.IsSuccessStatusCode)
                //{
                //    // Read the content of the response as a string
                //    string jsonContent = await response.Content.ReadAsStringAsync();

                //    // Now you can parse and work with the JSON data (e.g., using Newtonsoft.Json)
                //    // For simplicity, let's just display the JSON content in a label
                //    Label jsonLabel = new Label
                //    {
                //        Text = jsonContent,
                //        HorizontalOptions = LayoutOptions.CenterAndExpand,
                //        VerticalOptions = LayoutOptions.CenterAndExpand
                //    };

                //    // Add the label to the main content of the page
                //    var teste = jsonLabel;
                //}
                //else
                //{
                //    // Handle the case where the API request was not successful
                //    Console.WriteLine($"Error: {response.StatusCode}");
                //}

                //var lstRotaFugaAPI = await cliente.GetFromJsonAsync<List<RotaFugaModel>>("https://api.sgsb.com.br/API/ListarRotaFuga");

                HttpResponseMessage response = await cliente.GetAsync("https://api.sgsb.com.br/API/ListarRotaFuga");
            }
            return lstRotaFuga.ToList();

        }
        public static async Task<List<RotaFuga>> GetBuscarListPorIdRotaFuga(int id)
        {
           List<RotaFuga> lstRotaFuga = new List<RotaFuga>();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarListPorIdRotaFuga?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                    lstRotaFuga = await response.Content.ReadFromJsonAsync<List<RotaFuga>>();
                    return lstRotaFuga;

                }
            }
            return lstRotaFuga;
        }
        public async Task<RotaFuga> GetRotaFugaId(int id)
        {
            RotaFuga objRotaFuga = new RotaFuga();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdRotaFuga?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                     objRotaFuga = await response.Content.ReadFromJsonAsync<RotaFuga>();
                    return objRotaFuga;

                }
            }
            return objRotaFuga;
        }
        //public async Task<RotaFuga> EditTipoCondicaoFundacao(int id)
        //{
        //    var objRotaFuga = await http.GetFromJsonAsync<List<RotaFuga>>("/API/ListarRotaFuga");

        //    return objRotaFuga.Where(i => i.Id == id).FirstOrDefault();

        //}
        //public async Task<RotaFuga> VerTipoCondicaoFundacao(int id)
        //{
        //    var objRotaFuga = await http.GetFromJsonAsync<List<RotaFuga>>("/API/ListarRotaFuga");

        //    return objRotaFuga.Where(i => i.Id == id).FirstOrDefault();

        //}
        //public async Task<RotaFuga> Delete(int id)
        //{
        //    var objRotaFuga = await http.GetFromJsonAsync<List<RotaFuga>>("/API/ListarRotaFuga");

        //    return objRotaFuga.Where(i => i.Id == id).FirstOrDefault();

        //}

        //public static async Task<bool> CadastrarRotaFuga(RotaFuga model)
        //{
        //    using (var cliente = new HttpClient())
        //    {
        //        cliente.BaseAddress = new Uri(Infra.Constantes.URI);
        //        cliente.DefaultRequestHeaders.Accept.Clear();
        //        cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        // HTTP GET
        //        HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/ListarRotaFuga/" + model.Id);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            RotaFuga objRotaFuga = await response.Content.ReadFromJsonAsync<RotaFuga>();


        //        }
        //        // HTTP POST - define o produto
        //        var RotaFuga = new RotaFugaModel()
        //        {
        //            Nome = model.Nome,
        //            BarragemId = model.BarragemId,
        //            Latitude = model.Latitude,
        //            Longitude = model.Longitude,
                  
        //            DataCadastro = DateTime.Now,
        //            DataAlteracao = DateTime.Now


        //        };
        //        response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/AdicionarRotaFuga", RotaFuga);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            Uri produtoUrl = response.Headers.Location;

        //            return true;

        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}
        //public static async Task<bool> EditarRotaFuga(RotaFuga model)
        //{
        //    using (var cliente = new HttpClient())
        //    {
        //        cliente.BaseAddress = new Uri(Infra.Constantes.URI);
        //        cliente.DefaultRequestHeaders.Accept.Clear();
        //        cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        // HTTP GET
        //        HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdRotaFuga?id=" + model.Id);

            
        //        // HTTP POST - define o produto
        //        var RotaFuga = new RotaFuga()
        //        {
        //            Nome = model.Nome,
        //            BarragemId = model.BarragemId,
        //            Latitude = model.Latitude,
        //            Longitude = model.Longitude,
        //            Id = model.Id,
        //            DataCadastro = DateTime.Now,
        //            DataAlteracao = DateTime.Now


        //        };

        //        if (response.IsSuccessStatusCode)
        //        {
        //            response = await cliente.PutAsJsonAsync(Infra.Constantes.URI + "/API/AtualizaRotaFuga/", RotaFuga);
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}
        public async Task<RotaFuga> EditRotaFuga(int id)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdRotaFuga?id=" + id);

                return await response.Content.ReadFromJsonAsync<RotaFuga>();

            }
        }
        public static async Task<bool> DeletarRotaFuga(RotaFuga model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdRotaFuga?id=" + model.Id);

              



                if (response.IsSuccessStatusCode)
                {
                 
                    response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/ExcluirRotaFuga", model);
                    return true;
                }
                return false;
            }
        }


    }
}

