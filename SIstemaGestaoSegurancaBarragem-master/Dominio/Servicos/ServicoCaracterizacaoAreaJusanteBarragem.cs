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
    public class ServicoCaracterizacaoAreaJusanteBarragem : IServicoCaracterizacaoAreaJusanteBarragem
    {
        private readonly ICaracterizacaoAreaJusanteBarragem _ICaracterizacaoAreaJusanteBarragem;

        public ServicoCaracterizacaoAreaJusanteBarragem(ICaracterizacaoAreaJusanteBarragem caracterizacaoAreaJusanteBarragem) 
        {
            _ICaracterizacaoAreaJusanteBarragem = caracterizacaoAreaJusanteBarragem;
        }

        public async Task AdicionarCaracterizacaoAreaJusanteBarragem(CaracterizacaoAreaJusanteBarragem caracterizacaoAreaJusanteBarragem)
        {
            
                caracterizacaoAreaJusanteBarragem.DataAlteracao = DateTime.Now;
                caracterizacaoAreaJusanteBarragem.DataCadastro = DateTime.Now;
               await _ICaracterizacaoAreaJusanteBarragem.Adicionar(caracterizacaoAreaJusanteBarragem);
            }

        public async Task AtualizaCaracterizacaoAreaJusanteBarragem(CaracterizacaoAreaJusanteBarragem caracterizacaoAreaJusanteBarragem)
        {
           
                caracterizacaoAreaJusanteBarragem.DataAlteracao = DateTime.Now;
                caracterizacaoAreaJusanteBarragem.DataCadastro = caracterizacaoAreaJusanteBarragem.DataCadastro;
                await _ICaracterizacaoAreaJusanteBarragem.Atualizar(caracterizacaoAreaJusanteBarragem);
        
        }

        public async Task<List<CaracterizacaoAreaJusanteBarragem>> ListarCaracterizacaoAreaJusanteBarragem()
        {
            return  await _ICaracterizacaoAreaJusanteBarragem.ListarCaracterizacaoAreaJusanteBarragem(n => n.DistantciaKm != null);
        }

        public async Task<List<CaracterizacaoAreaJusanteBarragem>> ListarCaracterizacaoAreaJusanteBarragemBarragemId(int idBarragem)
        {
            return await _ICaracterizacaoAreaJusanteBarragem.ListarCaracterizacaoAreaJusanteBarragemBarragemId(idBarragem);
        }
    }
}
