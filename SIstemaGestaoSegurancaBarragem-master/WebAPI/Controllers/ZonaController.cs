using Aplicacao.Aplicacoes;
using Aplicacao.Interfaces;
using Dominio.Interfaces;
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
    public class ZonaController : ControllerBase
    {
        private readonly IAplicacaoZona _IAplicacaoZona;

        public ZonaController(IAplicacaoZona aplicacaoZona)
        {
            _IAplicacaoZona = aplicacaoZona;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpGet("/API/ListarZona")]
        public async Task<List<Zona>> ListarZona()
        {
            return await _IAplicacaoZona.ListarZona();
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpHead("/API/BuscarPorIdZona")]
        public async Task<Zona> BuscarPorIdZona(ZonaModel ZonaModel)
        {
            var objZonaModel = await _IAplicacaoZona.BuscarPorId(ZonaModel.id);
            return objZonaModel;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/AdicionarZona")]
        public async Task<List<Notifica>> AdicionarZona(ZonaModel ZonaModel)
        {
            var objZonaModel = new Zona()
            {
                //Id = ZonaModel.Id,
                area = ZonaModel.area,
                cpf = ZonaModel.cpf,
                dataNascimento = ZonaModel.dataNascimento,
                endereco = ZonaModel.endereco,
                Id = ZonaModel.id,
                idZonaNome = ZonaModel.idZonaNome,
                nomeCidadao = ZonaModel.nomeCidadao,
                numeroPessoas = ZonaModel.numeroPessoas,
                observacao = ZonaModel.observacao,
                numeroTelefone = ZonaModel.numeroTelefone,
                renda = ZonaModel.renda,
                usuario = ZonaModel.usuario

            };

            await _IAplicacaoZona.AdicionarZona(objZonaModel);

            return objZonaModel.Notificacoes;
        }
        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPut("/API/AtualizaZona")]
        public async Task<List<Notifica>> AtualizaZona(ZonaModel ZonaModel)
        {
            var objZona = await _IAplicacaoZona.BuscarPorId(ZonaModel.id);

            objZona.Id = ZonaModel.id > 0 ? ZonaModel.id : objZona.Id;
            objZona.area = ZonaModel.area;
            objZona.cpf = ZonaModel.cpf;
            objZona.dataNascimento = ZonaModel.dataNascimento;
            objZona.endereco = ZonaModel.endereco;
            objZona.Id = ZonaModel.id;
            objZona.idZonaNome = ZonaModel.idZonaNome;
            objZona.nomeCidadao = ZonaModel.nomeCidadao;
            objZona.numeroPessoas = ZonaModel.numeroPessoas;
            objZona.observacao = ZonaModel.observacao;
            objZona.numeroTelefone = ZonaModel.numeroTelefone;
            objZona.renda = ZonaModel.renda;
            objZona.usuario = ZonaModel.usuario;

            await _IAplicacaoZona.AtualizaZona(objZona);

            return objZona.Notificacoes;
        }

        [AllowAnonymous]
        //[Authorize]
        [Produces("application/json")]
        [HttpPost("/API/ExcluirZona")]
        public async Task<List<Notifica>> ExcluirZona([FromBody] int id)
        {
            var objZonaModel = await _IAplicacaoZona.BuscarPorId(id);

            await _IAplicacaoZona.Excluir(objZonaModel);

            return objZonaModel.Notificacoes;
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
        [HttpPost("/API/ListarZonaBarragemId")]
        public async Task<List<Zona>> ListarZonaBarragemId([FromBody] int id)
        {
            List<Zona> lstZona = new List<Zona>();
            lstZona = await _IAplicacaoZona.ListarZonaId(id);


            return lstZona;
        }
    }
}
