
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace WebAPI.Models

{
    public class ReservatorioModel
    {
        
        public int Id { get; set; }
        public int BarragemId { get; set; }

        
        public double CapacidadeTotalReservatorio { get; set; }

        public double VolumeUtilReservatorio { get; set; }

        public double BordaLivre { get; set; }

     
        public double MaximoMaximorum { get; set; }

        public double MaximoNormal { get; set; }

        public double MinimoOperacional { get; set; }

        public double VolumeMorto { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime DataAlteracao { get; set; }

    }
}
