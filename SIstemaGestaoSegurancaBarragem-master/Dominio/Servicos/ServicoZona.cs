using Dominio.Interfaces;
using Dominio.Interfaces.InterfaceServicos;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Servicos
{
    public class ServicoZona : IServicoZona
    {
        private readonly IZona _IZona;

        public ServicoZona(IZona objZona)
        {
            _IZona = objZona;
        }

        public async Task AdicionarZona(Zona objZona)
        {

            await _IZona.Adicionar(objZona);

        }

        public async Task AtualizaZona(Zona objZona)
        {

            await _IZona.Atualizar(objZona);

        }

        public async Task<List<Zona>> ListarZona()
        {
            return await _IZona.ListarZona(n => n.idZonaNome != null);
        }

   

        public async Task<List<Zona>> ListarZonaId(int id)
        {
            return await _IZona.ListarZonaId(id);
        }
    }
}
