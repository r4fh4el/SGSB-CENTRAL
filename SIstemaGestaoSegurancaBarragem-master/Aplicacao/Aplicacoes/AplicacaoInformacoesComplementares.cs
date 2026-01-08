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
    public class AplicacaoInformacoesComplementares : IAplicacaoInformacoesComplementares
    {
        // INTERFACE DOMINIO
        IInformacoesComplementares _IInformacoesComplementares;

        // INTERFACE SERVICO
        IServicoInformacoesComplementares _IServicoInformacoesComplementares;

        //CONTRUTOR COM INJEÇÂO DE INDEPENDENCIA
        public AplicacaoInformacoesComplementares(IInformacoesComplementares iInformacoesComplementares, IServicoInformacoesComplementares iServicoInformacoesComplementares)
        {
            _IInformacoesComplementares = iInformacoesComplementares;
            _IServicoInformacoesComplementares = iServicoInformacoesComplementares;
        }

        public async Task Adicionar(InformacoesComplementares Objeto)
        {
           await _IInformacoesComplementares.Adicionar(Objeto);
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AdicionarInformacoesComplementares(InformacoesComplementares informacoesComplementares)
        {
            await _IServicoInformacoesComplementares.AdicionarInformacoesComplementares(informacoesComplementares);
        }

        public async Task Atualizar(InformacoesComplementares Objeto)
        {
            await _IInformacoesComplementares.Atualizar(Objeto);
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AtualizaInformacoesComplementares(InformacoesComplementares informacoesComplementares)
        {
            await _IServicoInformacoesComplementares.AtualizaInformacoesComplementares(informacoesComplementares);
        }

        public async Task<InformacoesComplementares> BuscarPorId(int Id)
        {
            return await _IInformacoesComplementares.BuscarPorId(Id);
        }

        public async Task Excluir(InformacoesComplementares Objeto)
        {
            await _IInformacoesComplementares.Excluir(Objeto);
        }

        public async  Task<List<InformacoesComplementares>> Listar()
        {
            return await _IInformacoesComplementares.Listar();
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task<List<InformacoesComplementares>> ListarInformacoesComplementares()
        {
            return await _IServicoInformacoesComplementares.ListarInformacoesComplementares();
        }

        public Task Adicionar(AplicacaoInformacoesComplementares Objeto)
        {
            throw new NotImplementedException();
        }

        public Task Atualizar(AplicacaoInformacoesComplementares Objeto)
        {
            throw new NotImplementedException();
        }

        public Task Excluir(AplicacaoInformacoesComplementares Objeto)
        {
            throw new NotImplementedException();
        }

        public async Task<List<InformacoesComplementares>> ListarInformacoesComplementaresBarragemId(int idBarragem)
        {
            return await _IServicoInformacoesComplementares.ListarInformacoesComplementaresBarragemId(idBarragem);
        }

    }
}
