

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
    public class PontoEncontroService
    {
        HttpClient http;
        public PontoEncontroService(HttpClient _http)
        {
            this.http = _http;
        }
  

        public static async Task<List<PontoEncontroModel>> GetPontoEncontro()
        {
            List<PontoEncontroModel> lstPontoEncontro = new List<PontoEncontroModel>();

            using (var cliente = new HttpClient())
            {
                
                //cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Make a GET request to the API
                //HttpResponseMessage response = await cliente.GetAsync("https://localhost:7042/API/ListarPontoEncontroApp");

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

                //var lstPontoEncontroAPI = await cliente.GetFromJsonAsync<List<PontoEncontroModel>>("https://api.sgsb.com.br/API/ListarPontoEncontro");

                HttpResponseMessage response = await cliente.GetAsync("https://api.sgsb.com.br/API/ListarPontoEncontro");
            }
            return lstPontoEncontro.ToList();

        }
        public static async Task<List<PontoEncontro>> GetBuscarListPorIdPontoEncontro(int id)
        {
           List<PontoEncontro> lstPontoEncontro = new List<PontoEncontro>();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarListPorIdPontoEncontro?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                    lstPontoEncontro = await response.Content.ReadFromJsonAsync<List<PontoEncontro>>();
                    return lstPontoEncontro;

                }
            }
            return lstPontoEncontro;
        }
        public async Task<PontoEncontro> GetPontoEncontroId(int id)
        {
            PontoEncontro objPontoEncontro = new PontoEncontro();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdPontoEncontro?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                     objPontoEncontro = await response.Content.ReadFromJsonAsync<PontoEncontro>();
                    return objPontoEncontro;

                }
            }
            return objPontoEncontro;
        }
        //public async Task<PontoEncontro> EditTipoCondicaoFundacao(int id)
        //{
        //    var objPontoEncontro = await http.GetFromJsonAsync<List<PontoEncontro>>("/API/ListarPontoEncontro");

        //    return objPontoEncontro.Where(i => i.Id == id).FirstOrDefault();

        //}
        //public async Task<PontoEncontro> VerTipoCondicaoFundacao(int id)
        //{
        //    var objPontoEncontro = await http.GetFromJsonAsync<List<PontoEncontro>>("/API/ListarPontoEncontro");

        //    return objPontoEncontro.Where(i => i.Id == id).FirstOrDefault();

        //}
        //public async Task<PontoEncontro> Delete(int id)
        //{
        //    var objPontoEncontro = await http.GetFromJsonAsync<List<PontoEncontro>>("/API/ListarPontoEncontro");

        //    return objPontoEncontro.Where(i => i.Id == id).FirstOrDefault();

        //}

        //public static async Task<bool> CadastrarPontoEncontro(PontoEncontro model)
        //{
        //    using (var cliente = new HttpClient())
        //    {
        //        cliente.BaseAddress = new Uri(Infra.Constantes.URI);
        //        cliente.DefaultRequestHeaders.Accept.Clear();
        //        cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        // HTTP GET
        //        HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/ListarPontoEncontro/" + model.Id);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            PontoEncontro objPontoEncontro = await response.Content.ReadFromJsonAsync<PontoEncontro>();


        //        }
        //        // HTTP POST - define o produto
        //        var PontoEncontro = new PontoEncontroModel()
        //        {
        //            Nome = model.Nome,
        //            BarragemId = model.BarragemId,
        //            Latitude = model.Latitude,
        //            Longitude = model.Longitude,
                  
        //            DataCadastro = DateTime.Now,
        //            DataAlteracao = DateTime.Now


        //        };
        //        response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/AdicionarPontoEncontro", PontoEncontro);
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
        //public static async Task<bool> EditarPontoEncontro(PontoEncontro model)
        //{
        //    using (var cliente = new HttpClient())
        //    {
        //        cliente.BaseAddress = new Uri(Infra.Constantes.URI);
        //        cliente.DefaultRequestHeaders.Accept.Clear();
        //        cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        // HTTP GET
        //        HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdPontoEncontro?id=" + model.Id);

            
        //        // HTTP POST - define o produto
        //        var PontoEncontro = new PontoEncontro()
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
        //            response = await cliente.PutAsJsonAsync(Infra.Constantes.URI + "/API/AtualizaPontoEncontro/", PontoEncontro);
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}
        public async Task<PontoEncontro> EditPontoEncontro(int id)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdPontoEncontro?id=" + id);

                return await response.Content.ReadFromJsonAsync<PontoEncontro>();

            }
        }
        public static async Task<bool> DeletarPontoEncontro(PontoEncontro model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdPontoEncontro?id=" + model.Id);

              



                if (response.IsSuccessStatusCode)
                {
                 
                    response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/ExcluirPontoEncontro", model);
                    return true;
                }
                return false;
            }
        }


    }
}

