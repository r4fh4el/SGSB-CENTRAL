using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SGSB.Web.Models
{
    public class UsoBarragemModel
    {
        public int Id { get; set; }
        public int BarragemId { get; set; }
        public string Nome { get; set; }
        public bool GeracaoEnergia { get; set; }

    }
}
