using Aplicacao.Interfaces;
using Entidades.Entidades;
using Entidades.Notificacoes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndiceCaracterizacaoBHController : ControllerBase
    {
        private readonly IAplicacaoIndiceCaracterizacaoBH _IAplicacaoIndiceCaracterizacaoBH;

        public IndiceCaracterizacaoBHController(IAplicacaoIndiceCaracterizacaoBH aplicacaoIndiceCaracterizacaoBH)
        {
            _IAplicacaoIndiceCaracterizacaoBH = aplicacaoIndiceCaracterizacaoBH;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/ListarIndiceCaracterizacaoBH")]
        public async Task<List<IndiceCaracterizacaoBH>> ListarIndiceCaracterizacaoBH()
        {
            return await _IAplicacaoIndiceCaracterizacaoBH.ListarIndiceCaracterizacaoBH();
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/BuscarPorIdIndiceCaracterizacaoBH")]
        public async Task<IndiceCaracterizacaoBH> BuscarPorIdIndiceCaracterizacaoBH( int id)
        {
            var objIndiceCaracterizacaoBHModel = await _IAplicacaoIndiceCaracterizacaoBH.BuscarPorId(id);
            return objIndiceCaracterizacaoBHModel;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/AdicionarIndiceCaracterizacaoBH")]
        public async Task<List<Notifica>> AdicionarIndiceCaracterizacaoBH(IndiceCaracterizacaoBHModel indiceCaracterizacaoBHModel)
        {
            var objIndiceCaracterizacaoBHModel = new IndiceCaracterizacaoBH()
            {
                Id = indiceCaracterizacaoBHModel.Id,
                AltitudeAltimetricaBaciaKM = indiceCaracterizacaoBHModel.AltitudeAltimetricaBaciaKM,
                AltitudeAltimetricaBaciaM = indiceCaracterizacaoBHModel.AltitudeAltimetricaBaciaM,
                AltitudeMaximaBacia = indiceCaracterizacaoBHModel.AltitudeMaximaBacia,
                AreaBaciaHidrografica = indiceCaracterizacaoBHModel.AreaBaciaHidrografica,
                Barragem_ID = indiceCaracterizacaoBHModel.Barragem_ID,
                ComprimentoAxialBacia = indiceCaracterizacaoBHModel.ComprimentoAxialBacia,
                ComprimentoRioPrincipal = indiceCaracterizacaoBHModel.ComprimentoRioPrincipal,
                ComprimentoVetorialRioPrincipal = indiceCaracterizacaoBHModel.ComprimentoVetorialRioPrincipal,
                Perimetro = indiceCaracterizacaoBHModel.Perimetro,
                ResultadoCoeficienteCompacidade = indiceCaracterizacaoBHModel.ResultadoCoeficienteCompacidade,
                ResultadoCoeficienteManutencao = indiceCaracterizacaoBHModel.ResultadoCoeficienteManutencao,
                ResultadoDensidadeDrenagem = indiceCaracterizacaoBHModel.ResultadoDensidadeDrenagem,
                ResultadoFatorForma = indiceCaracterizacaoBHModel.ResultadoFatorForma,
                ResultadoGradienteCanais = indiceCaracterizacaoBHModel.ResultadoGradienteCanais,
                ResultadoIndiceCircularidade = indiceCaracterizacaoBHModel.ResultadoIndiceCircularidade,
                ResultadoIndiceRugosidade = indiceCaracterizacaoBHModel.ResultadoIndiceRugosidade,
                ResultadoRelacaoRelevo = indiceCaracterizacaoBHModel.ResultadoRelacaoRelevo,
                ResultadoSinuosidadeCursoDagua = indiceCaracterizacaoBHModel.ResultadoSinuosidadeCursoDagua,
                AltitudeVetorialRioPrincipal = indiceCaracterizacaoBHModel.AltitudeVetorialRioPrincipal
            };

             await _IAplicacaoIndiceCaracterizacaoBH.Adicionar(objIndiceCaracterizacaoBHModel);

            return objIndiceCaracterizacaoBHModel.Notificacoes;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPut("/API/AtualizaIndiceCaracterizacaoBH")]
        public async Task<List<Notifica>> AtualizaIndiceCaracterizacaoBH(IndiceCaracterizacaoBHModel indiceCaracterizacaoBHModel)
        {
            var objIndiceCaracterizacaoBH = await _IAplicacaoIndiceCaracterizacaoBH.BuscarPorId(indiceCaracterizacaoBHModel.Id);

            objIndiceCaracterizacaoBH.Id = indiceCaracterizacaoBHModel.Id;
                objIndiceCaracterizacaoBH.AltitudeAltimetricaBaciaKM = indiceCaracterizacaoBHModel.AltitudeAltimetricaBaciaKM;
                objIndiceCaracterizacaoBH.AltitudeAltimetricaBaciaM = indiceCaracterizacaoBHModel.AltitudeAltimetricaBaciaM;
                objIndiceCaracterizacaoBH.AltitudeMaximaBacia = indiceCaracterizacaoBHModel.AltitudeMaximaBacia;
                objIndiceCaracterizacaoBH.AreaBaciaHidrografica = indiceCaracterizacaoBHModel.AreaBaciaHidrografica;
                objIndiceCaracterizacaoBH.Barragem_ID = indiceCaracterizacaoBHModel.Barragem_ID;
                objIndiceCaracterizacaoBH.ComprimentoAxialBacia = indiceCaracterizacaoBHModel.ComprimentoAxialBacia;
                objIndiceCaracterizacaoBH.ComprimentoRioPrincipal = indiceCaracterizacaoBHModel.ComprimentoRioPrincipal;
                objIndiceCaracterizacaoBH.ComprimentoVetorialRioPrincipal = indiceCaracterizacaoBHModel.ComprimentoVetorialRioPrincipal;

                objIndiceCaracterizacaoBH.Perimetro = indiceCaracterizacaoBHModel.Perimetro;
                objIndiceCaracterizacaoBH.ResultadoCoeficienteCompacidade = indiceCaracterizacaoBHModel.ResultadoCoeficienteCompacidade;
                objIndiceCaracterizacaoBH.ResultadoCoeficienteManutencao = indiceCaracterizacaoBHModel.ResultadoCoeficienteManutencao;
                objIndiceCaracterizacaoBH.ResultadoDensidadeDrenagem = indiceCaracterizacaoBHModel.ResultadoDensidadeDrenagem;
                objIndiceCaracterizacaoBH.ResultadoFatorForma = indiceCaracterizacaoBHModel.ResultadoFatorForma;
                objIndiceCaracterizacaoBH.ResultadoGradienteCanais = indiceCaracterizacaoBHModel.ResultadoGradienteCanais;
                objIndiceCaracterizacaoBH.ResultadoIndiceCircularidade = indiceCaracterizacaoBHModel.ResultadoIndiceCircularidade;
                objIndiceCaracterizacaoBH.ResultadoIndiceRugosidade = indiceCaracterizacaoBHModel.ResultadoIndiceRugosidade;
                objIndiceCaracterizacaoBH.ResultadoRelacaoRelevo = indiceCaracterizacaoBHModel.ResultadoRelacaoRelevo;
                objIndiceCaracterizacaoBH.ResultadoSinuosidadeCursoDagua = indiceCaracterizacaoBHModel.ResultadoSinuosidadeCursoDagua;
            objIndiceCaracterizacaoBH.AltitudeVetorialRioPrincipal = indiceCaracterizacaoBHModel.AltitudeVetorialRioPrincipal;

            await _IAplicacaoIndiceCaracterizacaoBH.Atualizar(objIndiceCaracterizacaoBH);

            return objIndiceCaracterizacaoBH.Notificacoes;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/ExcluirIndiceCaracterizacaoBH")]
        public async Task<List<Notifica>> ExcluirIndiceCaracterizacaoBH(IndiceCaracterizacaoBH indiceCaracterizacaoBHModel)
        {
            var objIndiceCaracterizacaoBHModel = await _IAplicacaoIndiceCaracterizacaoBH.BuscarPorId(indiceCaracterizacaoBHModel.Id);

            await _IAplicacaoIndiceCaracterizacaoBH.Excluir(objIndiceCaracterizacaoBHModel);

            return objIndiceCaracterizacaoBHModel.Notificacoes;
        }

  
        private async Task<string> RetornarIdUsuarioLogado()
        {
            if (User != null)
            {
                var idUsuario = User.FindFirst("idUsuario");
                return idUsuario.Value;
            }
            else 
            {
                return string.Empty;
            }
        }
    }
}
