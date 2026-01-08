using Aplicacao.Interfaces;
using Aplicacao.Interfaces.Genericos;
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
    public class AplicacaoInstrumentos : IAplicacaoInstrumentos
    {
        // INTERFACE DOMINIO
        IInstrumentos _IInstrumentos;

        // INTERFACE SERVICO
        IServicoInstrumentos _IServicoInstrumentos;

        //CONTRUTOR COM INJEÇÂO DE INDEPENDENCIA
        public AplicacaoInstrumentos(IInstrumentos iInstrumentos, IServicoInstrumentos iServicoInstrumentos)
        {
            _IInstrumentos = iInstrumentos;
            _IServicoInstrumentos = iServicoInstrumentos;
        }

        public async Task Adicionar(Instrumentos Objeto)
        {
           await _IInstrumentos.Adicionar(Objeto);
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AdicionarInstrumentos(Instrumentos instrumentos)
        {
            await _IServicoInstrumentos.AdicionarInstrumentos(instrumentos);
        }

        public async Task Atualizar(Instrumentos Objeto)
        {
            await _IInstrumentos.Atualizar(Objeto);
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AtualizaInstrumentos(Instrumentos instrumentos)
        {
            await _IServicoInstrumentos.AtualizaInstrumentos(instrumentos);
        }

        public async Task<Instrumentos> BuscarPorId(int Id)
        {
            return await _IInstrumentos.BuscarPorId(Id);
        }

        public async Task Excluir(Instrumentos Objeto)
        {
            await _IInstrumentos.Excluir(Objeto);
        }

        public async  Task<List<Instrumentos>> Listar()
        {
            return await _IInstrumentos.Listar();
        }
        // CUSTOMIZAVEL RETORNA DO SERVIC O
        public async Task<List<Instrumentos>> ListarInstrumentos()
        {
            return await _IServicoInstrumentos.ListarInstrumentos();
        }

      
    
    }
}
