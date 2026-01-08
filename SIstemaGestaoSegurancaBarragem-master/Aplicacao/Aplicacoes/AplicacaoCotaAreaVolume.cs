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
    public class AplicacaoCotaAreaVolume : IAplicacaoCotaAreaVolume
    {
        // INTERFACE DOMINIO
        ICotaAreaVolume _ICotaAreaVolume;

        // INTERFACE SERVICO
        IServicoCotaAreaVolume _IServicoCotaAreaVolume;

        //CONTRUTOR COM INJEÇÂO DE INDEPENDENCIA
        public AplicacaoCotaAreaVolume(ICotaAreaVolume iCotaAreaVolume, IServicoCotaAreaVolume iServicoCotaAreaVolume)
        {
            _ICotaAreaVolume = iCotaAreaVolume;
            _IServicoCotaAreaVolume = iServicoCotaAreaVolume;
        }

        public async Task Adicionar(CotaAreaVolume Objeto)
        {
           await _ICotaAreaVolume.Adicionar(Objeto);
        }

        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AdicionarCotaAreaVolume(CotaAreaVolume cotaAreaVolume)
        {
            await _IServicoCotaAreaVolume.AdicionarCotaAreaVolume(cotaAreaVolume);
        }

        public async Task Atualizar(CotaAreaVolume Objeto)
        {
            await _ICotaAreaVolume.Atualizar(Objeto);
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task AtualizaCotaAreaVolume(CotaAreaVolume cotaAreaVolume)
        {
            await _IServicoCotaAreaVolume.AtualizaCotaAreaVolume(cotaAreaVolume);
        }

        public async Task<CotaAreaVolume> BuscarPorId(int Id)
        {
            return await _ICotaAreaVolume.BuscarPorId(Id);
        }

        public async Task Excluir(CotaAreaVolume Objeto)
        {
            await _ICotaAreaVolume.Excluir(Objeto);
        }

        public async  Task<List<CotaAreaVolume>> Listar()
        {
            return await _ICotaAreaVolume.Listar();
        }
        // CUSTOMIZAVEL RETORNA DO SERVICO
        public async Task<List<CotaAreaVolume>> ListarCotaAreaVolume()
        {
            return await _IServicoCotaAreaVolume.ListarCotaAreaVolume();
        }

        public Task Adicionar(AplicacaoCotaAreaVolume Objeto)
        {
            throw new NotImplementedException();
        }

        public Task Atualizar(AplicacaoCotaAreaVolume Objeto)
        {
            throw new NotImplementedException();
        }

        public Task Excluir(AplicacaoCotaAreaVolume Objeto)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CotaAreaVolume>> ListarCotaAreaVolumeBarragemId(int idBarragem)
        {
            return await _IServicoCotaAreaVolume.ListarCotaAreaVolumeBarragemId(idBarragem);
        }

    }
}
