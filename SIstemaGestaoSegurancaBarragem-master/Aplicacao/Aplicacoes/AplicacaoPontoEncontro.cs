using Aplicacao.Interfaces;
using Dominio.Interfaces;
using Dominio.Interfaces.InterfaceServicos;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Aplicacao.Aplicacoes
{
    public class AplicacaoPontoEncontro : IAplicacaoPontoEncontro
    {
        // INTERFACE DOMINIO
        IPontoEncontro _IPontoEncontro;

        // INTERFACE SERVICO
        IServicoPontoEncontro _IServicoPontoEncontro;

        //CONTRUTOR COM INJEÇÂO DE INDEPENDENCIA
        public AplicacaoPontoEncontro(IPontoEncontro iPontoEncontro, IServicoPontoEncontro iServicoPontoEncontro)
        {
            _IPontoEncontro = iPontoEncontro;
            _IServicoPontoEncontro = iServicoPontoEncontro;
        }

        public async Task Adicionar(PontoEncontro Objeto)
        {
           await _IPontoEncontro.Adicionar(Objeto);
        }

        public Task AdicionarBarragem(PontoEncontro tipoMaterialBarragem)
        {
            throw new NotImplementedException();
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AdicionarPontoEncontro(PontoEncontro tipoMaterialBarragem)
        {
            await _IServicoPontoEncontro.AdicionarPontoEncontro(tipoMaterialBarragem);
        }

        public Task AtualizaBarragem(PontoEncontro tipoMaterialBarragem)
        {
            throw new NotImplementedException();
        }

        public async Task Atualizar(PontoEncontro Objeto)
        {
            await _IPontoEncontro.Atualizar(Objeto);
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AtualizaPontoEncontro(PontoEncontro tipoMaterialBarragem)
        {
            await _IServicoPontoEncontro.AtualizaPontoEncontro(tipoMaterialBarragem);
        }

        public async Task<PontoEncontro> BuscarPorId(int Id)
        {
            return await _IPontoEncontro.BuscarPorId(Id);
        }

        public async Task Excluir(PontoEncontro Objeto)
        {
            await _IPontoEncontro.Excluir(Objeto);
        }

        public async  Task<List<PontoEncontro>> Listar()
        {
            return await _IPontoEncontro.Listar();
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task<List<PontoEncontro>> ListarPontoEncontro()
        {
            return await _IServicoPontoEncontro.ListarPontoEncontro();
        }
    }
}
