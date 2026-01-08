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
    public class ServicoBarragem : IServicoBarragem
    {
        private readonly IBarragem _IBarragem;

        public ServicoBarragem(IBarragem barragem) 
        {
            _IBarragem = barragem;
        }

        public async Task AdicionarBarragem(Barragem barragem)
        {
            var validarNome = barragem.ValidadePropriedadeSring(barragem.Nome, "Nome");

            

            if (validarNome)
            {
                barragem.DataAlteracao = DateTime.Now;
                barragem.DataCadastro = DateTime.Now;
               await _IBarragem.Adicionar(barragem);
            }
        }

        public async Task AtualizaBarragem(Barragem barragem)
        {
            var validarNome = barragem.ValidadePropriedadeSring(barragem.Nome, "Nome");

            if (validarNome)
            {
                barragem.DataAlteracao = DateTime.Now;
                barragem.DataCadastro = barragem.DataCadastro;
                await _IBarragem.Atualizar(barragem);
        }
        }

        public async Task<List<Barragem>> BuscarListaPorIdBarragem(int id)
        {
            return await _IBarragem.BuscarListaPorIdBarragem(id);
        }

        public async Task<bool> DeletarBarragemRelacionais(int idBarragem)
        {
            return await _IBarragem.DeletarBarragemRelacionais(idBarragem);
        }

        public async Task<List<Barragem>> ListarBarragem()
        {
            return  await _IBarragem.ListarBarragem(n => n.Nome != null);
        }
    }
}
