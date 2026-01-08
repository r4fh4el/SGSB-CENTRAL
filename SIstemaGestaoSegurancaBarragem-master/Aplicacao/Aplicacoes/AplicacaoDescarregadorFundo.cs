using Aplicacao.Interfaces;
using Aplicacao.Interfaces.Genericos;
using Dominio.Interfaces;
using Dominio.Interfaces.InterfaceServicos;
using Dominio.Servicos;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Aplicacao.Aplicacoes
{
    public class AplicacaoDescarregadorFundo : IAplicacaoDescarregadorFundo
    {
        // INTERFACE DOMINIO
        IDescarregadorFundo _IDescarregadorFundo;

        // INTERFACE SERVICO
        IServicoDescarregadorFundo _IServicoDescarregadorFundo;

        //CONTRUTOR COM INJEÇÂO DE INDEPENDENCIA
        public AplicacaoDescarregadorFundo(IDescarregadorFundo iDescarregadorFundo, IServicoDescarregadorFundo iServicoDescarregadorFundo)
        {
            _IDescarregadorFundo = iDescarregadorFundo;
            _IServicoDescarregadorFundo = iServicoDescarregadorFundo;
        }

        public async Task Adicionar(DescarregadorFundo Objeto)
        {
           await _IDescarregadorFundo.Adicionar(Objeto);
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AdicionarDescarregadorFundo(DescarregadorFundo descarregadorFundo)
        {
            await _IServicoDescarregadorFundo.AdicionarDescarregadorFundo(descarregadorFundo);
        }

        public async Task Atualizar(DescarregadorFundo Objeto)
        {
            await _IDescarregadorFundo.Atualizar(Objeto);
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AtualizaDescarregadorFundo(DescarregadorFundo descarregadorFundo)
        {
            await _IServicoDescarregadorFundo.AtualizaDescarregadorFundo(descarregadorFundo);
        }

        public async Task<DescarregadorFundo> BuscarPorId(int Id)
        {
            return await _IDescarregadorFundo.BuscarPorId(Id);
        }

        public async Task Excluir(DescarregadorFundo Objeto)
        {
            await _IDescarregadorFundo.Excluir(Objeto);
        }

        public async  Task<List<DescarregadorFundo>> Listar()
        {
            return await _IDescarregadorFundo.Listar();
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task<List<DescarregadorFundo>> ListarDescarregadorFundo()
        {
            return await _IServicoDescarregadorFundo.ListarDescarregadorFundo();
        }

        public Task Adicionar(AplicacaoDescarregadorFundo Objeto)
        {
            throw new NotImplementedException();
        }

        public Task Atualizar(AplicacaoDescarregadorFundo Objeto)
        {
            throw new NotImplementedException();
        }

        public Task Excluir(AplicacaoDescarregadorFundo Objeto)
        {
            throw new NotImplementedException();
        }
        public async Task<List<DescarregadorFundo>> ListarDescarregadorFundoBarragemId(int idBarragem)
        {
            return await _IServicoDescarregadorFundo.ListarDescarregadorFundoBarragemId(idBarragem);
        }
    }
}
