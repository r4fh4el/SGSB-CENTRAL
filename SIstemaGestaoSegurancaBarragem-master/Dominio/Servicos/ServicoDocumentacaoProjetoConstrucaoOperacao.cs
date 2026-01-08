using Dominio.Interfaces;
using Dominio.Interfaces.InterfaceServicos;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Servicos
{
    public class ServicoDocumentacaoProjetoConstrucaoOperacao : IServicoDocumentacaoProjetoConstrucaoOperacao
    {
        private readonly IDocumentacaoProjetoConstrucaoOperacao _IDocumentacaoProjetoConstrucaoOperacao;

        public ServicoDocumentacaoProjetoConstrucaoOperacao(IDocumentacaoProjetoConstrucaoOperacao documentacaoProjetoConstrucaoOperacao) 
        {
            _IDocumentacaoProjetoConstrucaoOperacao = documentacaoProjetoConstrucaoOperacao;
        }

        public async Task AdicionarDocumentacaoProjetoConstrucaoOperacao(DocumentacaoProjetoConstrucaoOperacao documentacaoProjetoConstrucaoOperacao)
        {
          
                documentacaoProjetoConstrucaoOperacao.DataAlteracao = DateTime.Now;
                documentacaoProjetoConstrucaoOperacao.DataCadastro = DateTime.Now;
               await _IDocumentacaoProjetoConstrucaoOperacao.Adicionar(documentacaoProjetoConstrucaoOperacao);
            
        }

        public async Task AtualizaDocumentacaoProjetoConstrucaoOperacao(DocumentacaoProjetoConstrucaoOperacao documentacaoProjetoConstrucaoOperacao)
        {
            
                documentacaoProjetoConstrucaoOperacao.DataAlteracao = DateTime.Now;
                documentacaoProjetoConstrucaoOperacao.DataCadastro = documentacaoProjetoConstrucaoOperacao.DataCadastro;
                await _IDocumentacaoProjetoConstrucaoOperacao.Atualizar(documentacaoProjetoConstrucaoOperacao);
        
        }

        public async Task<List<DocumentacaoProjetoConstrucaoOperacao>> ListarDocumentacaoProjetoConstrucaoOperacao()
        {
            return  await _IDocumentacaoProjetoConstrucaoOperacao.ListarDocumentacaoProjetoConstrucaoOperacao(n => n.Pergunta != null);
        }

        public async Task<List<DocumentacaoProjetoConstrucaoOperacao>> ListarDocumentacaoProjetoConstrucaoOperacaoBarragemId(int idBarragem)
        {
            return await _IDocumentacaoProjetoConstrucaoOperacao.ListarDocumentacaoProjetoConstrucaoOperacaoBarragemId(idBarragem);
        }
    }
}
