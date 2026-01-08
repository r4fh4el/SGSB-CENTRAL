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
    public class AplicacaoSistemaDrenagemBarragem : IAplicacaoSistemaDrenagemBarragem
    {
        // INTERFACE DOMINIO
        ISistemaDrenagemBarragem _ISistemaDrenagemBarragem;

        // INTERFACE SERVICO
        IServicoSistemaDrenagemBarragem _IServicoSistemaDrenagemBarragem;

        //CONTRUTOR COM INJEÇÂO DE INDEPENDENCIA
        public AplicacaoSistemaDrenagemBarragem(ISistemaDrenagemBarragem iSistemaDrenagemBarragem, IServicoSistemaDrenagemBarragem iServicoSistemaDrenagemBarragem)
        {
            _ISistemaDrenagemBarragem = iSistemaDrenagemBarragem;
            _IServicoSistemaDrenagemBarragem = iServicoSistemaDrenagemBarragem;
        }

        public async Task Adicionar(SistemaDrenagemBarragem Objeto)
        {
           await _ISistemaDrenagemBarragem.Adicionar(Objeto);
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AdicionarSistemaDrenagemBarragem(SistemaDrenagemBarragem SistemaDrenagemBarragem)
        {
            await _IServicoSistemaDrenagemBarragem.AdicionarSistemaDrenagemBarragem(SistemaDrenagemBarragem);
        }

        public async Task Atualizar(SistemaDrenagemBarragem Objeto)
        {
            await _ISistemaDrenagemBarragem.Atualizar(Objeto);
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AtualizaSistemaDrenagemBarragem(SistemaDrenagemBarragem SistemaDrenagemBarragem)
        {
            await _IServicoSistemaDrenagemBarragem.AtualizaSistemaDrenagemBarragem(SistemaDrenagemBarragem);
        }

        public async Task<SistemaDrenagemBarragem> BuscarPorId(int Id)
        {
            return await _ISistemaDrenagemBarragem.BuscarPorId(Id);
        }

        public async Task Excluir(SistemaDrenagemBarragem Objeto)
        {
            await _ISistemaDrenagemBarragem.Excluir(Objeto);
        }

        public async  Task<List<SistemaDrenagemBarragem>> Listar()
        {
            return await _ISistemaDrenagemBarragem.Listar();
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task<List<SistemaDrenagemBarragem>> ListarSistemaDrenagemBarragem()
        {
            return await _IServicoSistemaDrenagemBarragem.ListarSistemaDrenagemBarragem();
        }

        public Task Adicionar(AplicacaoSistemaDrenagemBarragem Objeto)
        {
            throw new NotImplementedException();
        }

        public Task Atualizar(AplicacaoSistemaDrenagemBarragem Objeto)
        {
            throw new NotImplementedException();
        }

        public Task Excluir(AplicacaoSistemaDrenagemBarragem Objeto)
        {
            throw new NotImplementedException();
        }

        public async Task<List<SistemaDrenagemBarragem>> ListarSistemaDrenagemBarragemBarragemId(int idBarragem)
        {
            return await _IServicoSistemaDrenagemBarragem.ListarSistemaDrenagemBarragemBarragemId(idBarragem);
        }
    }
}
