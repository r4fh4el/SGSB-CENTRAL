using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SGSB.Web.Models
{
    public class VertedouroModel
    { 
        public int Id { get; set; }
        public int BarragemId { get; set; }

        public string? TipoEstruturaExtravasoraPrincipal { get; set; }

        public bool VertedouroControle { get; set; }

        public int QuantidadeComportas { get; set; }
        public string? ModalidadeDissipacaoEnergia { get; set; }
        public bool TipoAcionamentoComportas { get; set; }

        public string? LocalizacaoVertedouro { get; set; }

        public double ComprimentoTotalVertedouro { get; set; }

        public double LarguraTotalVertedouro { get; set; }

        public double Altura { get; set; }

        public double CotaSoleiraVetedouro { get; set; }

        public bool VetedouroAuxiliar { get; set; }

        public int TempoRetornoVazaoMaximaProjetoAnos { get; set; }

        public double VazaoMaximaProjetoVertedor { get; set; }
    }

    }
