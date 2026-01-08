using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class HidrogramaParabolicoModel
    {
        public int Id { get; set; }

        public double? valorVazao { get; set; }

        public double? valorQp { get; set; }

        public double? valorTempoHora { get; set; }


        public int BarragemId { get; set; }
       
        public DateTime DataCadastro { get; set; }

     
        public DateTime DataAlteracao { get; set; }


    }
}
