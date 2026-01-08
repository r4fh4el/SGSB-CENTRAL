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
    public class AplicacaoDocumentacaoProjetoConstrucaoOperacao : IAplicacaoDocumentacaoProjetoConstrucaoOperacao
    {
        // INTERFACE DOMINIO
        IDocumentacaoProjetoConstrucaoOperacao _IDocumentacaoProjetoConstrucaoOperacao;

        // INTERFACE SERVICO
        IServicoDocumentacaoProjetoConstrucaoOperacao _IServicoDocumentacaoProjetoConstrucaoOperacao;

        //CONTRUTOR COM INJEÇÂO DE INDEPENDENCIA
        public AplicacaoDocumentacaoProjetoConstrucaoOperacao(IDocumentacaoProjetoConstrucaoOperacao iDocumentacaoProjetoConstrucaoOperacao, IServicoDocumentacaoProjetoConstrucaoOperacao iServicoDocumentacaoProjetoConstrucaoOperacao)
        {
            _IDocumentacaoProjetoConstrucaoOperacao = iDocumentacaoProjetoConstrucaoOperacao;
            _IServicoDocumentacaoProjetoConstrucaoOperacao = iServicoDocumentacaoProjetoConstrucaoOperacao;
        }

        public async Task Adicionar(DocumentacaoProjetoConstrucaoOperacao Objeto)
        {
           await _IDocumentacaoProjetoConstrucaoOperacao.Adicionar(Objeto);
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AdicionarDocumentacaoProjetoConstrucaoOperacao(DocumentacaoProjetoConstrucaoOperacao instrumentos)
        {
            await _IServicoDocumentacaoProjetoConstrucaoOperacao.AdicionarDocumentacaoProjetoConstrucaoOperacao(instrumentos);
        }

        public async Task Atualizar(DocumentacaoProjetoConstrucaoOperacao Objeto)
        {
            await _IDocumentacaoProjetoConstrucaoOperacao.Atualizar(Objeto);
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AtualizaDocumentacaoProjetoConstrucaoOperacao(DocumentacaoProjetoConstrucaoOperacao instrumentos)
        {
            await _IServicoDocumentacaoProjetoConstrucaoOperacao.AtualizaDocumentacaoProjetoConstrucaoOperacao(instrumentos);
        }

        public async Task<DocumentacaoProjetoConstrucaoOperacao> BuscarPorId(int Id)
        {
            return await _IDocumentacaoProjetoConstrucaoOperacao.BuscarPorId(Id);
        }

        public async Task Excluir(DocumentacaoProjetoConstrucaoOperacao Objeto)
        {
            await _IDocumentacaoProjetoConstrucaoOperacao.Excluir(Objeto);
        }

        public async  Task<List<DocumentacaoProjetoConstrucaoOperacao>> Listar()
        {
            return await _IDocumentacaoProjetoConstrucaoOperacao.Listar();
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task<List<DocumentacaoProjetoConstrucaoOperacao>> ListarDocumentacaoProjetoConstrucaoOperacao()
        {
            return await _IServicoDocumentacaoProjetoConstrucaoOperacao.ListarDocumentacaoProjetoConstrucaoOperacao();

        }

        public async Task<List<DocumentacaoProjetoConstrucaoOperacao>> ListarDocumentacaoProjetoConstrucaoOperacaoBarragemId(int idBarragem)
        {
            return await _IServicoDocumentacaoProjetoConstrucaoOperacao.ListarDocumentacaoProjetoConstrucaoOperacaoBarragemId(idBarragem);
        }

    }
}
