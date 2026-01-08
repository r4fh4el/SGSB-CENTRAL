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
using SGSB.Web.Models;

namespace SGSB.Web.Data
{
    public class TaludeService
    {
        HttpClient http;
        public TaludeService(HttpClient _http)
        {
            this.http = _http;
        }
        private readonly IJSRuntime js;
        public async Task<List<Barragem>> GetBarragemAsync()
        {
            var objBarragem = await http.GetFromJsonAsync<List<Barragem>>(Infra.Constantes.URI + "/API/ListarBarragem");

            return objBarragem.ToList();

        }


        public async Task<List<Talude>> GetTalude()
        {
            var objTalude = await http.GetFromJsonAsync<List<Talude>>( Infra.Constantes.URI +"/API/ListarTalude");

            return objTalude.ToList();

        }
        public async Task<Talude> GetTaludeId(int id)
        {
            Talude objTalude = new Talude();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdTalude?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                     objTalude = await response.Content.ReadFromJsonAsync<Talude>();
                    return objTalude;

                }
            }
            return objTalude;
        }
        //public async Task<Talude> EditTipoCondicaoFundacao(int id)
        //{
        //    var objTalude = await http.GetFromJsonAsync<List<Talude>>("/API/ListarTalude");

        //    return objTalude.Where(i => i.Id == id).FirstOrDefault();

        //}
        //public async Task<Talude> VerTipoCondicaoFundacao(int id)
        //{
        //    var objTalude = await http.GetFromJsonAsync<List<Talude>>("/API/ListarTalude");

        //    return objTalude.Where(i => i.Id == id).FirstOrDefault();

        //}
        //public async Task<Talude> Delete(int id)
        //{
        //    var objTalude = await http.GetFromJsonAsync<List<Talude>>("/API/ListarTalude");

        //    return objTalude.Where(i => i.Id == id).FirstOrDefault();

        //}

        public  async Task<bool> CadastrarTalude(TaludeModel model)
        {
            using (var cliente = new HttpClient())
            {
                //cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/ListarTalude/" + model.Id);
                if (response.IsSuccessStatusCode)
                {
                    Talude objTalude = await response.Content.ReadFromJsonAsync<Talude>();


                }
                // HTTP POST - define o produto
                var Talude = new TaludeModel()
                {
           
                    Id = model.Id,
                    InclinacaoTaludeJusante = model.InclinacaoTaludeJusante,
                    InclinacaoTaludeMontante = model.InclinacaoTaludeMontante,
                    TipoProtecaoSuperficieJusante = model.TipoProtecaoSuperficieJusante,
                    TipoProtecaoSuperficieMontante = model.TipoProtecaoSuperficieMontante,
                    BarragemId = model.BarragemId,
                    
                    DataCadastro = DateTime.Now,
                    DataAlteracao = DateTime.Now


                };
                response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/AdicionarTalude/", Talude);
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

        public async Task<int> GetProximoTaludeIdAsync()
        {
            var objTalude = await http.GetFromJsonAsync<List<Talude>>(Infra.Constantes.URI + "/API/ListarTalude");

            int proximoId = 0;

            if (objTalude.ToList().Count == 0)
            {
                proximoId = 1;
            }
            else
            {
                proximoId = objTalude.ToList().LastOrDefault().Id + 1;
            }

            return proximoId;

        }
        public   async Task<bool> EditarTalude(TaludeModel model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdTalude?id=" + model.Id);

                // HTTP POST - define o produto
                var taludeModel = new TaludeModel()
                {
  
                    Id = model.Id,
                    InclinacaoTaludeJusante = model.InclinacaoTaludeJusante,
                    InclinacaoTaludeMontante = model.InclinacaoTaludeMontante,
                    TipoProtecaoSuperficieJusante = model.TipoProtecaoSuperficieJusante,
                    TipoProtecaoSuperficieMontante = model.TipoProtecaoSuperficieMontante,
                    BarragemId = model.BarragemId,
                    DataAlteracao = DateTime.Now



                };

                 response = await cliente.PutAsJsonAsync(Infra.Constantes.URI + "/API/AtualizaTalude/", taludeModel);

                if (response.IsSuccessStatusCode)
                {
                     return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public async Task<Talude> EditTalude(int id)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdTalude?id=" + id);

                return await response.Content.ReadFromJsonAsync<Talude>();

            }
        }
        public  async Task<bool> DeletarTalude(Talude model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdTalude?id=" + model.Id);

              



                if (response.IsSuccessStatusCode)
                {
                 
                    response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/ExcluirTalude", model);
                    return true;
                }
                return false;
            }
        }
        public static async Task<TaludeModel> ListarTaludeBarragemId(int idBarragem)
        {
            List<TaludeModel> objTalude = new List<TaludeModel>();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/ListarTaludeBarragemId", idBarragem);
                if (response.IsSuccessStatusCode)
                {

                    objTalude = await response.Content.ReadFromJsonAsync<List<TaludeModel>>();

                    return objTalude.FirstOrDefault();
                }
            }
            return objTalude.FirstOrDefault();
        }

    }
}

