using Aplicacao.Interfaces;
using Dominio.Interfaces;
using Dominio.Interfaces.InterfaceServicos;
using Dominio.Servicos;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Aplicacao.Aplicacoes
{
    public class AplicacaoVertedouro : IAplicacaoVertedouro
    {
        // INTERFACE DOMINIO
        IVertedouro _IVertedouro;

        // INTERFACE SERVICO
        IServicoVertedouro _IServicoVertedouro;

        //CONTRUTOR COM INJEÇÂO DE INDEPENDENCIA
        public AplicacaoVertedouro(IVertedouro iVertedouro, IServicoVertedouro iServicoVertedouro)
        {
            _IVertedouro = iVertedouro;
            _IServicoVertedouro = iServicoVertedouro;
        }

        public async Task Adicionar(Vertedouro Objeto)
        {
           await _IVertedouro.Adicionar(Objeto);
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AdicionarVertedouro(Vertedouro tipoMaterialBarragem)
        {
            await _IServicoVertedouro.AdicionarVertedouro(tipoMaterialBarragem);
        }

        public async Task Atualizar(Vertedouro Objeto)
        {
            await _IVertedouro.Atualizar(Objeto);
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AtualizaVertedouro(Vertedouro tipoMaterialBarragem)
        {
            await _IServicoVertedouro.AtualizaVertedouro(tipoMaterialBarragem);
        }

        public async Task<Vertedouro> BuscarPorId(int Id)
        {
            return await _IVertedouro.BuscarPorId(Id);
        }

        public async Task Excluir(Vertedouro Objeto)
        {
            await _IVertedouro.Excluir(Objeto);
        }

        public async  Task<List<Vertedouro>> Listar()
        {
            return await _IVertedouro.Listar();
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task<List<Vertedouro>> ListarVertedouro()
        {
            return await _IServicoVertedouro.ListarVertedouro();
        }

        public async Task<List<Vertedouro>> ListarVertedouroBarragemId(int idBarragem)
        {
            return await _IServicoVertedouro.ListarVertedouroBarragemId(idBarragem);
        }
    }
}
