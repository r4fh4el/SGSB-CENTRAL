using Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;
using SGSB.Web.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Linq;
using System;
using System.Threading;
using Azure;

namespace SGSB.Web.Data
{
    public class BarragemService
    {

        HttpClient http;
        public BarragemService(HttpClient _http)
        {
            this.http = _http;
        }
        private readonly IJSRuntime js;

        public async Task<List<Barragem>> GetBarragemAsync()
        {
            var objBarragem = await http.GetFromJsonAsync<List<Barragem>>(Infra.Constantes.URI + "/API/ListarBarragem");

            return objBarragem.ToList();

        }

        public async Task<int> GetProximoBarragemIdAsync()
        {
            var objBarragem = await http.GetFromJsonAsync<List<Barragem>>(Infra.Constantes.URI + "/API/ListarBarragem");

            return objBarragem.Any() ? objBarragem.Max(obj => obj.Id) : 1;
        }
        public async Task<List<TipoEmpreendedor>> GetTipoEmpreendedorAsync()
        {
            var objTipoEmpreendedor = await http.GetFromJsonAsync<List<TipoEmpreendedor>>(Infra.Constantes.URI + "/API/ListarTipoEmpreendedor");

            return objTipoEmpreendedor.ToList();

        }

        public async Task<List<DocumentacaoPerguntas>> GetDocumentacaoPerguntasAsync()
        {
            var objDocumentacaoPerguntas = await http.GetFromJsonAsync<List<DocumentacaoPerguntas>>(Infra.Constantes.URI + "/API/ListarDocumentacaoPerguntas");

            return objDocumentacaoPerguntas.ToList();

        }
        public async Task<List<TipoEstruturaBarragem>> GetTipoEstruturaBarragemAsync()
        {
            var objTipoEstruturaBarragem = await http.GetFromJsonAsync<List<TipoEstruturaBarragem>>(Infra.Constantes.URI + "/API/ListarTipoEstruturaBarragem");

            return objTipoEstruturaBarragem.ToList();

        }
        public async Task<List<TipoMaterialBarragem>> GetTipoMaterialBarragemAsync()
        {
            var objTipoMaterialBarragem = await http.GetFromJsonAsync<List<TipoMaterialBarragem>>(Infra.Constantes.URI + "/API/ListarTipoMaterialBarragem");

            return objTipoMaterialBarragem.ToList();

        }

        public async Task<List<DocumentacaoProjetoConstrucaoOperacao>> GetDocumentacaoProjetoConstrucaoOperacaoAsync()
        {
            var lstDocumentacaoProjetoConstrucaoOperacao = await http.GetFromJsonAsync<List<DocumentacaoProjetoConstrucaoOperacao>>(Infra.Constantes.URI + "/API/ListarDocumentacaoProjetoConstrucaoOperacao");

            return lstDocumentacaoProjetoConstrucaoOperacao.ToList();

        }
        public async Task<List<UsoBarragem>> GetUsoBarragem()
        {
            var lstUsoBarragem = await http.GetFromJsonAsync<List<UsoBarragem>>(Infra.Constantes.URI + "/API/ListarUsoBarragem");

            return lstUsoBarragem.ToList();

        }
        public async Task<List<TipoEdificacao>> GetTipoEdificacao()
        {
            var lstTipoEdificacao = await http.GetFromJsonAsync<List<TipoEdificacao>>(Infra.Constantes.URI + "/API/ListarTipoEdificacao");

            return lstTipoEdificacao.ToList();

        }
        public async Task<List<CotaAreaVolume>> GetCotaAreaVolumeAsync()
        {
            var lstCotaAreaVolume = await http.GetFromJsonAsync<List<CotaAreaVolume>>(Infra.Constantes.URI + "/API/ListarCotaAreaVolume");

            return lstCotaAreaVolume.ToList();

        }
        public async Task<List<CotaAreaVolume>> GetCotaAreaVolumeBarragemIdAsync(int idBarragem)
        {
            List<CotaAreaVolume> lstCotaAreaVolume = new List<CotaAreaVolume>();
            CotaAreaVolume objCotaAreaVolume = new CotaAreaVolume();
            using (var cliente = new HttpClient())
            {
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                objCotaAreaVolume.BarragemId = idBarragem;
                var response  = await cliente.GetAsync(Infra.Constantes.URI + "/API/ListarCotaAreaVolumeBarragemId?idBarragem=" + idBarragem);

                try
                {
                    response.EnsureSuccessStatusCode();
                    lstCotaAreaVolume = await response.Content.ReadFromJsonAsync<List<CotaAreaVolume>>();
                    return lstCotaAreaVolume;

                    // Handle success
                }
                catch (HttpRequestException ex)
                {
                    // Handle failure
                }
            }
            return lstCotaAreaVolume.ToList();

        }
        public async Task<List<Instrumentos>> GetInstrumentosAsync()
        {
            var lstInstrumentos = await http.GetFromJsonAsync<List<Instrumentos>>(Infra.Constantes.URI + "/API/ListarInstrumentos");

            return lstInstrumentos.ToList();

        }
        public async Task<List<SistemaDrenagem>> GetSistemaDrenagemAsync()
        {
            var lstSistemaDrenagem = await http.GetFromJsonAsync<List<SistemaDrenagem>>(Infra.Constantes.URI + "/API/ListarSistemaDrenagem");

            return lstSistemaDrenagem.ToList();

        }
        public async Task<List<CondicaoFundacao>> GetCondicaoFundacaoAsync()
        {
            var objCondicaoFundacao = await http.GetFromJsonAsync<List<CondicaoFundacao>>(Infra.Constantes.URI + "/API/ListarCondicaoFundacao");

            return objCondicaoFundacao.ToList();
        }


