using Aplicacao.Interfaces;
using Aplicacao.Interfaces.Genericos;
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
    public class AplicacaoInspecoes : IAplicacaoInspecoes
    {
        // INTERFACE DOMINIO
        IInspecoes _IInspecoes;

        // INTERFACE SERVICO
        IServicoInspecoes _IServicoInspecoes;

        //CONTRUTOR COM INJEÇÂO DE INDEPENDENCIA
        public AplicacaoInspecoes(IInspecoes iInspecoes, IServicoInspecoes iServicoInspecoes)
        {
            _IInspecoes = iInspecoes;
            _IServicoInspecoes = iServicoInspecoes;
        }

        public async Task Adicionar(Inspecoes Objeto)
        {
           await _IInspecoes.Adicionar(Objeto);
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AdicionarInspecoes(Inspecoes inspecoes)
        {
            await _IServicoInspecoes.AdicionarInspecoes(inspecoes);
        }

        public async Task Atualizar(Inspecoes Objeto)
        {
            await _IInspecoes.Atualizar(Objeto);
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AtualizaInspecoes(Inspecoes inspecoes)
        {
            await _IServicoInspecoes.AtualizaInspecoes(inspecoes);
        }

        public async Task<Inspecoes> BuscarPorId(int Id)
        {
            return await _IInspecoes.BuscarPorId(Id);
        }

        public async Task Excluir(Inspecoes Objeto)
        {
            await _IInspecoes.Excluir(Objeto);
        }

        public async  Task<List<Inspecoes>> Listar()
        {
            return await _IInspecoes.Listar();
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task<List<Inspecoes>> ListarInspecoes()
        {
            return await _IServicoInspecoes.ListarInspecoes();
        }

        public Task Adicionar(AplicacaoInspecoes Objeto)
        {
            throw new NotImplementedException();
        }

        public Task Atualizar(AplicacaoInspecoes Objeto)
        {
            throw new NotImplementedException();
        }

        public Task Excluir(AplicacaoInspecoes Objeto)
        {
            throw new NotImplementedException();
        }


        public async Task<List<Inspecoes>> ListarInspecoesBarragemId(int idBarragem)
        {
            return await _IServicoInspecoes.ListarInspecoesBarragemId(idBarragem);
        }
    }
}
