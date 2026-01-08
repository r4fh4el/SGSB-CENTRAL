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
    public class ServicoReservatorio : IServicoReservatorio
    {
        private readonly IReservatorio _IReservatorio;

        public ServicoReservatorio(IReservatorio reservatorio) 
        {
            _IReservatorio = reservatorio;
        }

        public async Task AdicionarReservatorio(Reservatorio reservatorio)
        {
         
                reservatorio.DataAlteracao = DateTime.Now;
                reservatorio.DataCadastro = DateTime.Now;
               await _IReservatorio.Adicionar(reservatorio);
          
        }

        public async Task AtualizaReservatorio(Reservatorio reservatorio)
        {
           
                reservatorio.DataAlteracao = DateTime.Now;
                reservatorio.DataCadastro = reservatorio.DataCadastro;
                await _IReservatorio.Atualizar(reservatorio);
        
        }

        public async Task<List<Reservatorio>> ListarReservatorio()
        {
            return  await _IReservatorio.ListarReservatorio(n => n.BordaLivre != null);
        }

        public async Task<List<Reservatorio>> ListarReservatorioBarragemId(int idBarragem)
        {
            return await _IReservatorio.ListarReservatorioBarragemId(idBarragem);
        }
    }
}
