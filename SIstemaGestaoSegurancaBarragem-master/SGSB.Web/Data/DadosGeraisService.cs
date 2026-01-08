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
using System.Threading;
using System.Text;
using System.Text.Json;
using Entidades.Entidades;

namespace SGSB.Web.Data
{
    public class DadosGeraisService
    {

        HttpClient http;
        public DadosGeraisService(HttpClient _http)
        {
            this.http = _http;
        }
        private readonly IJSRuntime js;

        public async Task<List<DadosGeraisViewModel>> GetDadosGeraisAsync()
        {
            var objDadosGerais = await http.GetFromJsonAsync<List<DadosGeraisViewModel>>(Infra.Constantes.URI + "/API/ListarDadosGerais");

            return objDadosGerais.ToList();

        }

        public async Task<int> GetProximoDadosGeraisIdAsync()
        {
            var objDadosGerais = await http.GetFromJsonAsync<List<DadosGeraisViewModel>>(Infra.Constantes.URI + "/API/ListarDadosGerais");

            int proximoId = 0;

            if (objDadosGerais.ToList().Count == 0)
            {
                proximoId = 1;
            }
            else
            {
                proximoId = objDadosGerais.ToList().LastOrDefault().Id + 1;
            }

            return proximoId;

        }
        public async Task<List<TipoEmpreendedorModel>> GetTipoEmpreendedorAsync()
        {
            var objTipoEmpreendedor = await http.GetFromJsonAsync<List<TipoEmpreendedorModel>>(Infra.Constantes.URI + "/API/ListarTipoEmpreendedor");

            return objTipoEmpreendedor.ToList();

        }

        public async Task<List<DocumentacaoPerguntasModel>> GetDocumentacaoPerguntasAsync()
        {
            var objDocumentacaoPerguntas = await http.GetFromJsonAsync<List<DocumentacaoPerguntasModel>>(Infra.Constantes.URI + "/API/ListarDocumentacaoPerguntas");

            return objDocumentacaoPerguntas.ToList();

        }


        public async Task<List<DocumentacaoProjetoConstrucaoOperacaoModel>> GetDocumentacaoProjetoConstrucaoOperacaoAsync()
        {
            var lstDocumentacaoProjetoConstrucaoOperacao = await http.GetFromJsonAsync<List<DocumentacaoProjetoConstrucaoOperacaoModel>>(Infra.Constantes.URI + "/API/ListarDocumentacaoProjetoConstrucaoOperacao");

            return lstDocumentacaoProjetoConstrucaoOperacao.ToList();

        }




        public static async Task<DadosGeraisViewModel> GetDadosGeraisId(int id)
        {
            DadosGeraisViewModel objDadosGerais = new DadosGeraisViewModel();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdDadosGerais?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                    objDadosGerais = await response.Content.ReadFromJsonAsync<DadosGeraisViewModel>();
                    return objDadosGerais;

                }
            }
            return objDadosGerais;
        }

        public static async Task<DadosGeraisViewModel> ListarDadosGeraisBarragemId(int idBarragem)
        {
            List<DadosGeraisViewModel> objDadosGerais = new List<DadosGeraisViewModel>();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/ListarDadosGeraisBarragemId", idBarragem);
                if (response.IsSuccessStatusCode)
                {
                   
                    objDadosGerais = await response.Content.ReadFromJsonAsync<List<DadosGeraisViewModel>>();
                  
                    return objDadosGerais.FirstOrDefault();
                }
            }
            return objDadosGerais.FirstOrDefault();
        }
        public async Task<DadosGeraisViewModel> VerDadosGerais(int id)
        {
            DadosGeraisViewModel objDadosGerais = new DadosGeraisViewModel();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdDadosGerais?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                    objDadosGerais = await response.Content.ReadFromJsonAsync<DadosGeraisViewModel>();
                    return objDadosGerais;

                }
            }
            return objDadosGerais;
        }
        public static async Task<bool> EditarDadosGerais(DadosGeraisViewModel model)
        {
            using (var cliente = new HttpClient())
            {
                //cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
             
                // HTTP POST - define o produto
                var dadosGeraisModel = new DadosGeraisViewModel()
                {
                    Id = model.Id,
                    DataAlteracao = model.DataAlteracao,
                    DataCadastro = model.DataCadastro,
                    EmailEmpreendedor = model.EmailEmpreendedor,
                    EnderecoEmpreendedor = model.EnderecoEmpreendedor,
                    Latitude = model.Latitude,
                    Longitude = model.Longitude,
                    NomeEmpreendedor = model.NomeEmpreendedor,
                    QuantidadeBarragem = model.QuantidadeBarragem,
                    ResponsavelEmpreendedor = model.ResponsavelEmpreendedor,
                    TelefoneEmpreendedor = model.TelefoneEmpreendedor,
                    TipoEmpreendedor = model.TipoEmpreendedor,
                    BarragemId = model.BarragemId,



                };

                HttpResponseMessage response = await cliente.PutAsJsonAsync(Infra.Constantes.URI + "/API/AtualizaDadosGerais/", dadosGeraisModel);

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


        public async Task<DadosGeraisViewModel> EditDadosGerais(int id)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdDadosGerais?id=" + id);

                return await response.Content.ReadFromJsonAsync<DadosGeraisViewModel>();

            }
        }

        public static async Task<bool> DeletarDadosGerais(Model.DadosGerais model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdDadosGerais?id=" + model.Id);

                if (response.IsSuccessStatusCode)
                {

                    response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/ExcluirDadosGerais", model);
                    return true;
                }
                return false;
            }
        }
        public static async Task<bool> CadastrarDadosGerais(DadosGeraisViewModel model)
        {
            using (var cliente = new HttpClient())
            {
                //cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/ListarDadosGerais/");

                // HTTP POST - define o produto
                var dados = new DadosGeraisViewModel()
                {
                    DataAlteracao = model.DataAlteracao,
                    DataCadastro = model.DataCadastro,
                    EmailEmpreendedor = model.EmailEmpreendedor,
                    EnderecoEmpreendedor = model.EnderecoEmpreendedor,
                    Latitude = model.Latitude,
                    Longitude = model.Longitude,
                    NomeEmpreendedor = model.NomeEmpreendedor,
                    QuantidadeBarragem = model.QuantidadeBarragem,
                    ResponsavelEmpreendedor = model.ResponsavelEmpreendedor,
                    TelefoneEmpreendedor = model.TelefoneEmpreendedor,
                    TipoEmpreendedor = model.TipoEmpreendedor,
                    BarragemId = model.BarragemId,
                    Id = model.Id
                };
                response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/AdicionarDadosGerais/", dados);
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
    }
}
