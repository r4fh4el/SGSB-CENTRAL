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
    public class ServicoVazaPico : IServicoVazaPico
    {
        private readonly IVazaPico _IVazaPico;

        public ServicoVazaPico(IVazaPico vazaPico)
        {
            _IVazaPico = vazaPico;
        }

        public async Task AdicionarVazaPico(VazaPico vazaPico)
        {

            vazaPico.DataAlteracao = DateTime.Now;
            vazaPico.DataCadastro = DateTime.Now;
            await _IVazaPico.Adicionar(vazaPico);

        }

        public async Task AtualizaVazaPico(VazaPico vazaPico)
        {

            vazaPico.DataAlteracao = DateTime.Now;
            vazaPico.DataCadastro = vazaPico.DataCadastro;
            await _IVazaPico.Atualizar(vazaPico);

        }

        public async Task<List<VazaPico>> ListarVazaPico()
        {
            return await _IVazaPico.ListarVazaPico(n => n.Id != null);
        }


        public async Task<VazaPico> VerificarBarragemId(int BarragemId)
        {
            return await _IVazaPico.VerificarBarragemId(BarragemId);
        }
    }
}