        public async Task<List<Barragem>> GetBuscarListaPorIdBarragem(int id)
        {
            var objBarragem = await http.GetFromJsonAsync<List<Barragem>>(Infra.Constantes.URI + "/API/BuscarListaPorIdBarragem?id=" + id);

            return objBarragem.ToList();

        }
        public async Task<Barragem> GetBarragemId(int id)
        {
            Barragem objBarragem = new Barragem();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdBarragem?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                    objBarragem = await response.Content.ReadFromJsonAsync<Barragem>();
                    return objBarragem;

                }
            }
            return objBarragem;
        }
        public async Task<Barragem> VerBarragem(int id)
        {
            Barragem objBarragem = new Barragem();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdBarragem?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                    objBarragem = await response.Content.ReadFromJsonAsync<Barragem>();
                    return objBarragem;

                }
            }
            return objBarragem;
        }
        public static async Task<bool> EditarBarragem(Barragem model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdBarragem?id=" + model.Id);

                // HTTP POST - define o produto
                var barragem = new Barragem()
                {
                    Id = model.Id,
                    AlturaMAxima = model.AlturaMAxima,
                    AnoConclusaoObra = model.AnoConclusaoObra,
                    BaciaHidrograficaAbrangencia = model.BaciaHidrograficaAbrangencia,
                    Nome = model.Nome,
                    ComprimentoCoroamento = model.ComprimentoCoroamento,
                    CondicaoFundacao = model.CondicaoFundacao,
                    CondicaoFundacaoId = model.CondicaoFundacaoId,
                    Latitude = model.Latitude,
                    Longitude = model.Longitude,
                    CotaCoroamentoBarragem = model.CotaCoroamentoBarragem,
                    CursoDaguaBarrado = model.CursoDaguaBarrado,
                    IdadeBarragem = model.IdadeBarragem,
                    LarguraCoroamentoBarragem = model.LarguraCoroamentoBarragem,
                    TipoEstruturaBarragem = model.TipoEstruturaBarragem,
                    TipoFundacao = model.TipoFundacao,
                    //Barragem = model.Barragem,
                    Tipo_Estrutura_Id = model.Tipo_Estrutura_Id,
                    Tipo_Material_Id = model.Tipo_Material_Id,
                    DataCadastro = DateTime.Now,
                    DataAlteracao = DateTime.Now


                };


                if (response.IsSuccessStatusCode)
                {
                    response = await cliente.PutAsJsonAsync(Infra.Constantes.URI + "/API/AtualizaBarragem/", barragem);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        public async Task<Barragem> EditBarragem(int id)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdBarragem?id=" + id);

                return await response.Content.ReadFromJsonAsync<Barragem>();

            }
        }

        public static async Task<bool> DeletarBarragem(Barragem model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdBarragem?id=" + model.Id);





                if (response.IsSuccessStatusCode)
                {

                    response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/ExcluirBarragem", model);
                    return true;
                }
                return false;
            }
        }
        public static async Task<bool> CadastrarBarragem(Barragem model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/ListarBarragem/");

                // HTTP POST - define o produto
                var barragem = new BarragemViewModel()
                {
                    AlturaMAxima = model.AlturaMAxima,
                    AnoConclusaoObra = model.AnoConclusaoObra,
                    BaciaHidrograficaAbrangencia = model.BaciaHidrograficaAbrangencia,
                    Nome = model.Nome,
                    ComprimentoCoroamento = model.ComprimentoCoroamento,
                    //CondicaoFundacao = model.CondicaoFundacao,
                    CondicaoFundacaoId = model.CondicaoFundacaoId,
                    CotaCoroamentoBarragem = model.CotaCoroamentoBarragem,
                    CursoDaguaBarrado = model.CursoDaguaBarrado,
                    IdadeBarragem = model.IdadeBarragem,
                    LarguraCoroamentoBarragem = model.LarguraCoroamentoBarragem,
                    TipoFundacao = model.TipoFundacao,
                    //Barragem = model.Barragem,
                    Tipo_Estrutura_Id = model.Tipo_Estrutura_Id,
                    Tipo_Material_Id = model.Tipo_Material_Id,
                    Latitude = model.Latitude,
                    Longitude = model.Longitude,
                    //TipoEstruturaBarragem = model.TipoEstruturaBarragem,






                };
                response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/AdicionarBarragem/", barragem);
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
        public static async Task<bool> DeletarBarragemRelacionais(int id)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + @"/API/DeletarBarragemRelacionais", id);

                if (response.IsSuccessStatusCode)
                {

                    return true;
                }

            }
            return false;
        }
    }
}
