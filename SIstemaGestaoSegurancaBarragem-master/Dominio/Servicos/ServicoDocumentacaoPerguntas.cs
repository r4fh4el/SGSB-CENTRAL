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
    public class ServicoDocumentacaoPerguntas : IServicoDocumentacaoPerguntas
    {
        private readonly IDocumentacaoPerguntas _IDocumentacaoPerguntas;

        public ServicoDocumentacaoPerguntas(IDocumentacaoPerguntas documentacaoPerguntas) 
        {
            _IDocumentacaoPerguntas = documentacaoPerguntas;
        }

        public async Task AdicionarDocumentacaoPerguntas(DocumentacaoPerguntas documentacaoPerguntas)
        {
            var validarNome = documentacaoPerguntas.ValidadePropriedadeSring(documentacaoPerguntas.Pergunta, "Pergunta");

            if (validarNome)
            {
                documentacaoPerguntas.DataAlteracao = DateTime.Now;
                documentacaoPerguntas.DataCadastro = DateTime.Now;
               await _IDocumentacaoPerguntas.Adicionar(documentacaoPerguntas);
            }
        }

        public async Task AtualizaDocumentacaoPerguntas(DocumentacaoPerguntas documentacaoPerguntas)
        {
            var validarNome = documentacaoPerguntas.ValidadePropriedadeSring(documentacaoPerguntas.Pergunta, "Pergunta");

            if (validarNome)
            {
                documentacaoPerguntas.DataAlteracao = DateTime.Now;
                documentacaoPerguntas.DataCadastro = documentacaoPerguntas.DataCadastro;
                await _IDocumentacaoPerguntas.Atualizar(documentacaoPerguntas);
        }
        }

        public async Task<List<DocumentacaoPerguntas>> ListarDocumentacaoPerguntas()
        {
            return  await _IDocumentacaoPerguntas.ListarDocumentacaoPerguntas(n => n.Pergunta != null);
        }
    }
}
