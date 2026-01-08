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
    public class AplicacaoInstrumentosBarragem : IAplicacaoInstrumentosBarragem
    {
        // INTERFACE DOMINIO
        IInstrumentosBarragem _IInstrumentosBarragem;

        // INTERFACE SERVICO
        IServicoInstrumentosBarragem _IServicoInstrumentosBarragem;

        //CONTRUTOR COM INJEÇÂO DE INDEPENDENCIA
        public AplicacaoInstrumentosBarragem(IInstrumentosBarragem iInstrumentosBarragem, IServicoInstrumentosBarragem iServicoInstrumentosBarragem)
        {
            _IInstrumentosBarragem = iInstrumentosBarragem;
            _IServicoInstrumentosBarragem = iServicoInstrumentosBarragem;
        }

        public async Task Adicionar(InstrumentosBarragem Objeto)
        {
           await _IInstrumentosBarragem.Adicionar(Objeto);
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AdicionarInstrumentosBarragem(InstrumentosBarragem InstrumentosBarragem)
        {
            await _IServicoInstrumentosBarragem.AdicionarInstrumentosBarragem(InstrumentosBarragem);
        }

        public async Task Atualizar(InstrumentosBarragem Objeto)
        {
            await _IInstrumentosBarragem.Atualizar(Objeto);
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AtualizaInstrumentosBarragem(InstrumentosBarragem InstrumentosBarragem)
        {
            await _IServicoInstrumentosBarragem.AtualizaInstrumentosBarragem(InstrumentosBarragem);
        }

        public async Task<InstrumentosBarragem> BuscarPorId(int Id)
        {
            return await _IInstrumentosBarragem.BuscarPorId(Id);
        }

        public async Task Excluir(InstrumentosBarragem Objeto)
        {
            await _IInstrumentosBarragem.Excluir(Objeto);
        }

        public async  Task<List<InstrumentosBarragem>> Listar()
        {
            return await _IInstrumentosBarragem.Listar();
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task<List<InstrumentosBarragem>> ListarInstrumentosBarragem()
        {
            return await _IServicoInstrumentosBarragem.ListarInstrumentosBarragem();
        }

        public async Task<List<InstrumentosBarragem>> ListarInstrumentosBarragemBarragemId(int idBarragem)
        {
            return await _IServicoInstrumentosBarragem.ListarInstrumentosBarragemBarragemId(idBarragem);
        }
    }

}
