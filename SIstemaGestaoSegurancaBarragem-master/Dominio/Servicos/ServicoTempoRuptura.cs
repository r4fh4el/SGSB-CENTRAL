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
    public class ServicoTempoRuptura : IServicoTempoRuptura
    {
        private readonly ITempoRuptura _ITempoRuptura;

        public ServicoTempoRuptura(ITempoRuptura TempoRuptura)
        {
            _ITempoRuptura = TempoRuptura;
        }

        public async Task AdicionarTempoRuptura(TempoRuptura TempoRuptura)
        {

            TempoRuptura.DataAlteracao = DateTime.Now;
            TempoRuptura.DataCadastro = DateTime.Now;
            await _ITempoRuptura.Adicionar(TempoRuptura);

        }

        public async Task AtualizaTempoRuptura(TempoRuptura TempoRuptura)
        {

            TempoRuptura.DataAlteracao = DateTime.Now;
            TempoRuptura.DataCadastro = TempoRuptura.DataCadastro;
            await _ITempoRuptura.Atualizar(TempoRuptura);

        }

        public async Task<List<TempoRuptura>> ListarTempoRuptura()
        {
            return await _ITempoRuptura.ListarTempoRuptura(n => n.Id != null);
        }
    }
}
