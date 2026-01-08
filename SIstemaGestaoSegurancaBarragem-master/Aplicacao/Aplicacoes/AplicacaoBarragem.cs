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
    public class AplicacaoBarragem : IAplicacaoBarragem
    {
        // INTERFACE DOMINIO
        IBarragem _IBarragem;

        // INTERFACE SERVICO
        IServicoBarragem _IServicoBarragem;

        //CONTRUTOR COM INJEÇÂO DE INDEPENDENCIA
        public AplicacaoBarragem(IBarragem iBarragem, IServicoBarragem iServicoBarragem)
        {
            _IBarragem = iBarragem;
            _IServicoBarragem = iServicoBarragem;
        }

        public async Task Adicionar(Barragem Objeto)
        {
           await _IBarragem.Adicionar(Objeto);
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AdicionarBarragem(Barragem barragem)
        {
            await _IServicoBarragem.AdicionarBarragem(barragem);
        }

        public async Task Atualizar(Barragem Objeto)
        {
            await _IBarragem.Atualizar(Objeto);
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AtualizaBarragem(Barragem barragem)
        {
            await _IServicoBarragem.AtualizaBarragem(barragem);
        }

        public async Task<Barragem> BuscarPorId(int Id)
        {
            return await _IBarragem.BuscarPorId(Id);
        }  
        
 
        public async Task Excluir(Barragem Objeto)
        {
            await _IBarragem.Excluir(Objeto);
        }

        public async  Task<List<Barragem>> Listar()
        {
            return await _IBarragem.Listar();
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task<List<Barragem>> ListarBarragem()
        {
            return await _IServicoBarragem.ListarBarragem();
        }

        public async Task<List<Barragem>> BuscarListaPorIdBarragem(int id)
        {
            return await _IServicoBarragem.BuscarListaPorIdBarragem(id);
        }

        public async Task<bool> DeletarBarragemRelacionais(int idBarragem)
        {
            return await _IServicoBarragem.DeletarBarragemRelacionais(idBarragem);
        }
    }
}
