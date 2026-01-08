using Entidades.Entidades;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class TempoRupturaModel
    {
        public int Id { get; set; }
        public double? valorTempoRuptura { get; set; }

        public int BarragemId { get; set; }
        public virtual Barragem? Barragem { get; set; }

    
        public DateTime DataCadastro { get; set; }

     
        public DateTime DataAlteracao { get; set; }
    }
}
