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
    public class ServicoInstrumentos : IServicoInstrumentos
    {
        private readonly IInstrumentos _IInstrumentos;

        public ServicoInstrumentos(IInstrumentos instrumentos) 
        {
            _IInstrumentos = instrumentos;
        }

        public async Task AdicionarInstrumentos(Instrumentos instrumentos)
        {
            var validarNome = instrumentos.ValidadePropriedadeSring(instrumentos.Nome , "Nome");

            if (validarNome)
            {
                instrumentos.DataAlteracao = DateTime.Now;
                instrumentos.DataCadastro = DateTime.Now;
               await _IInstrumentos.Adicionar(instrumentos);
            }
        }

        public async Task AtualizaInstrumentos(Instrumentos instrumentos)
        {
            var validarNome = instrumentos.ValidadePropriedadeSring(instrumentos.Nome, "Nome");

            if (validarNome)
            {
                instrumentos.DataAlteracao = DateTime.Now;
                instrumentos.DataCadastro = instrumentos.DataCadastro;
                await _IInstrumentos.Atualizar(instrumentos);
        }
        }

        public async Task<List<Instrumentos>> ListarInstrumentos()
        {
            return  await _IInstrumentos.ListarInstrumentos(n => n.Nome != null);
        }
    }
}
