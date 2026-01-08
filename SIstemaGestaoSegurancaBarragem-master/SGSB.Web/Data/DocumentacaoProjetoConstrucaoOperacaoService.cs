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
    public class DocumentacaoProjetoConstrucaoOperacaoService
    {
        HttpClient http;
        public DocumentacaoProjetoConstrucaoOperacaoService(HttpClient _http)
        {
            this.http = _http;
        }
        private readonly IJSRuntime js;


        public async Task<List<DocumentacaoProjetoConstrucaoOperacao>> GetDocumentacaoProjetoConstrucaoOperacao()
        {
            var objDocumentacaoProjetoConstrucaoOperacao = await http.GetFromJsonAsync<List<DocumentacaoProjetoConstrucaoOperacao>>( Infra.Constantes.URI +"/API/ListarDocumentacaoProjetoConstrucaoOperacao");

            return objDocumentacaoProjetoConstrucaoOperacao.ToList();

        }
        public async Task<DocumentacaoProjetoConstrucaoOperacao> GetDocumentacaoProjetoConstrucaoOperacaoId(int id)
        {
            DocumentacaoProjetoConstrucaoOperacao objDocumentacaoProjetoConstrucaoOperacao = new DocumentacaoProjetoConstrucaoOperacao();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdDocumentacaoProjetoConstrucaoOperacao?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                     objDocumentacaoProjetoConstrucaoOperacao = await response.Content.ReadFromJsonAsync<DocumentacaoProjetoConstrucaoOperacao>();
                    return objDocumentacaoProjetoConstrucaoOperacao;

                }
            }
            return objDocumentacaoProjetoConstrucaoOperacao;
        }
        //public async Task<DocumentacaoProjetoConstrucaoOperacao> EditTipoCondicaoFundacao(int id)
        //{
        //    var objDocumentacaoProjetoConstrucaoOperacao = await http.GetFromJsonAsync<List<DocumentacaoProjetoConstrucaoOperacao>>("/API/ListarDocumentacaoProjetoConstrucaoOperacao");

        //    return objDocumentacaoProjetoConstrucaoOperacao.Where(i => i.Id == id).FirstOrDefault();

        //}
        //public async Task<DocumentacaoProjetoConstrucaoOperacao> VerTipoCondicaoFundacao(int id)
        //{
        //    var objDocumentacaoProjetoConstrucaoOperacao = await http.GetFromJsonAsync<List<DocumentacaoProjetoConstrucaoOperacao>>("/API/ListarDocumentacaoProjetoConstrucaoOperacao");

        //    return objDocumentacaoProjetoConstrucaoOperacao.Where(i => i.Id == id).FirstOrDefault();

        //}
        //public async Task<DocumentacaoProjetoConstrucaoOperacao> Delete(int id)
        //{
        //    var objDocumentacaoProjetoConstrucaoOperacao = await http.GetFromJsonAsync<List<DocumentacaoProjetoConstrucaoOperacao>>("/API/ListarDocumentacaoProjetoConstrucaoOperacao");

        //    return objDocumentacaoProjetoConstrucaoOperacao.Where(i => i.Id == id).FirstOrDefault();

        //}
        public async Task<int> GetProximoDocumentacaoProjetoConstrucaoOperacaoAsync()
        {
            var objDocumentacaoProjetoConstrucaoOperacao = await http.GetFromJsonAsync<List<Model.DocumentacaoProjetoConstrucaoOperacao>>(Infra.Constantes.URI + "/API/ListarDocumentacaoProjetoConstrucaoOperacao");

            int proximoId = 0;

            if (objDocumentacaoProjetoConstrucaoOperacao.ToList().Count == 0)
            {
                proximoId = 1;
            }
            else
            {
                proximoId = objDocumentacaoProjetoConstrucaoOperacao.ToList().LastOrDefault().Id + 1;
            }

            return proximoId;

        }
        public static async Task<bool> CadastrarDocumentacaoProjetoConstrucaoOperacao(DocumentacaoProjetoConstrucaoOperacaoModel model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/ListarDocumentacaoProjetoConstrucaoOperacao/" + model.Id);
                if (response.IsSuccessStatusCode)
                {
                    DocumentacaoProjetoConstrucaoOperacao objDocumentacaoProjetoConstrucaoOperacao = await response.Content.ReadFromJsonAsync<DocumentacaoProjetoConstrucaoOperacao>();


                }
                // HTTP POST - define o produto
                var documentacaoProjetoConstrucaoOperacao = new DocumentacaoProjetoConstrucaoOperacaoModel()
                {
                    Pergunta = model.Pergunta,
                    Resposta = model.Resposta,
                    BarragemId = model.BarragemId,


                };
                response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/AdicionarDocumentacaoProjetoConstrucaoOperacao", documentacaoProjetoConstrucaoOperacao);
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
        public static async Task<bool> EditarDocumentacaoProjetoConstrucaoOperacao(DocumentacaoProjetoConstrucaoOperacaoModel model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdDocumentacaoProjetoConstrucaoOperacao?id=" + model.Id);

                // HTTP POST - define o produto
                var documentacaoProjetoConstrucaoOperacao = new DocumentacaoProjetoConstrucaoOperacao()
                {
                    Id = model.Id,
                    Pergunta = model.Pergunta,
                    Resposta = model.Resposta,
                 
                    BarragemId = model.BarragemId,
                    DataCadastro = DateTime.Now,
                    DataAlteracao = DateTime.Now


                };


                if (response.IsSuccessStatusCode)
                {
                    response = await cliente.PutAsJsonAsync(Infra.Constantes.URI + "/API/AtualizaDocumentacaoProjetoConstrucaoOperacao/", documentacaoProjetoConstrucaoOperacao);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public async Task<DocumentacaoProjetoConstrucaoOperacao> EditDocumentacaoProjetoConstrucaoOperacao(int id)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdDocumentacaoProjetoConstrucaoOperacao?id=" + id);

                return await response.Content.ReadFromJsonAsync<DocumentacaoProjetoConstrucaoOperacao>();

            }
        }
        public static async Task<bool> DeletarDocumentacaoProjetoConstrucaoOperacao(DocumentacaoProjetoConstrucaoOperacao model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdDocumentacaoProjetoConstrucaoOperacao?id=" + model.Id);

              



                if (response.IsSuccessStatusCode)
                {
                 
                    response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/ExcluirDocumentacaoProjetoConstrucaoOperacao", model);
                    return true;
                }
                return false;
            }
        }

        public static async Task<List<DocumentacaoProjetoConstrucaoOperacaoModel>> ListarDocumentacaoProjetoConstrucaoOperacaoBarragemId(int idBarragem)
        {
            List<DocumentacaoProjetoConstrucaoOperacaoModel> objDocumentacaoProjetoConstrucaoOperacao = new List<DocumentacaoProjetoConstrucaoOperacaoModel>();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/ListarDocumentacaoProjetoConstrucaoOperacaoBarragemId", idBarragem);
                if (response.IsSuccessStatusCode)
                {

                    objDocumentacaoProjetoConstrucaoOperacao = await response.Content.ReadFromJsonAsync<List<DocumentacaoProjetoConstrucaoOperacaoModel>>();

                    return objDocumentacaoProjetoConstrucaoOperacao;
                }
            }
            return objDocumentacaoProjetoConstrucaoOperacao;
        }
    }
}

