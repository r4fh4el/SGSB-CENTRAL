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

namespace SGSB.Web.Data
{
    public class TempoConcentracaoService
    {
        HttpClient http;
        public TempoConcentracaoService(HttpClient _http)
        {
            this.http = _http;
        }
        private readonly IJSRuntime js;


        public async Task<List<TempoConcentracao>> GetTempoConcentracao()
        {
            var objTempoConcentracao = await http.GetFromJsonAsync<List<TempoConcentracao>>(Infra.Constantes.URI + "/API/ListarTempoConcentracao");

            return objTempoConcentracao.ToList();

        }
        public async Task<TempoConcentracao> GetTempoConcentracaoId(int id)
        {
            TempoConcentracao objTempoConcentracao = new TempoConcentracao();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdTempoConcentracao?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                    objTempoConcentracao = await response.Content.ReadFromJsonAsync<TempoConcentracao>();
                    return objTempoConcentracao;

                }
            }
            return objTempoConcentracao;
        }

        public static async Task<bool> CadastrarTempoConcentracao(TempoConcentracao model)
        {
            using (var cliente = new HttpClient())
            {
                //cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
             
                // HTTP POST - define o produto
                var tempoConcentracaoModel = new TempoConcentracaoModel()
                {
                    ComprimentoRioPrincipal_L = model.ComprimentoRioPrincipal_L,
                    AreaDrenagem_A = model.AreaDrenagem_A,
                    BarragemId = model.BarragemId,
                    DeclividadeBacia_S = model.DeclividadeBacia_S,
                    ResultadoCarter = model.ResultadoCarter,
                    ResultadoCorpsEngineers = model.ResultadoCorpsEngineers,
                    ResultadoDooge = model.ResultadoDooge,
                    ResultadoKirpich = model.ResultadoKirpich,
                    ResultadoVenTeChow = model.ResultadoVenTeChow,
                    DataCadastro = DateTime.Now,
                    DataAlteracao = DateTime.Now


                };
                HttpResponseMessage response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/AdicionarTempoConcentracao", tempoConcentracaoModel);
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
        public static async Task<bool> EditarTempoConcentracao(TempoConcentracao model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdTempoConcentracao?id=" + model.Id);

                // HTTP POST - define o produto
                var tempoConcentracao = new TempoConcentracao()
                {
                    NomePropriedade = model.NomePropriedade,
                    ComprimentoRioPrincipal_L = model.ComprimentoRioPrincipal_L,
                    AreaDrenagem_A = model.AreaDrenagem_A,
                    BarragemId = model.BarragemId,
                    DeclividadeBacia_S = model.DeclividadeBacia_S,
                    Id = model.Id,
                    Notificacoes = model.Notificacoes,
                    Mensagem = model.Mensagem,
                    ResultadoCarter = model.ResultadoCarter,
                    ResultadoCorpsEngineers = model.ResultadoCorpsEngineers,
                    ResultadoDooge = model.ResultadoDooge,
                    ResultadoKirpich = model.ResultadoKirpich,
                    ResultadoVenTeChow = model.ResultadoVenTeChow,
                    DataAlteracao = DateTime.Now

                };


                if (response.IsSuccessStatusCode)
                {
                    response = await cliente.PutAsJsonAsync(Infra.Constantes.URI + "/API/AtualizaTempoConcentracao/", tempoConcentracao);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public async Task<TempoConcentracao> EditTempoConcentracao(int id)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdTempoConcentracao?id=" + id);

                return await response.Content.ReadFromJsonAsync<TempoConcentracao>();

            }
        }
        public static async Task<bool> DeletarTempoConcentracao(TempoConcentracao model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdTempoConcentracao?id=" + model.Id);





                if (response.IsSuccessStatusCode)
                {

                    response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/ExcluirTempoConcentracao", model);
                    return true;
                }
                return false;
            }
        }


    }
}

