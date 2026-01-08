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
    public class ServicoInstrumentosBarragem : IServicoInstrumentosBarragem
    {
        private readonly IInstrumentosBarragem _IInstrumentosBarragem;

        public ServicoInstrumentosBarragem(IInstrumentosBarragem InstrumentosBarragem) 
        {
            _IInstrumentosBarragem = InstrumentosBarragem;
        }

        public async Task AdicionarInstrumentosBarragem(InstrumentosBarragem InstrumentosBarragem)
        {
            var validarNome = InstrumentosBarragem.ValidadePropriedadeSring(InstrumentosBarragem.NomePropriedade , "Nome");

            if (validarNome)
            {
                InstrumentosBarragem.DataAlteracao = DateTime.Now;
                InstrumentosBarragem.DataCadastro = DateTime.Now;
               await _IInstrumentosBarragem.Adicionar(InstrumentosBarragem);
            }
        }

        public async Task AtualizaInstrumentosBarragem(InstrumentosBarragem InstrumentosBarragem)
        {
            var validarNome = InstrumentosBarragem.ValidadePropriedadeSring(InstrumentosBarragem.NomePropriedade, "Nome");

            if (validarNome)
            {
                InstrumentosBarragem.DataAlteracao = DateTime.Now;
                InstrumentosBarragem.DataCadastro = InstrumentosBarragem.DataCadastro;
                await _IInstrumentosBarragem.Atualizar(InstrumentosBarragem);
        }
        }

        public async Task<List<InstrumentosBarragem>> ListarInstrumentosBarragem()
        {
            return  await _IInstrumentosBarragem.ListarInstrumentosBarragem(n => n.InstrumentosId != null);
        }

        public async Task<List<InstrumentosBarragem>> ListarInstrumentosBarragemBarragemId(int idBarragem)
        {
            return await _IInstrumentosBarragem.ListarInstrumentosBarragemBarragemId(idBarragem);
        }
    }
}
