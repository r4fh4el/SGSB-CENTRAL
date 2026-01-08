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
using Infraestrutura.Migrations;
using InformacoesComplementares = Model.InformacoesComplementares;

namespace SGSB.Web.Data
{
    public class InformacoesComplementaresService
    {

        HttpClient http;
        public InformacoesComplementaresService(HttpClient _http)
        {
            this.http = _http;
        }
        private readonly IJSRuntime js;

        public async Task<List<Model.InformacoesComplementares>> GetInformacoesComplementaresAsync()
        {
            var objInformacoesComplementares = await http.GetFromJsonAsync<List<Model.InformacoesComplementares>>(Infra.Constantes.URI + "/API/ListarInformacoesComplementares");

            return objInformacoesComplementares.ToList();

        }

        public async Task<int> GetProximoInformacoesComplementaresIdAsync()
        {
            var objInformacoesComplementares = await http.GetFromJsonAsync<List<Model.InformacoesComplementares>>(Infra.Constantes.URI + "/API/ListarInformacoesComplementares");

            int proximoId = 0;

            if (objInformacoesComplementares.ToList().Count == 0)
            {
                proximoId = 1;
            }
            else
            {
                proximoId = objInformacoesComplementares.ToList().LastOrDefault().Id + 1;
            }

            return proximoId;

        }
 


