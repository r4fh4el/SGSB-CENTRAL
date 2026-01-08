using System;

namespace WebAPI.Models
{
  public class InformacoesComplementaresModel
    {
      
        public int Id { get; set; }

        public string? NomeSetor { get; set; }


        public bool TemOutorgaConstrucao { get; set; }

        public string? NumeroPortariaOutorga { get; set; }

        public bool TemLicencaOperacao { get; set; }

        public string? NumeroPortariaLicencaOperacao { get; set; }

        public string? VazaoMinimaRestituicaoAno { get; set; }

        public bool TemVigia { get; set; }

        public bool TemOperador24 { get; set; }

        public bool TemEquipeOperacaoBarragem { get; set; }

        public bool PossuiEscritorioLocalBarragem { get; set; }


        public bool PossuiEdificacaoLocalBarragem { get; set; }

        public bool TemHistoricoIndicidenteAcidente { get; set; }

        
        public string? HistoricoIndicidenteAcidente { get; set; }

       
        public int AnoUltimaReforma { get; set; }

        public int BarragemId { get; set; }
        public virtual BarragemModel? Barragem { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime DataAlteracao { get; set; }
    }
}
