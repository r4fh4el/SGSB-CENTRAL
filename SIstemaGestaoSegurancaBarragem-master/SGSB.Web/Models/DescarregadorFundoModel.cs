using Plotly.Blazor.Traces.PointCloudLib.MarkerLib;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace SGSB.Web.Models
{
    public class DescarregadorFundoModel
    {
      
        public int Id { get; set; }

        public int BarragemId { get; set; }

        public string? Tipo { get; set; }

        public string? Localizacao { get; set; }

      
        public double CotaSoleiraEntrada { get; set; }

     
        public double Comprimento { get; set; }


        public double Dimensao { get; set; }

        public string? TipoComporta { get; set; }

        public bool TipoAcionamentoComporta { get; set; }

        public bool FonteAlternativaEnergia { get; set; }

        public bool ComandoDistancia { get; set; }

        public DateTime DataCadastro { get; set; }

     
        public DateTime DataAlteracao { get; set; }

    }
}
