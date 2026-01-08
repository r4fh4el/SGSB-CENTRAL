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
    public class ServicoUsoBarragemBarragem : IServicoUsoBarragemBarragem
    {
        private readonly IUsoBarragemBarragem _IUsoBarragemBarragem;

        public ServicoUsoBarragemBarragem(IUsoBarragemBarragem UsoBarragemBarragem) 
        {
            _IUsoBarragemBarragem = UsoBarragemBarragem;
        }

        public async Task AdicionarUsoBarragemBarragem(UsoBarragemBarragem UsoBarragemBarragem)
        {
                UsoBarragemBarragem.DataAlteracao = DateTime.Now;
                UsoBarragemBarragem.DataCadastro = DateTime.Now;
               await _IUsoBarragemBarragem.Adicionar(UsoBarragemBarragem);
           
        }

        public async Task AtualizaUsoBarragemBarragem(UsoBarragemBarragem UsoBarragemBarragem)
        {
            
                UsoBarragemBarragem.DataAlteracao = DateTime.Now;
                UsoBarragemBarragem.DataCadastro = UsoBarragemBarragem.DataCadastro;
                await _IUsoBarragemBarragem.Atualizar(UsoBarragemBarragem);
        
        }

        public async Task<List<UsoBarragemBarragem>> ListarUsoBarragemBarragem()
        {
            return  await _IUsoBarragemBarragem.ListarUsoBarragemBarragem(n => n.UsoBarragemId != null);
        }

        public async Task<List<UsoBarragemBarragem>> ListarUsoBarragemBarragemBarragemId(int idBarragem)
        {
            return await _IUsoBarragemBarragem.ListarUsoBarragemBarragemBarragemId(idBarragem);
        }
    }
}
