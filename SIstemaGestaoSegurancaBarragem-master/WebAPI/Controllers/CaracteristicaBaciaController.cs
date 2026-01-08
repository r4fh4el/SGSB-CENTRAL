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
    public class CaracteristicaBaciaController : ControllerBase
    {
        private readonly IAplicacaoCaracteristicaBacia _IAplicacaoCaracteristicaBacia;

        public CaracteristicaBaciaController(IAplicacaoCaracteristicaBacia aplicacaoCaracteristicaBacia)
        {
            _IAplicacaoCaracteristicaBacia = aplicacaoCaracteristicaBacia;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/ListarCaracteristicaBacia")]
        public async Task<List<CaracteristicaBacia>> ListarCaracteristicaBacia()
        {
            return await _IAplicacaoCaracteristicaBacia.ListarCaracteristicaBacia();
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/BuscarPorIdCaracteristicaBacia")]
        public async Task<CaracteristicaBacia> BuscarPorIdCaracteristicaBacia( int id)
        {
            var objCaracteristicaBaciaModel = await _IAplicacaoCaracteristicaBacia.BuscarPorId(id);
            return objCaracteristicaBaciaModel;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/AdicionarCaracteristicaBacia")]
        public async Task<List<Notifica>> AdicionarCaracteristicaBacia(CaracteristicaBacia caracteristicaBaciaModel)
        {
            var objCaracteristicaBaciaModel = new CaracteristicaBacia()
            {
                Id = caracteristicaBaciaModel.Id,
                nome = caracteristicaBaciaModel.nome,
                AltitudeAltimetricaKm = caracteristicaBaciaModel.AltitudeAltimetricaKm,
                AltitudeAltimetricaM = caracteristicaBaciaModel.AltitudeAltimetricaM,
                AltitudeMaxima = caracteristicaBaciaModel.AltitudeMaxima,
                AltitudeMinima = caracteristicaBaciaModel.AltitudeMinima,
                AreaBacia = caracteristicaBaciaModel.AreaBacia,
                AreaDrenagem = caracteristicaBaciaModel.AreaDrenagem,
                ComprimentoAxial = caracteristicaBaciaModel.ComprimentoAxial,
                ComprimentoRioPrincipal = caracteristicaBaciaModel.ComprimentoRioPrincipal,
                ComprimentototalRio = caracteristicaBaciaModel.ComprimentototalRio,
                ComprimentoVetorialRioPrincipal = caracteristicaBaciaModel.ComprimentoVetorialRioPrincipal,
                cursoHidrico = caracteristicaBaciaModel.cursoHidrico,
                Declividade = caracteristicaBaciaModel.Declividade,
                DefinicaoPadraoDrenagem = caracteristicaBaciaModel.DefinicaoPadraoDrenagem,
                EnumCaracteristicaPredominanteBacia = caracteristicaBaciaModel.EnumCaracteristicaPredominanteBacia,
                FlagSubBacia = caracteristicaBaciaModel.FlagSubBacia,
                MunicipioFozRio = caracteristicaBaciaModel.MunicipioFozRio,
                MunicipioNascenteRio = caracteristicaBaciaModel.MunicipioNascenteRio,
                Perimetro = caracteristicaBaciaModel.Perimetro,
                UsoSoloPredominante = caracteristicaBaciaModel.UsoSoloPredominante,
                UsoSoloPredominanteId = caracteristicaBaciaModel.UsoSoloPredominanteId
                
            };

             await _IAplicacaoCaracteristicaBacia.Adicionar(caracteristicaBaciaModel);

            return objCaracteristicaBaciaModel.Notificacoes;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPut("/API/AtualizaCaracteristicaBacia")]
        public async Task<List<Notifica>> AtualizaCaracteristicaBacia(CaracteristicaBacia caracteristicaBaciaModel)
        {
            var objCaracteristicaBacia = await _IAplicacaoCaracteristicaBacia.BuscarPorId(caracteristicaBaciaModel.Id);

            objCaracteristicaBacia.Id = caracteristicaBaciaModel.Id;
                 objCaracteristicaBacia.nome = caracteristicaBaciaModel.nome;
                 objCaracteristicaBacia.AltitudeAltimetricaKm = caracteristicaBaciaModel.AltitudeAltimetricaKm;
                 objCaracteristicaBacia.AltitudeAltimetricaM = caracteristicaBaciaModel.AltitudeAltimetricaM;
                 objCaracteristicaBacia.AltitudeMaxima = caracteristicaBaciaModel.AltitudeMaxima;
                 objCaracteristicaBacia.AltitudeMinima = caracteristicaBaciaModel.AltitudeMinima;
                 objCaracteristicaBacia.AreaBacia = caracteristicaBaciaModel.AreaBacia;
                 objCaracteristicaBacia.AreaDrenagem = caracteristicaBaciaModel.AreaDrenagem;
                 objCaracteristicaBacia.ComprimentoAxial = caracteristicaBaciaModel.ComprimentoAxial;
                 objCaracteristicaBacia.ComprimentoRioPrincipal = caracteristicaBaciaModel.ComprimentoRioPrincipal;
                 objCaracteristicaBacia.ComprimentototalRio = caracteristicaBaciaModel.ComprimentototalRio;
                 objCaracteristicaBacia.ComprimentoVetorialRioPrincipal = caracteristicaBaciaModel.ComprimentoVetorialRioPrincipal;
                 objCaracteristicaBacia.cursoHidrico = caracteristicaBaciaModel.cursoHidrico;
                 objCaracteristicaBacia.Declividade = caracteristicaBaciaModel.Declividade;
                 objCaracteristicaBacia.DefinicaoPadraoDrenagem = caracteristicaBaciaModel.DefinicaoPadraoDrenagem;
                 objCaracteristicaBacia.EnumCaracteristicaPredominanteBacia = caracteristicaBaciaModel.EnumCaracteristicaPredominanteBacia;
                 objCaracteristicaBacia.FlagSubBacia = caracteristicaBaciaModel.FlagSubBacia;
                 objCaracteristicaBacia.MunicipioFozRio = caracteristicaBaciaModel.MunicipioFozRio;
                 objCaracteristicaBacia.MunicipioNascenteRio = caracteristicaBaciaModel.MunicipioNascenteRio;
                 objCaracteristicaBacia.Perimetro = caracteristicaBaciaModel.Perimetro;
                 objCaracteristicaBacia.UsoSoloPredominante = caracteristicaBaciaModel.UsoSoloPredominante;
            objCaracteristicaBacia.UsoSoloPredominanteId = caracteristicaBaciaModel.UsoSoloPredominanteId;

            await _IAplicacaoCaracteristicaBacia.Atualizar(caracteristicaBaciaModel);

            return objCaracteristicaBacia.Notificacoes;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/ExcluirCaracteristicaBacia")]
        public async Task<List<Notifica>> ExcluirCaracteristicaBacia(CaracteristicaBacia caracteristicaBaciaModel)
        {
            var objCaracteristicaBaciaModel = await _IAplicacaoCaracteristicaBacia.BuscarPorId(caracteristicaBaciaModel.Id);

            await _IAplicacaoCaracteristicaBacia.Excluir(objCaracteristicaBaciaModel);

            return objCaracteristicaBaciaModel.Notificacoes;
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
