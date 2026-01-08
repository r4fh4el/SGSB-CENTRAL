using Aplicacao.Interfaces;
using Aplicacao.Interfaces.Genericos;
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
    public class AplicacaoUsoBarragem : IAplicacaoUsoBarragem
    {
        // INTERFACE DOMINIO
        IUsoBarragem _IUsoBarragem;

        // INTERFACE SERVICO
        IServicoUsoBarragem _IServicoUsoBarragem;

        //CONTRUTOR COM INJEÇÂO DE INDEPENDENCIA
        public AplicacaoUsoBarragem(IUsoBarragem iUsoBarragem, IServicoUsoBarragem iServicoUsoBarragem)
        {
            _IUsoBarragem = iUsoBarragem;
            _IServicoUsoBarragem = iServicoUsoBarragem;
        }

        public async Task Adicionar(UsoBarragem Objeto)
        {
           await _IUsoBarragem.Adicionar(Objeto);
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AdicionarUsoBarragem(UsoBarragem usoBarragem)
        {
            await _IServicoUsoBarragem.AdicionarUsoBarragem(usoBarragem);
        }

        public async Task Atualizar(UsoBarragem Objeto)
        {
            await _IUsoBarragem.Atualizar(Objeto);
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AtualizaUsoBarragem(UsoBarragem usoBarragem)
        {
            await _IServicoUsoBarragem.AtualizaUsoBarragem(usoBarragem);
        }

        public async Task<UsoBarragem> BuscarPorId(int Id)
        {
            return await _IUsoBarragem.BuscarPorId(Id);
        }

        public async Task Excluir(UsoBarragem Objeto)
        {
            await _IUsoBarragem.Excluir(Objeto);
        }

        public async  Task<List<UsoBarragem>> Listar()
        {
            return await _IUsoBarragem.Listar();
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task<List<UsoBarragem>> ListarUsoBarragem()
        {
            return await _IServicoUsoBarragem.ListarUsoBarragem();
        }

        public Task Adicionar(AplicacaoUsoBarragem Objeto)
        {
            throw new NotImplementedException();
        }

        public Task Atualizar(AplicacaoUsoBarragem Objeto)
        {
            throw new NotImplementedException();
        }

        public Task Excluir(AplicacaoUsoBarragem Objeto)
        {
            throw new NotImplementedException();
        }

     
    }
}
