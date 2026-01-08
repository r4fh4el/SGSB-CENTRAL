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
    public class ServicoCategoriaRisco : IServicoCategoriaRisco
    {
        private readonly ICategoriaRisco _ICategoriaRisco;

        public ServicoCategoriaRisco(ICategoriaRisco tipoMaterialBarragem) 
        {
            _ICategoriaRisco = tipoMaterialBarragem;
        }

        public async Task AdicionarCategoriaRisco(CategoriaRisco tipoMaterialBarragem)
        {
            
                tipoMaterialBarragem.DataAlteracao = DateTime.Now;
                tipoMaterialBarragem.DataCadastro = DateTime.Now;
               await _ICategoriaRisco.Adicionar(tipoMaterialBarragem);
           
        }

        public async Task AtualizaCategoriaRisco(CategoriaRisco tipoMaterialBarragem)
        {
          
                tipoMaterialBarragem.DataAlteracao = DateTime.Now;
                tipoMaterialBarragem.DataCadastro = tipoMaterialBarragem.DataCadastro;
                await _ICategoriaRisco.Atualizar(tipoMaterialBarragem);
        
        }

        public async Task<List<CategoriaRisco>> ListarCategoriaRisco()
        {
            return  await _ICategoriaRisco.ListarCategoriaRisco(n => n.ValorTotal != null);
        }
    }
}
