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
    public class AplicacaoDocumentacaoPerguntas : IAplicacaoDocumentacaoPerguntas
    {
        // INTERFACE DOMINIO
        IDocumentacaoPerguntas _IDocumentacaoPerguntas;

        // INTERFACE SERVICO
        IServicoDocumentacaoPerguntas _IServicoDocumentacaoPerguntas;

        //CONTRUTOR COM INJEÇÂO DE INDEPENDENCIA
        public AplicacaoDocumentacaoPerguntas(IDocumentacaoPerguntas iDocumentacaoPerguntas, IServicoDocumentacaoPerguntas iServicoDocumentacaoPerguntas)
        {
            _IDocumentacaoPerguntas = iDocumentacaoPerguntas;
            _IServicoDocumentacaoPerguntas = iServicoDocumentacaoPerguntas;
        }

        public async Task Adicionar(DocumentacaoPerguntas Objeto)
        {
           await _IDocumentacaoPerguntas.Adicionar(Objeto);
        }

        public Task AdicionarBarragem(DocumentacaoPerguntas documentacaoPerguntas)
        {
            throw new NotImplementedException();
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AdicionarDocumentacaoPerguntas(DocumentacaoPerguntas documentacaoPerguntas)
        {
            await _IServicoDocumentacaoPerguntas.AdicionarDocumentacaoPerguntas(documentacaoPerguntas);
        }

        public Task AtualizaBarragem(DocumentacaoPerguntas documentacaoPerguntas)
        {
            throw new NotImplementedException();
        }

        public async Task Atualizar(DocumentacaoPerguntas Objeto)
        {
            await _IDocumentacaoPerguntas.Atualizar(Objeto);
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AtualizaDocumentacaoPerguntas(DocumentacaoPerguntas documentacaoPerguntas)
        {
            await _IServicoDocumentacaoPerguntas.AtualizaDocumentacaoPerguntas(documentacaoPerguntas);
        }

        public async Task<DocumentacaoPerguntas> BuscarPorId(int Id)
        {
            return await _IDocumentacaoPerguntas.BuscarPorId(Id);
        }

        public async Task Excluir(DocumentacaoPerguntas Objeto)
        {
            await _IDocumentacaoPerguntas.Excluir(Objeto);
        }

        public async  Task<List<DocumentacaoPerguntas>> Listar()
        {
            return await _IDocumentacaoPerguntas.Listar();
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task<List<DocumentacaoPerguntas>> ListarDocumentacaoPerguntas()
        {
            return await _IServicoDocumentacaoPerguntas.ListarDocumentacaoPerguntas();
        }
    }
}
