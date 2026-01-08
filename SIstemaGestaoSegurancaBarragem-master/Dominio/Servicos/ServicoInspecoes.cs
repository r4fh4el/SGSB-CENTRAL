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
    public class ServicoInspecoes : IServicoInspecoes
    {
        private readonly IInspecoes _IInspecoes;

        public ServicoInspecoes(IInspecoes inspecoes) 
        {
            _IInspecoes = inspecoes;
        }

        public async Task AdicionarInspecoes(Inspecoes inspecoes)
        {
          
                inspecoes.DataAlteracao = DateTime.Now;
                inspecoes.DataCadastro = DateTime.Now;
               await _IInspecoes.Adicionar(inspecoes);
          
        }

        public async Task AtualizaInspecoes(Inspecoes inspecoes)
        {
           
                inspecoes.DataAlteracao = DateTime.Now;
                inspecoes.DataCadastro = inspecoes.DataCadastro;
                await _IInspecoes.Atualizar(inspecoes);
       
        }

        public async Task<List<Inspecoes>> ListarInspecoes()
        {
            return  await _IInspecoes.ListarInspecoes(n => n.Nome_Setor != null);
        }

        public async Task<List<Inspecoes>> ListarInspecoesBarragemId(int idBarragem)
        {
            return await _IInspecoes.ListarInspecoesBarragemId(idBarragem);
        }
    }
}
