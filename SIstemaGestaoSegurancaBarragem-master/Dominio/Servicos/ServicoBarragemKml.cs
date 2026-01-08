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
    public class ServicoBarragemKml : IServicoBarragemKml
    {
        private readonly IBarragemKml _IBarragemKml;

        public ServicoBarragemKml(IBarragemKml BarragemKml) 
        {
            _IBarragemKml = BarragemKml;
        }

        public async Task AdicionarBarragemKml(BarragemKml BarragemKml)
        {

                BarragemKml.DataAlteracao = DateTime.Now;
                BarragemKml.DataCadastro = DateTime.Now;
               await _IBarragemKml.Adicionar(BarragemKml);
   
        }

        public async Task AtualizaBarragemKml(BarragemKml BarragemKml)
        {
           
                BarragemKml.DataAlteracao = DateTime.Now;
                BarragemKml.DataCadastro = BarragemKml.DataCadastro;
                await _IBarragemKml.Atualizar(BarragemKml);
        
        }

        public async Task<List<BarragemKml>> ListarBarragemKml()
        {
            return  await _IBarragemKml.ListarBarragemKml(n => n.Coordenadas != null);
        }



     
    }
}
