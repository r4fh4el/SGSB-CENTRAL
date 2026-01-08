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
    public class ReservatorioService
    {

        HttpClient http;
        public ReservatorioService(HttpClient _http)
        {
            this.http = _http;
        }
        private readonly IJSRuntime js;

        public async Task<List<Reservatorio>> GetReservatorioAsync()
        {
            var objReservatorio = await http.GetFromJsonAsync<List<Reservatorio>>(Infra.Constantes.URI + "/API/ListarReservatorio");

            return objReservatorio.ToList();

        }

        public async Task<int> GetProximoReservatorioIdAsync()
        {
            var objReservatorio = await http.GetFromJsonAsync<List<Reservatorio>>(Infra.Constantes.URI + "/API/ListarReservatorio");

            int proximoId = 0;

            if (objReservatorio.ToList().Count == 0)
            {
                proximoId = 1;
            }
            else
            {
                proximoId = objReservatorio.ToList().LastOrDefault().Id + 1;
            }

            return proximoId;

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


        public async Task<List<DocumentacaoProjetoConstrucaoOperacao>> GetDocumentacaoProjetoConstrucaoOperacaoAsync()
        {
            var lstDocumentacaoProjetoConstrucaoOperacao = await http.GetFromJsonAsync<List<DocumentacaoProjetoConstrucaoOperacao>>(Infra.Constantes.URI + "/API/ListarDocumentacaoProjetoConstrucaoOperacao");

            return lstDocumentacaoProjetoConstrucaoOperacao.ToList();

        }



        public static async Task<Reservatorio> GetReservatorioId(int id)
        {
            Reservatorio objReservatorio = new Reservatorio();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdReservatorio?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                    objReservatorio = await response.Content.ReadFromJsonAsync<Reservatorio>();
                    return objReservatorio;

                }
            }
            return objReservatorio;
        }
        public async Task<Reservatorio> VerReservatorio(int id)
        {
            Reservatorio objReservatorio = new Reservatorio();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdReservatorio?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                    objReservatorio = await response.Content.ReadFromJsonAsync<Reservatorio>();
                    return objReservatorio;

                }
            }
            return objReservatorio;
        }
        public static async Task<bool> EditarReservatorio(ReservatorioModel model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
             
                // HTTP POST - define o produto
                var reservatorioModel = new ReservatorioModel()
                {
                    Id = model.Id,
                      DataAlteracao = model.DataAlteracao,
                    DataCadastro = model.DataCadastro,
                    BordaLivre = model.BordaLivre,
                    CapacidadeTotalReservatorio = model.CapacidadeTotalReservatorio,
                    MaximoMaximorum = model.MaximoMaximorum,
                    MaximoNormal = model.MaximoNormal,
                    MinimoOperacional = model.MinimoOperacional,
                    VolumeMorto = model.VolumeMorto,
                    VolumeUtilReservatorio = model.VolumeUtilReservatorio,
                    BarragemId = model.BarragemId

                };

                HttpResponseMessage response = await cliente.PutAsJsonAsync(Infra.Constantes.URI + "/API/AtualizaReservatorio/", reservatorioModel);

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


        public async Task<Reservatorio> EditReservatorio(int id)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdReservatorio?id=" + id);

                return await response.Content.ReadFromJsonAsync<Reservatorio>();

            }
        }

        public static async Task<bool> DeletarReservatorio(Reservatorio model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdReservatorio?id=" + model.Id);
                
                if (response.IsSuccessStatusCode)
                {

                    response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/ExcluirReservatorio", model);
                    return true;
                }
                return false;
            }
        }
        public static async Task<bool> CadastrarReservatorio(ReservatorioModel model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
               
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/ListarReservatorio/");
           
                // HTTP POST - define o produto
                var reservatorio = new ReservatorioModel()
                {
                    //Id = model.Id,
           
                    DataAlteracao = model.DataAlteracao,
                    DataCadastro = model.DataCadastro,
                   BarragemId = model.BarragemId,
                 
                    BordaLivre = model.BordaLivre,
                    CapacidadeTotalReservatorio = model.CapacidadeTotalReservatorio,
                    MaximoMaximorum = model.MaximoMaximorum,
                    MaximoNormal = model.MaximoNormal,
                    MinimoOperacional = model.MinimoOperacional,
                    VolumeMorto = model.VolumeMorto,
                    VolumeUtilReservatorio = model.VolumeUtilReservatorio,
           

                };
                response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/AdicionarReservatorio/", reservatorio);
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

        public static async Task<ReservatorioModel> ListarReservatorioBarragemId(int idBarragem)
        {
            List<ReservatorioModel> objReservatorio = new List<ReservatorioModel>();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/ListarReservatorioBarragemId", idBarragem);
                if (response.IsSuccessStatusCode)
                {

                    objReservatorio = await response.Content.ReadFromJsonAsync<List<ReservatorioModel>>();

                    return objReservatorio.FirstOrDefault();
                }
            }
            return objReservatorio.FirstOrDefault();
        }
    }
}
