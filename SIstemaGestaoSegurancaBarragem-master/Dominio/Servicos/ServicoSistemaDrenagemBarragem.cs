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
    public class ServicoSistemaDrenagemBarragem : IServicoSistemaDrenagemBarragem
    {
        private readonly ISistemaDrenagemBarragem _ISistemaDrenagemBarragem;

        public ServicoSistemaDrenagemBarragem(ISistemaDrenagemBarragem SistemaDrenagemBarragem) 
        {
            _ISistemaDrenagemBarragem = SistemaDrenagemBarragem;
        }

        public async Task AdicionarSistemaDrenagemBarragem(SistemaDrenagemBarragem SistemaDrenagemBarragem)
        {
            SistemaDrenagemBarragem.DataAlteracao = DateTime.Now;
                SistemaDrenagemBarragem.DataCadastro = DateTime.Now;
               await _ISistemaDrenagemBarragem.Adicionar(SistemaDrenagemBarragem);
           
        }

        public async Task AtualizaSistemaDrenagemBarragem(SistemaDrenagemBarragem SistemaDrenagemBarragem)
        {
   
                SistemaDrenagemBarragem.DataAlteracao = DateTime.Now;
                SistemaDrenagemBarragem.DataCadastro = SistemaDrenagemBarragem.DataCadastro;
                await _ISistemaDrenagemBarragem.Atualizar(SistemaDrenagemBarragem);
        
        }

        public async Task<List<SistemaDrenagemBarragem>> ListarSistemaDrenagemBarragem()
        {
            return  await _ISistemaDrenagemBarragem.ListarSistemaDrenagemBarragem(n => n.NomePropriedade != null);
        }


        public async Task<List<SistemaDrenagemBarragem>> ListarSistemaDrenagemBarragemBarragemId(int idBarragem)
        {
            return await _ISistemaDrenagemBarragem.ListarSistemaDrenagemBarragemBarragemId(idBarragem);
        }
    }
}
