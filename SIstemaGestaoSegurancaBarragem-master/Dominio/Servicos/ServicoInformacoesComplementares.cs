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
    public class ServicoInformacoesComplementares : IServicoInformacoesComplementares
    {
        private readonly IInformacoesComplementares _IInformacoesComplementares;

        public ServicoInformacoesComplementares(IInformacoesComplementares informacoesComplementares) 
        {
            _IInformacoesComplementares = informacoesComplementares;
        }

        public async Task AdicionarInformacoesComplementares(InformacoesComplementares informacoesComplementares)
        {
           
                informacoesComplementares.DataAlteracao = DateTime.Now;
                informacoesComplementares.DataCadastro = DateTime.Now;
               await _IInformacoesComplementares.Adicionar(informacoesComplementares);
            
        }

        public async Task AtualizaInformacoesComplementares(InformacoesComplementares informacoesComplementares)
        {
          
                informacoesComplementares.DataAlteracao = DateTime.Now;
                informacoesComplementares.DataCadastro = informacoesComplementares.DataCadastro;
                await _IInformacoesComplementares.Atualizar(informacoesComplementares);
        
        }

        public async Task<List<InformacoesComplementares>> ListarInformacoesComplementares()
        {
            return  await _IInformacoesComplementares.ListarInformacoesComplementares(n => n.NomeSetor != null);
        }

        public async Task<List<InformacoesComplementares>> ListarInformacoesComplementaresBarragemId(int idBarragem)
        {
            return await _IInformacoesComplementares.ListarInformacoesComplementaresBarragemId(idBarragem);
        }
    }
}
