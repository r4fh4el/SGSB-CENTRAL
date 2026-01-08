
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace WebAPI.Models
{

    public class TomadaDaguaModel
    {
     
        public int Id { get; set; }

      
        public string? Localizacao { get; set; }

        public double Comprimento { get; set; }

        public string? ControleEntrada { get; set; }
      
        public int BarragemId { get; set; }

      
        public string? ControleSaida { get; set; }

        public double CotasTomadasDaguaEntrada { get; set; }

        public bool FonteAlternativaEnergia { get; set; }

        public bool PossibilidadeManobraManual { get; set; }

        public bool ComandoDistancia { get; set; }

      
        public DateTime DataCadastro { get; set; }

       
        public DateTime DataAlteracao { get; set; }
    }
}
