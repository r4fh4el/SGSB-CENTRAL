using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SGSB.Web.Models
{
    public class DadosGeraisViewModel
    {

        public int Id { get; set; }
        public int BarragemId { get; set; }


        public string? NomeEmpreendedor { get; set; }


        public int TipoEmpreendedor { get; set; }

        public string? EnderecoEmpreendedor { get; set; }

        public string? ResponsavelEmpreendedor { get; set; }


        public string? EmailEmpreendedor { get; set; }

        public string? TelefoneEmpreendedor { get; set; }

        public int QuantidadeBarragem { get; set; }

        public string? Latitude { get; set; }

        public string? Longitude { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime DataAlteracao { get; set; }

    }
}
