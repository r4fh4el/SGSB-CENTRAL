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
    public class AplicacaoUsoBarragemBarragem : IAplicacaoUsoBarragemBarragem
    {
        // INTERFACE DOMINIO
        IUsoBarragemBarragem _IUsoBarragemBarragem;

        // INTERFACE SERVICO
        IServicoUsoBarragemBarragem _IServicoUsoBarragemBarragem;

        //CONTRUTOR COM INJEÇÂO DE INDEPENDENCIA
        public AplicacaoUsoBarragemBarragem(IUsoBarragemBarragem iUsoBarragemBarragem, IServicoUsoBarragemBarragem iServicoUsoBarragemBarragem)
        {
            _IUsoBarragemBarragem = iUsoBarragemBarragem;
            _IServicoUsoBarragemBarragem = iServicoUsoBarragemBarragem;
        }

        public async Task Adicionar(UsoBarragemBarragem Objeto)
        {
           await _IUsoBarragemBarragem.Adicionar(Objeto);
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AdicionarUsoBarragemBarragem(UsoBarragemBarragem UsoBarragemBarragem)
        {
            await _IServicoUsoBarragemBarragem.AdicionarUsoBarragemBarragem(UsoBarragemBarragem);
        }

        public async Task Atualizar(UsoBarragemBarragem Objeto)
        {
            await _IUsoBarragemBarragem.Atualizar(Objeto);
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AtualizaUsoBarragemBarragem(UsoBarragemBarragem UsoBarragemBarragem)
        {
            await _IServicoUsoBarragemBarragem.AtualizaUsoBarragemBarragem(UsoBarragemBarragem);
        }

        public async Task<UsoBarragemBarragem> BuscarPorId(int Id)
        {
            return await _IUsoBarragemBarragem.BuscarPorId(Id);
        }

        public async Task Excluir(UsoBarragemBarragem Objeto)
        {
            await _IUsoBarragemBarragem.Excluir(Objeto);
        }

        public async  Task<List<UsoBarragemBarragem>> Listar()
        {
            return await _IUsoBarragemBarragem.Listar();
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task<List<UsoBarragemBarragem>> ListarUsoBarragemBarragem()
        {
            return await _IServicoUsoBarragemBarragem.ListarUsoBarragemBarragem();
        }

        public Task Adicionar(AplicacaoUsoBarragemBarragem Objeto)
        {
            throw new NotImplementedException();
        }

        public Task Atualizar(AplicacaoUsoBarragemBarragem Objeto)
        {
            throw new NotImplementedException();
        }

        public Task Excluir(AplicacaoUsoBarragemBarragem Objeto)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UsoBarragemBarragem>> ListarUsoBarragemBarragemBarragemId(int idBarragem)
        {
            return await _IServicoUsoBarragemBarragem.ListarUsoBarragemBarragemBarragemId(idBarragem);
        }
    }
}
