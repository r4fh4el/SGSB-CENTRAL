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
    public class AplicacaoUsoSoloPredominante : IAplicacaoUsoSoloPredominante
    {
        // INTERFACE DOMINIO
        IUsoSoloPredominante _IUsoSoloPredominante;

        // INTERFACE SERVICO
        IServicoUsoSoloPredominante _IServicoUsoSoloPredominante;

        //CONTRUTOR COM INJEÇÂO DE INDEPENDENCIA
        public AplicacaoUsoSoloPredominante(IUsoSoloPredominante iUsoSoloPredominante, IServicoUsoSoloPredominante iServicoUsoSoloPredominante)
        {
            _IUsoSoloPredominante = iUsoSoloPredominante;
            _IServicoUsoSoloPredominante = iServicoUsoSoloPredominante;
        }

        public async Task Adicionar(UsoSoloPredominante Objeto)
        {
           await _IUsoSoloPredominante.Adicionar(Objeto);
        }

        public Task AdicionarBarragem(UsoSoloPredominante usoSoloPredominante)
        {
            throw new NotImplementedException();
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AdicionarUsoSoloPredominante(UsoSoloPredominante usoSoloPredominante)
        {
            await _IServicoUsoSoloPredominante.AdicionarUsoSoloPredominante(usoSoloPredominante);
        }

        public Task AtualizaBarragem(UsoSoloPredominante usoSoloPredominante)
        {
            throw new NotImplementedException();
        }

        public async Task Atualizar(UsoSoloPredominante Objeto)
        {
            await _IUsoSoloPredominante.Atualizar(Objeto);
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AtualizaUsoSoloPredominante(UsoSoloPredominante usoSoloPredominante)
        {
            await _IServicoUsoSoloPredominante.AtualizaUsoSoloPredominante(usoSoloPredominante);
        }

        public async Task<UsoSoloPredominante> BuscarPorId(int Id)
        {
            return await _IUsoSoloPredominante.BuscarPorId(Id);
        }

        public async Task Excluir(UsoSoloPredominante Objeto)
        {
            await _IUsoSoloPredominante.Excluir(Objeto);
        }

        public async  Task<List<UsoSoloPredominante>> Listar()
        {
            return await _IUsoSoloPredominante.Listar();
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task<List<UsoSoloPredominante>> ListarUsoSoloPredominante()
        {
            return await _IServicoUsoSoloPredominante.ListarUsoSoloPredominante();
        }
    }
}
