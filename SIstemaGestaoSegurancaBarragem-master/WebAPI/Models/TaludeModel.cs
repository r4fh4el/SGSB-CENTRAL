
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace WebAPI.Models
{
    public class TaludeModel
    {
      
        public int Id { get; set; }
      
        public int BarragemId { get; set; }

        public double InclinacaoTaludeMontante { get; set; }

     
        public double InclinacaoTaludeJusante { get; set; }

        public string? TipoProtecaoSuperficieMontante { get; set; }


        public string? TipoProtecaoSuperficieJusante { get; set; }

        public DateTime DataCadastro { get; set; }

    
        public DateTime DataAlteracao { get; set; }


    }
}
