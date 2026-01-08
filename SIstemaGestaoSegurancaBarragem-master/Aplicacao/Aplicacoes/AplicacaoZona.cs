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
    public class AplicacaoZona : IAplicacaoZona
    {
        // INTERFACE DOMINIO
        IZona _IZona;

        // INTERFACE SERVICO
        IServicoZona _IServicoZona;

        //CONTRUTOR COM INJEÇÂO DE INDEPENDENCIA
        public AplicacaoZona(IZona iZona, IServicoZona iServicoZona)
        {
            _IZona = iZona;
            _IServicoZona = iServicoZona;
        }

        public async Task Adicionar(Zona Objeto)
        {
           await _IZona.Adicionar(Objeto);
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AdicionarZona(Zona tipoMaterialBarragem)
        {
            await _IServicoZona.AdicionarZona(tipoMaterialBarragem);
        }

        public async Task Atualizar(Zona Objeto)
        {
            await _IZona.Atualizar(Objeto);
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AtualizaZona(Zona tipoMaterialBarragem)
        {
            await _IServicoZona.AtualizaZona(tipoMaterialBarragem);
        }

        public async Task<Zona> BuscarPorId(int Id)
        {
            return await _IZona.BuscarPorId(Id);
        }

        public async Task Excluir(Zona Objeto)
        {
            await _IZona.Excluir(Objeto);
        }

        public async  Task<List<Zona>> Listar()
        {
            return await _IZona.Listar();
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task<List<Zona>> ListarZona()
        {
            return await _IServicoZona.ListarZona();
        }

        public async Task<List<Zona>> ListarZonaId(int idBarragem)
        {
            return await _IServicoZona.ListarZonaId(idBarragem);
        }
    }
}
