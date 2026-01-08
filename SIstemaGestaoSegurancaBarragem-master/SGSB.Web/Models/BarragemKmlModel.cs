using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace SGSB.Web.Models
{
    public class BarragemKmlModel
    {
        public int Id { get; set; }

        public int BarragemId { get; set; }

      
        public virtual BarragemModel Barragem { get; set; }

        public string? Coordenadas { get; set; }

        public DateTime DataCadastro { get; set; }


        public DateTime DataAlteracao { get; set; }
    }
}
