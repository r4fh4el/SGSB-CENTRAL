using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGSB.Web.Models
{
    public class HidrogramaTriangulaModel
    {
        public int Id { get; set; }


        public double volumesReservatorio { get; set; }

        public double valorQp { get; set; }
        public double valorTempoPico { get; set; }


        public int BarragemId { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime DataAlteracao { get; set; }
    }
}
