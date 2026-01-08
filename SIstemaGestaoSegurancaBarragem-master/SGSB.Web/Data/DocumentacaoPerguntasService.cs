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

namespace SGSB.Web.Data
{
    public class DocumentacaoPerguntasService
    {
        HttpClient http;
        public DocumentacaoPerguntasService(HttpClient _http)
        {
            this.http = _http;
        }
        private readonly IJSRuntime js;


        public async Task<List<DocumentacaoPerguntas>> GetDocumentacaoPerguntas()
        {
            var objDocumentacaoPerguntas = await http.GetFromJsonAsync<List<DocumentacaoPerguntas>>( Infra.Constantes.URI +"/API/ListarDocumentacaoPerguntas");

            return objDocumentacaoPerguntas.ToList();

        }
        public async Task<DocumentacaoPerguntas> GetDocumentacaoPerguntasId(int id)
        {
            DocumentacaoPerguntas objDocumentacaoPerguntas = new DocumentacaoPerguntas();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdDocumentacaoPerguntas?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                     objDocumentacaoPerguntas = await response.Content.ReadFromJsonAsync<DocumentacaoPerguntas>();
                    return objDocumentacaoPerguntas;

                }
            }
            return objDocumentacaoPerguntas;
        }
        //public async Task<DocumentacaoPerguntas> EditTipoCondicaoFundacao(int id)
        //{
        //    var objDocumentacaoPerguntas = await http.GetFromJsonAsync<List<DocumentacaoPerguntas>>("/API/ListarDocumentacaoPerguntas");

        //    return objDocumentacaoPerguntas.Where(i => i.Id == id).FirstOrDefault();

        //}
        //public async Task<DocumentacaoPerguntas> VerTipoCondicaoFundacao(int id)
        //{
        //    var objDocumentacaoPerguntas = await http.GetFromJsonAsync<List<DocumentacaoPerguntas>>("/API/ListarDocumentacaoPerguntas");

        //    return objDocumentacaoPerguntas.Where(i => i.Id == id).FirstOrDefault();

        //}
        //public async Task<DocumentacaoPerguntas> Delete(int id)
        //{
        //    var objDocumentacaoPerguntas = await http.GetFromJsonAsync<List<DocumentacaoPerguntas>>("/API/ListarDocumentacaoPerguntas");

        //    return objDocumentacaoPerguntas.Where(i => i.Id == id).FirstOrDefault();

        //}

        public static async Task<bool> CadastrarDocumentacaoPerguntas(DocumentacaoPerguntas model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/ListarDocumentacaoPerguntas/" + model.Id);
                if (response.IsSuccessStatusCode)
                {
                    DocumentacaoPerguntas objDocumentacaoPerguntas = await response.Content.ReadFromJsonAsync<DocumentacaoPerguntas>();


                }
                // HTTP POST - define o produto
                var documentacaoProjetoConstrucaoOperacao = new DocumentacaoPerguntas()
                {
                    Pergunta = model.Pergunta,
             
                    DataCadastro = DateTime.Now,
                    DataAlteracao = DateTime.Now


                };
                response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/AdicionarDocumentacaoPerguntas", documentacaoProjetoConstrucaoOperacao);
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
        public static async Task<bool> EditarDocumentacaoPerguntas(DocumentacaoPerguntas model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdDocumentacaoPerguntas?id=" + model.Id);

                // HTTP POST - define o produto
                var documentacaoProjetoConstrucaoOperacao = new DocumentacaoPerguntas()
                {
                    Id = model.Id,
                    Pergunta = model.Pergunta,
                 
                    DataCadastro = DateTime.Now,
                    DataAlteracao = DateTime.Now


                };


                if (response.IsSuccessStatusCode)
                {
                    response = await cliente.PutAsJsonAsync(Infra.Constantes.URI + "/API/AtualizaDocumentacaoPerguntas/", documentacaoProjetoConstrucaoOperacao);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public async Task<DocumentacaoPerguntas> EditDocumentacaoPerguntas(int id)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdDocumentacaoPerguntas?id=" + id);

                return await response.Content.ReadFromJsonAsync<DocumentacaoPerguntas>();

            }
        }
        public static async Task<bool> DeletarDocumentacaoPerguntas(DocumentacaoPerguntas model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdDocumentacaoPerguntas?id=" + model.Id);

              



                if (response.IsSuccessStatusCode)
                {
                 
                    response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/ExcluirDocumentacaoPerguntas", model);
                    return true;
                }
                return false;
            }
        }


    }
}

