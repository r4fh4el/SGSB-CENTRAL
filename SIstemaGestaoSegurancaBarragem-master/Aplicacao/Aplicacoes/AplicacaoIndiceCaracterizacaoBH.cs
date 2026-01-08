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
    public class AplicacaoIndiceCaracterizacaoBH : IAplicacaoIndiceCaracterizacaoBH
    {
        // INTERFACE DOMINIO
        IIndiceCaracterizacaoBH _IIndiceCaracterizacaoBH;

        // INTERFACE SERVICO
        IServicoIndiceCaracterizacaoBH _IServicoIndiceCaracterizacaoBH;

        //CONTRUTOR COM INJEÇÂO DE INDEPENDENCIA
        public AplicacaoIndiceCaracterizacaoBH(IIndiceCaracterizacaoBH iIndiceCaracterizacaoBH, IServicoIndiceCaracterizacaoBH iServicoIndiceCaracterizacaoBH)
        {
            _IIndiceCaracterizacaoBH = iIndiceCaracterizacaoBH;
            _IServicoIndiceCaracterizacaoBH = iServicoIndiceCaracterizacaoBH;
        }

        public async Task Adicionar(IndiceCaracterizacaoBH Objeto)
        {
           await _IIndiceCaracterizacaoBH.Adicionar(Objeto);
        }

        public Task AdicionarBarragem(IndiceCaracterizacaoBH indiceCaracterizacaoBH)
        {
            throw new NotImplementedException();
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AdicionarIndiceCaracterizacaoBH(IndiceCaracterizacaoBH indiceCaracterizacaoBH)
        {
            await _IServicoIndiceCaracterizacaoBH.AdicionarIndiceCaracterizacaoBH(indiceCaracterizacaoBH);
        }

        public Task AtualizaBarragem(IndiceCaracterizacaoBH indiceCaracterizacaoBH)
        {
            throw new NotImplementedException();
        }

        public async Task Atualizar(IndiceCaracterizacaoBH Objeto)
        {
            await _IIndiceCaracterizacaoBH.Atualizar(Objeto);
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AtualizaIndiceCaracterizacaoBH(IndiceCaracterizacaoBH indiceCaracterizacaoBH)
        {
            await _IServicoIndiceCaracterizacaoBH.AtualizaIndiceCaracterizacaoBH(indiceCaracterizacaoBH);
        }

        public async Task<IndiceCaracterizacaoBH> BuscarPorId(int Id)
        {
            return await _IIndiceCaracterizacaoBH.BuscarPorId(Id);
        }

        public async Task Excluir(IndiceCaracterizacaoBH Objeto)
        {
            await _IIndiceCaracterizacaoBH.Excluir(Objeto);
        }

        public async  Task<List<IndiceCaracterizacaoBH>> Listar()
        {
            return await _IIndiceCaracterizacaoBH.Listar();
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task<List<IndiceCaracterizacaoBH>> ListarIndiceCaracterizacaoBH()
        {
            return await _IServicoIndiceCaracterizacaoBH.ListarIndiceCaracterizacaoBH();
        }
    }
}
