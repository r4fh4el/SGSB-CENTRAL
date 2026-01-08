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
    public class ServicoSistemaDrenagem : IServicoSistemaDrenagem
    {
        private readonly ISistemaDrenagem _ISistemaDrenagem;

        public ServicoSistemaDrenagem(ISistemaDrenagem sistemaDrenagem) 
        {
            _ISistemaDrenagem = sistemaDrenagem;
        }

        public async Task AdicionarSistemaDrenagem(SistemaDrenagem sistemaDrenagem)
        {
            var validarNome = sistemaDrenagem.ValidadePropriedadeSring(sistemaDrenagem.Nome , "Nome");

            if (validarNome)
            {
                sistemaDrenagem.DataAlteracao = DateTime.Now;
                sistemaDrenagem.DataCadastro = DateTime.Now;
               await _ISistemaDrenagem.Adicionar(sistemaDrenagem);
            }
        }

        public async Task AtualizaSistemaDrenagem(SistemaDrenagem sistemaDrenagem)
        {
            var validarNome = sistemaDrenagem.ValidadePropriedadeSring(sistemaDrenagem.Nome, "Nome");

            if (validarNome)
            {
                sistemaDrenagem.DataAlteracao = DateTime.Now;
                sistemaDrenagem.DataCadastro = sistemaDrenagem.DataCadastro;
                await _ISistemaDrenagem.Atualizar(sistemaDrenagem);
        }
        }

        public async Task<List<SistemaDrenagem>> ListarSistemaDrenagem()
        {
            return  await _ISistemaDrenagem.ListarSistemaDrenagem(n => n.Nome != null);
        }
    }
}
