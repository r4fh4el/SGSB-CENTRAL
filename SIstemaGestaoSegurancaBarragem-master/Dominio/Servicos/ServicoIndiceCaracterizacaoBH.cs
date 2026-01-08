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
    public class ServicoIndiceCaracterizacaoBH : IServicoIndiceCaracterizacaoBH
    {
        private readonly IIndiceCaracterizacaoBH _IIndiceCaracterizacaoBH;

        public ServicoIndiceCaracterizacaoBH(IIndiceCaracterizacaoBH indiceCaracterizacaoBH) 
        {
            _IIndiceCaracterizacaoBH = indiceCaracterizacaoBH;
        }

        public async Task AdicionarIndiceCaracterizacaoBH(IndiceCaracterizacaoBH indiceCaracterizacaoBH)
        {
                indiceCaracterizacaoBH.DataAlteracao = DateTime.Now;
                indiceCaracterizacaoBH.DataCadastro = DateTime.Now;
               await _IIndiceCaracterizacaoBH.Adicionar(indiceCaracterizacaoBH);
           
        }

        public async Task AtualizaIndiceCaracterizacaoBH(IndiceCaracterizacaoBH indiceCaracterizacaoBH)
        {

                indiceCaracterizacaoBH.DataAlteracao = DateTime.Now;
                indiceCaracterizacaoBH.DataCadastro = indiceCaracterizacaoBH.DataCadastro;
                await _IIndiceCaracterizacaoBH.Atualizar(indiceCaracterizacaoBH);
      
        }

        public async Task<List<IndiceCaracterizacaoBH>> ListarIndiceCaracterizacaoBH()
        {
            return  await _IIndiceCaracterizacaoBH.Listar();
        }
    }
}
