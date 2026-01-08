using Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Linq;
using System;
using SGSB.Web.Models;
using Entidades.Entidades;

namespace SGSB.Web.Data
{
    public class VertedouroService
    {

        HttpClient http;
        public VertedouroService(HttpClient _http)
        {
            this.http = _http;
        }
        private readonly IJSRuntime js;

        public async Task<List<Model.Vertedouro>> GetVertedouroAsync()
        {
            var objVertedouro = await http.GetFromJsonAsync<List<Model.Vertedouro>>(Infra.Constantes.URI + "/API/ListarVertedouro");

            return objVertedouro.ToList();

        }

        public async Task<int> GetProximoVertedouroIdAsync()
        {
            var objVertedouro = await http.GetFromJsonAsync<List<Model.Vertedouro>>(Infra.Constantes.URI + "/API/ListarVertedouro");

            int proximoId = 0;

            if (objVertedouro.ToList().Count == 0)
            {
                proximoId = 1;
            }
            else
            {
                proximoId = objVertedouro.ToList().LastOrDefault().Id + 1;
            }

            return proximoId;

        }
 


        public static async Task<Model.Vertedouro> GetVertedouroId(int id)
        {
            Model.Vertedouro objVertedouro = new Model.Vertedouro();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdVertedouro?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                    objVertedouro = await response.Content.ReadFromJsonAsync<Model.Vertedouro>();
                    return objVertedouro;

                }
            }
            return objVertedouro;
        }
        public async Task<Model.Vertedouro> VerVertedouro(int id)
        {
            Model.Vertedouro objVertedouro = new Model.Vertedouro();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdVertedouro?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                    objVertedouro = await response.Content.ReadFromJsonAsync<Model.Vertedouro>();
                    return objVertedouro;

                }
            }
            return objVertedouro;
        }
        public static async Task<bool> EditarVertedouro(VertedouroModel model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdVertedouro?id=" + model.Id);

                // HTTP POST - define o produto
                var vertedouro = new Model.Vertedouro()
                {
                    Id = model.Id,
        
                    Altura = model.Altura,
                    ComprimentoTotalVertedouro = model.ComprimentoTotalVertedouro,
                    CotaSoleiraVetedouro = model.CotaSoleiraVetedouro,
                    LarguraTotalVertedouro = model.LarguraTotalVertedouro,
                    LocalizacaoVertedouro = model.LocalizacaoVertedouro,
                    ModalidadeDissipacaoEnergia = model.ModalidadeDissipacaoEnergia,
      
                    QuantidadeComportas = model.QuantidadeComportas,
                    TempoRetornoVazaoMaximaProjetoAnos = model.TempoRetornoVazaoMaximaProjetoAnos,  
                    TipoAcionamentoComportas = model.TipoAcionamentoComportas,  
                    TipoEstruturaExtravasoraPrincipal = model.TipoEstruturaExtravasoraPrincipal,  
                    VazaoMaximaProjetoVertedor = model.VazaoMaximaProjetoVertedor,  
                    VertedouroControle = model.VertedouroControle,  
                    VetedouroAuxiliar = model.VetedouroAuxiliar,
                      BarragemId = model.BarragemId,
                      DataAlteracao = DateTime.Now

                };


                if (response.IsSuccessStatusCode)
                {
                    response = await cliente.PutAsJsonAsync(Infra.Constantes.URI + "/API/AtualizaVertedouro/", vertedouro);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        public async Task<Model.Vertedouro> EditVertedouro(int id)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdVertedouro?id=" + id);

                return await response.Content.ReadFromJsonAsync<Model.Vertedouro>();

            }
        }

        public static async Task<bool> DeletarVertedouro(Model.Vertedouro model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdVertedouro?id=" + model.Id);
                
                if (response.IsSuccessStatusCode)
                {

                    response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/ExcluirVertedouro", model);
                    return true;
                }
                return false;
            }
        }
        public static async Task<bool> CadastrarVertedouro(VertedouroModel model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
               
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/ListarVertedouro/");
           
                // HTTP POST - define o produto
                var vertedouro = new VertedouroModel()
                {
                    Id = model.Id,
      
                    Altura = model.Altura,
                    ComprimentoTotalVertedouro = model.ComprimentoTotalVertedouro,
                    CotaSoleiraVetedouro = model.CotaSoleiraVetedouro,
                    LarguraTotalVertedouro = model.LarguraTotalVertedouro,
                    LocalizacaoVertedouro = model.LocalizacaoVertedouro,
                    ModalidadeDissipacaoEnergia = model.ModalidadeDissipacaoEnergia,
                  BarragemId = model.BarragemId,
                    QuantidadeComportas = model.QuantidadeComportas,
                    TempoRetornoVazaoMaximaProjetoAnos = model.TempoRetornoVazaoMaximaProjetoAnos,
                    TipoAcionamentoComportas = model.TipoAcionamentoComportas,
                    TipoEstruturaExtravasoraPrincipal = model.TipoEstruturaExtravasoraPrincipal,
                    VazaoMaximaProjetoVertedor = model.VazaoMaximaProjetoVertedor,
                    VertedouroControle = model.VertedouroControle,
                    VetedouroAuxiliar = model.VetedouroAuxiliar,
               
                };
                response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/AdicionarVertedouro/", vertedouro);
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

        public static async Task<VertedouroModel> ListarVertedouroBarragemId(int idBarragem)
        {
            List<VertedouroModel> objVertedouro = new List<VertedouroModel>();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/ListarVertedouroBarragemId", idBarragem);
                if (response.IsSuccessStatusCode)
                {

                    objVertedouro = await response.Content.ReadFromJsonAsync<List<VertedouroModel>>();

                    return objVertedouro.FirstOrDefault();
                }
            }
            return objVertedouro.FirstOrDefault();
        }

    }
}
