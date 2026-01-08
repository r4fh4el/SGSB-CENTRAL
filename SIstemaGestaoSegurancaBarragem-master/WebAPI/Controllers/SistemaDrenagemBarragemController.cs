using Aplicacao.Aplicacoes;
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
    public class SistemaDrenagemBarragemController : ControllerBase
    {
        private readonly IAplicacaoSistemaDrenagemBarragem _IAplicacaoSistemaDrenagemBarragem;

        public SistemaDrenagemBarragemController(IAplicacaoSistemaDrenagemBarragem aplicacaoSistemaDrenagemBarragem)
        {
            _IAplicacaoSistemaDrenagemBarragem = aplicacaoSistemaDrenagemBarragem;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/ListarSistemaDrenagemBarragem")]
        public async Task<List<SistemaDrenagemBarragem>> ListarSistemaDrenagemBarragem()
        {
            return await _IAplicacaoSistemaDrenagemBarragem.ListarSistemaDrenagemBarragem();
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/BuscarPorIdSistemaDrenagemBarragem")]
        public async Task<SistemaDrenagemBarragem> BuscarPorIdSistemaDrenagemBarragem(int id)
        {
            var objSistemaDrenagemBarragemModel = await _IAplicacaoSistemaDrenagemBarragem.BuscarPorId(id);
            return objSistemaDrenagemBarragemModel;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/AdicionarSistemaDrenagemBarragem")]
        public async Task<List<Notifica>> AdicionarSistemaDrenagemBarragem([FromBody] SistemaDrenagemBarragemModel SistemaDrenagemBarragemModel)
        {
            var objSistemaDrenagemBarragemModel = new SistemaDrenagemBarragem()
            {
                Id = SistemaDrenagemBarragemModel.Id,
                BarragemId = SistemaDrenagemBarragemModel.BarragemId,
                SistemaDrenagemId = SistemaDrenagemBarragemModel.SistemaDrenagemId,
                DataAlteracao = DateTime.Now,
                DataCadastro = DateTime.Now
            };

             await _IAplicacaoSistemaDrenagemBarragem.AdicionarSistemaDrenagemBarragem(objSistemaDrenagemBarragemModel);

            return objSistemaDrenagemBarragemModel.Notificacoes;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPut("/API/AtualizaSistemaDrenagemBarragem")]
        public async Task<List<Notifica>> AtualizaSistemaDrenagemBarragem([FromBody] SistemaDrenagemBarragemModel SistemaDrenagemBarragemModel)
        {
            var objSistemaDrenagemBarragem = await _IAplicacaoSistemaDrenagemBarragem.BuscarPorId(SistemaDrenagemBarragemModel.Id);

            objSistemaDrenagemBarragem.Id = SistemaDrenagemBarragemModel.Id;
            objSistemaDrenagemBarragem.BarragemId = SistemaDrenagemBarragemModel.BarragemId;
            objSistemaDrenagemBarragem.SistemaDrenagemId = SistemaDrenagemBarragemModel.SistemaDrenagemId;
            objSistemaDrenagemBarragem.DataAlteracao = DateTime.Now;

            await _IAplicacaoSistemaDrenagemBarragem.AtualizaSistemaDrenagemBarragem(objSistemaDrenagemBarragem);

            return objSistemaDrenagemBarragem.Notificacoes;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/ExcluirSistemaDrenagemBarragem")]
        public async Task<List<Notifica>> ExcluirSistemaDrenagemBarragem(SistemaDrenagemBarragemModel SistemaDrenagemBarragemModel)
        {
            var objSistemaDrenagemBarragemModel = await _IAplicacaoSistemaDrenagemBarragem.BuscarPorId(SistemaDrenagemBarragemModel.Id);

            await _IAplicacaoSistemaDrenagemBarragem.Excluir(objSistemaDrenagemBarragemModel);

            return objSistemaDrenagemBarragemModel.Notificacoes;
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

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/ListarSistemaDrenagemBarragemBarragemId")]
        public async Task<List<SistemaDrenagemBarragem>> ListarSistemaDrenagemBarragemBarragemId([FromBody] int idBarragem)
        {
            List<SistemaDrenagemBarragem> lstSistemaDrenagemBarragem = new List<SistemaDrenagemBarragem>();
            lstSistemaDrenagemBarragem = await _IAplicacaoSistemaDrenagemBarragem.ListarSistemaDrenagemBarragemBarragemId(idBarragem);


            return lstSistemaDrenagemBarragem;
        }
    }
}
