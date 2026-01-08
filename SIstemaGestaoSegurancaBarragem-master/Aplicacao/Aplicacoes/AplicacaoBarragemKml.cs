using Aplicacao.Interfaces;
using Dominio.Interfaces;
using Dominio.Interfaces.InterfaceServicos;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Aplicacao.Aplicacoes
{
    public class AplicacaoBarragemKml : IAplicacaoBarragemKml
    {
        // INTERFACE DOMINIO
        IBarragemKml _IBarragemKml;

        // INTERFACE SERVICO
        IServicoBarragemKml _IServicoBarragemKml;

        //CONTRUTOR COM INJEÇÂO DE INDEPENDENCIA
        public AplicacaoBarragemKml(IBarragemKml iBarragemKml, IServicoBarragemKml iServicoBarragemKml)
        {
            _IBarragemKml = iBarragemKml;
            _IServicoBarragemKml = iServicoBarragemKml;
        }

        public async Task Adicionar(BarragemKml Objeto)
        {
           await _IBarragemKml.Adicionar(Objeto);
        }

        public Task AdicionarBarragem(BarragemKml tipoMaterialBarragem)
        {
            throw new NotImplementedException();
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AdicionarBarragemKml(BarragemKml tipoMaterialBarragem)
        {
            await _IServicoBarragemKml.AdicionarBarragemKml(tipoMaterialBarragem);
        }

        public Task AtualizaBarragem(BarragemKml tipoMaterialBarragem)
        {
            throw new NotImplementedException();
        }

        public async Task Atualizar(BarragemKml Objeto)
        {
            await _IBarragemKml.Atualizar(Objeto);
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AtualizaBarragemKml(BarragemKml tipoMaterialBarragem)
        {
            await _IServicoBarragemKml.AtualizaBarragemKml(tipoMaterialBarragem);
        }

        public async Task<BarragemKml> BuscarPorId(int Id)
        {
            return await _IBarragemKml.BuscarPorId(Id);
        }

        public async Task Excluir(BarragemKml Objeto)
        {
            await _IBarragemKml.Excluir(Objeto);
        }

        public async  Task<List<BarragemKml>> Listar()
        {
            return await _IBarragemKml.Listar();
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task<List<BarragemKml>> ListarBarragemKml()
        {
            return await _IServicoBarragemKml.ListarBarragemKml();
        }
    }
}
