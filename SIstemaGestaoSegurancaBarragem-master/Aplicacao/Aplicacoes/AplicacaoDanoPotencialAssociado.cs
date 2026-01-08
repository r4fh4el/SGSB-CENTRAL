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
    public class AplicacaoDanoPotencialAssociado : IAplicacaoDanoPotencialAssociado
    {
        // INTERFACE DOMINIO
        IDanoPotencialAssociado _IDanoPotencialAssociado;

        // INTERFACE SERVICO
        IServicoDanoPotencialAssociado _IServicoDanoPotencialAssociado;

        //CONTRUTOR COM INJEÇÂO DE INDEPENDENCIA
        public AplicacaoDanoPotencialAssociado(IDanoPotencialAssociado iDanoPotencialAssociado, IServicoDanoPotencialAssociado iServicoDanoPotencialAssociado)
        {
            _IDanoPotencialAssociado = iDanoPotencialAssociado;
            _IServicoDanoPotencialAssociado = iServicoDanoPotencialAssociado;
        }

        public async Task Adicionar(DanoPotencialAssociado Objeto)
        {
           await _IDanoPotencialAssociado.Adicionar(Objeto);
        }

        public Task AdicionarBarragem(DanoPotencialAssociado danoPotencialAssociado)
        {
            throw new NotImplementedException();
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AdicionarDanoPotencialAssociado(DanoPotencialAssociado danoPotencialAssociado)
        {
            await _IServicoDanoPotencialAssociado.AdicionarDanoPotencialAssociado(danoPotencialAssociado);
        }

        public Task AtualizaBarragem(DanoPotencialAssociado danoPotencialAssociado)
        {
            throw new NotImplementedException();
        }

        public async Task Atualizar(DanoPotencialAssociado Objeto)
        {
            await _IDanoPotencialAssociado.Atualizar(Objeto);
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AtualizaDanoPotencialAssociado(DanoPotencialAssociado danoPotencialAssociado)
        {
            await _IServicoDanoPotencialAssociado.AtualizaDanoPotencialAssociado(danoPotencialAssociado);
        }

        public async Task<DanoPotencialAssociado> BuscarPorId(int Id)
        {
            return await _IDanoPotencialAssociado.BuscarPorId(Id);
        }

        public async Task Excluir(DanoPotencialAssociado Objeto)
        {
            await _IDanoPotencialAssociado.Excluir(Objeto);
        }

        public async  Task<List<DanoPotencialAssociado>> Listar()
        {
            return await _IDanoPotencialAssociado.Listar();
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task<List<DanoPotencialAssociado>> ListarDanoPotencialAssociado()
        {
            return await _IServicoDanoPotencialAssociado.ListarDanoPotencialAssociado();
        }

        public async Task<DanoPotencialAssociado> GetDanoPotencialAssociadoBarragemId(int Id)
        {
            return await _IDanoPotencialAssociado.GetDanoPotencialAssociadoBarragemId(Id);
        }

    }
}
