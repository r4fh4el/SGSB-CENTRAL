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
    public class AplicacaoSistemaDrenagem : IAplicacaoSistemaDrenagem
    {
        // INTERFACE DOMINIO
        ISistemaDrenagem _ISistemaDrenagem;

        // INTERFACE SERVICO
        IServicoSistemaDrenagem _IServicoSistemaDrenagem;

        //CONTRUTOR COM INJEÇÂO DE INDEPENDENCIA
        public AplicacaoSistemaDrenagem(ISistemaDrenagem iSistemaDrenagem, IServicoSistemaDrenagem iServicoSistemaDrenagem)
        {
            _ISistemaDrenagem = iSistemaDrenagem;
            _IServicoSistemaDrenagem = iServicoSistemaDrenagem;
        }

        public async Task Adicionar(SistemaDrenagem Objeto)
        {
           await _ISistemaDrenagem.Adicionar(Objeto);
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AdicionarSistemaDrenagem(SistemaDrenagem sistemaDrenagem)
        {
            await _IServicoSistemaDrenagem.AdicionarSistemaDrenagem(sistemaDrenagem);
        }

        public async Task Atualizar(SistemaDrenagem Objeto)
        {
            await _ISistemaDrenagem.Atualizar(Objeto);
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AtualizaSistemaDrenagem(SistemaDrenagem sistemaDrenagem)
        {
            await _IServicoSistemaDrenagem.AtualizaSistemaDrenagem(sistemaDrenagem);
        }

        public async Task<SistemaDrenagem> BuscarPorId(int Id)
        {
            return await _ISistemaDrenagem.BuscarPorId(Id);
        }

        public async Task Excluir(SistemaDrenagem Objeto)
        {
            await _ISistemaDrenagem.Excluir(Objeto);
        }

        public async  Task<List<SistemaDrenagem>> Listar()
        {
            return await _ISistemaDrenagem.Listar();
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task<List<SistemaDrenagem>> ListarSistemaDrenagem()
        {
            return await _IServicoSistemaDrenagem.ListarSistemaDrenagem();
        }

        public Task Adicionar(AplicacaoSistemaDrenagem Objeto)
        {
            throw new NotImplementedException();
        }

        public Task Atualizar(AplicacaoSistemaDrenagem Objeto)
        {
            throw new NotImplementedException();
        }

        public Task Excluir(AplicacaoSistemaDrenagem Objeto)
        {
            throw new NotImplementedException();
        }

     
    }
}
