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
    public class ServicoHidrogramaParabolico : IServicoHidrogramaParabolico
    {
        private readonly IHidrogramaParabolico _IHidrogramaParabolico;

        public ServicoHidrogramaParabolico(IHidrogramaParabolico HidrogramaParabolico)
        {
            _IHidrogramaParabolico = HidrogramaParabolico;
        }

        public async Task AdicionarHidrogramaParabolico(HidrogramaParabolico HidrogramaParabolico)
        {

            HidrogramaParabolico.DataAlteracao = DateTime.Now;
            HidrogramaParabolico.DataCadastro = DateTime.Now;
            await _IHidrogramaParabolico.Adicionar(HidrogramaParabolico);

        }

        public async Task AtualizaHidrogramaParabolico(HidrogramaParabolico HidrogramaParabolico)
        {

            HidrogramaParabolico.DataAlteracao = DateTime.Now;
            HidrogramaParabolico.DataCadastro = HidrogramaParabolico.DataCadastro;
            await _IHidrogramaParabolico.Atualizar(HidrogramaParabolico);

        }

        public async Task<List<HidrogramaParabolico>> ListarHidrogramaParabolico()
        {
            return await _IHidrogramaParabolico.ListarHidrogramaParabolico(n => n.Id != null);
        }
    }
}