        public static async Task<Model.InformacoesComplementares> GetInformacoesComplementaresId(int id)
        {
            Model.InformacoesComplementares objInformacoesComplementares = new Model.InformacoesComplementares();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdInformacoesComplementares?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                    objInformacoesComplementares = await response.Content.ReadFromJsonAsync<Model.InformacoesComplementares>();
                    return objInformacoesComplementares;

                }
            }
            return objInformacoesComplementares;
        }
        public async Task<Model.InformacoesComplementares> VerInformacoesComplementares(int id)
        {
            Model.InformacoesComplementares objInformacoesComplementares = new Model.InformacoesComplementares();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdInformacoesComplementares?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                    objInformacoesComplementares = await response.Content.ReadFromJsonAsync<Model.InformacoesComplementares>();
                    return objInformacoesComplementares;

                }
            }
            return objInformacoesComplementares;
        }

        public static async Task<bool> EditarInformacoesComplementares(InformacoesComplementaresModel informacoesComplementaresModel)
        {
            using (var cliente = new HttpClient())
            {
                //cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                //HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdInformacoesComplementares?id=" + informacoesComplementaresModel.Id);

                // HTTP POST - define o produto
                var informacoesComplementares = new InformacoesComplementares()
                {
                    DataAlteracao = informacoesComplementaresModel.DataAlteracao,
                    DataCadastro = informacoesComplementaresModel.DataCadastro,
                    AnoUltimaReforma = informacoesComplementaresModel.AnoUltimaReforma,
                    HistoricoIndicidenteAcidente = informacoesComplementaresModel.HistoricoIndicidenteAcidente,
                    NomeSetor = informacoesComplementaresModel.NomeSetor,
                    NumeroPortariaLicencaOperacao = informacoesComplementaresModel.NumeroPortariaLicencaOperacao,
                    PossuiEscritorioLocalBarragem = informacoesComplementaresModel.PossuiEscritorioLocalBarragem,
                    NumeroPortariaOutorga = informacoesComplementaresModel.NumeroPortariaOutorga,
                    PossuiEdificacaoLocalBarragem = informacoesComplementaresModel.PossuiEdificacaoLocalBarragem,
                    TemEquipeOperacaoBarragem = informacoesComplementaresModel.TemEquipeOperacaoBarragem,
                    TemHistoricoIndicidenteAcidente = informacoesComplementaresModel.TemHistoricoIndicidenteAcidente,
                    TemLicencaOperacao = informacoesComplementaresModel.TemLicencaOperacao,
                    TemOperador24 = informacoesComplementaresModel.TemOperador24,
                    TemOutorgaConstrucao = informacoesComplementaresModel.TemOutorgaConstrucao,
                    TemVigia = informacoesComplementaresModel.TemVigia,
                    VazaoMinimaRestituicaoAno = informacoesComplementaresModel.VazaoMinimaRestituicaoAno,
                    Id = informacoesComplementaresModel.Id,
                    BarragemId = informacoesComplementaresModel.BarragemId


                };

                HttpResponseMessage response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/AtualizaInformacoesComplementare/", informacoesComplementares);

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
        //public static async Task<bool> EditarInformacoesComplementares(InformacoesComplementaresModel informacoesComplementaresModel)
        //{
        //    using (var cliente = new HttpClient())
        //    {
        //        //cliente.BaseAddress = new Uri(Infra.Constantes.URI);
        //        cliente.DefaultRequestHeaders.Accept.Clear();
        //        cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        // HTTP GET
        //        HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdInformacoesComplementares?id=" + informacoesComplementaresModel.Id);

        //        // HTTP POST - define o produto
        //        var infoModel = new InformacoesComplementaresModel()

        //        {
        //            DataAlteracao = informacoesComplementaresModel.DataAlteracao,
        //            DataCadastro = informacoesComplementaresModel.DataCadastro,
        //            AnoUltimaReforma = informacoesComplementaresModel.AnoUltimaReforma,
        //            HistoricoIndicidenteAcidente = informacoesComplementaresModel.HistoricoIndicidenteAcidente,
        //            NomeSetor = informacoesComplementaresModel.NomeSetor,
        //            NumeroPortariaLicencaOperacao = informacoesComplementaresModel.NumeroPortariaLicencaOperacao,
        //            PossuiEscritorioLocalBarragem = informacoesComplementaresModel.PossuiEscritorioLocalBarragem,
        //            NumeroPortariaOutorga = informacoesComplementaresModel.NumeroPortariaOutorga,
        //            PossuiEdificacaoLocalBarragem = informacoesComplementaresModel.PossuiEdificacaoLocalBarragem,
        //            TemEquipeOperacaoBarragem = informacoesComplementaresModel.TemEquipeOperacaoBarragem,
        //            TemHistoricoIndicidenteAcidente = informacoesComplementaresModel.TemHistoricoIndicidenteAcidente,
        //            TemLicencaOperacao = informacoesComplementaresModel.TemLicencaOperacao,
        //            TemOperador24 = informacoesComplementaresModel.TemOperador24,
        //            TemOutorgaConstrucao = informacoesComplementaresModel.TemOutorgaConstrucao,
        //            TemVigia = informacoesComplementaresModel.TemVigia,
        //            VazaoMinimaRestituicaoAno = informacoesComplementaresModel.VazaoMinimaRestituicaoAno,
        //            Id = informacoesComplementaresModel.Id,
        //            BarragemId = informacoesComplementaresModel.BarragemId


        //        };

        //        response = await cliente.PutAsJsonAsync(Infra.Constantes.URI + "/API/AtualizaInformacoesComplementares", infoModel);

        //        if (response.IsSuccessStatusCode)
        //        {
        //              return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}


        public async Task<Model.InformacoesComplementares> EditInformacoesComplementares(int id)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdInformacoesComplementares?id=" + id);

                return await response.Content.ReadFromJsonAsync<Model.InformacoesComplementares>();

            }
        }

        public static async Task<bool> DeletarInformacoesComplementares(Model.InformacoesComplementares model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/BuscarPorIdInformacoesComplementares?id=" + model.Id);
                
                if (response.IsSuccessStatusCode)
                {

                    response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/ExcluirInformacoesComplementares", model);
                    return true;
                }
                return false;
            }
        }

        public async Task<int> GetProximoInformacoesComplementaresAsync()
        {
            var objInformacoesComplementares = await http.GetFromJsonAsync<List<Model.InformacoesComplementares>>(Infra.Constantes.URI + "/API/ListarInformacoesComplementares");

            int proximoId = 0;

            if (objInformacoesComplementares.ToList().Count == 0)
            {
                proximoId = 1;
            }
            else
            {
                proximoId = objInformacoesComplementares.ToList().LastOrDefault().Id + 1;
            }

            return proximoId;

        }

        public static async Task<bool> CadastrarInformacoesComplementares([FromBody] InformacoesComplementaresModel model)
        {
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
               
                // HTTP GET
                //HttpResponseMessage response = await cliente.GetAsync(Infra.Constantes.URI + "/API/ListarInformacoesComplementares/");

                // HTTP POST - define o produto
                var informacoesComplementaresModel = new InformacoesComplementaresModel()
                {
                    Id = model.Id,

                    DataAlteracao = model.DataAlteracao,
                    
                    DataCadastro = model.DataCadastro,
                    BarragemId = model.BarragemId,
                    AnoUltimaReforma = model.BarragemId,
                    HistoricoIndicidenteAcidente = model.HistoricoIndicidenteAcidente,
                    NumeroPortariaLicencaOperacao = model.NumeroPortariaLicencaOperacao,
                    NomeSetor = model.NomeSetor,
                    NumeroPortariaOutorga = model.NumeroPortariaOutorga,
               
                    PossuiEdificacaoLocalBarragem = model.PossuiEdificacaoLocalBarragem,
                    PossuiEscritorioLocalBarragem = model.PossuiEscritorioLocalBarragem,
                    TemEquipeOperacaoBarragem = model.TemEquipeOperacaoBarragem,
                    TemLicencaOperacao = model.TemLicencaOperacao,
                    TemHistoricoIndicidenteAcidente = model.TemHistoricoIndicidenteAcidente,
                    TemOperador24 = model.TemOperador24,
                    TemOutorgaConstrucao = model.TemOutorgaConstrucao,
                    TemVigia = model.TemVigia,
                    VazaoMinimaRestituicaoAno = model.VazaoMinimaRestituicaoAno,
                };
                HttpResponseMessage response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/AdicionarInformacoesComplementares/", informacoesComplementaresModel);
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


        public static async Task<InformacoesComplementaresModel> ListarInformacoesComplementaresBarragemId(int idBarragem)
        {
            List<InformacoesComplementaresModel> objInformacoesComplementares = new List<InformacoesComplementaresModel>();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(Infra.Constantes.URI);
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                HttpResponseMessage response = await cliente.PostAsJsonAsync(Infra.Constantes.URI + "/API/ListarInformacoesComplementaresBarragemId", idBarragem);
                if (response.IsSuccessStatusCode)
                {

                    objInformacoesComplementares = await response.Content.ReadFromJsonAsync<List<InformacoesComplementaresModel>>();

                    return objInformacoesComplementares.FirstOrDefault();
                }
            }
            return objInformacoesComplementares.FirstOrDefault();
        }
    }
}
