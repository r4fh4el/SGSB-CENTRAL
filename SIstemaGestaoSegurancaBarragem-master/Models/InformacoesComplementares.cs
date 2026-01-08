
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Notificacoes;
namespace Model
{
    [Table("INFORMACOES_COMPLEMENTARES")]
    public class InformacoesComplementares : Notifica
    {
        [Column("ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("STR_NOME_SETOR")]
        [MaxLength(255)]
        public string? NomeSetor { get; set; }


        [Column("BOOL_TEM_OUTORGA_CONSTRUCAO")]
        public bool TemOutorgaConstrucao { get; set; }

        [Column("STR_NUMERO_PORTARIA_OUTORGA")]
        [MaxLength(255)]
        public string? NumeroPortariaOutorga { get; set; }


        [Column("BOOL_TEM_LICENCA_OPERACAO")]
        public bool TemLicencaOperacao { get; set; }

        [Column("STR_NUMERO_PORTARIA_LICENCA_OPERACAO")]
        [MaxLength(255)]
        public string? NumeroPortariaLicencaOperacao { get; set; }


        [Column("STR_VAZAO_MINIMA_RESTITUICAO_ANO")]
        [MaxLength(255)]
        public string? VazaoMinimaRestituicaoAno { get; set; }

        [Column("BOOL_TEM_VIGIA")]
        public bool TemVigia { get; set; }


        [Column("BOOL_TEM_OPERADOR24")]
        public bool TemOperador24 { get; set; }

        [Column("BOOL_TEM_EQUIPE_OPERACAO")]
        public bool TemEquipeOperacaoBarragem{ get; set; }


        [Column("BOOL_TEM_ESCRITORIO_LOCAL")]
        public bool PossuiEscritorioLocalBarragem { get; set; }


        [Column("BOOL_TEM_EDIFICACAO_LOCAL")]
        public bool PossuiEdificacaoLocalBarragem { get; set; }

        [Column("BOOL_TEM_HISTORICO_INCIDENTE_ACIDENTE")]
        public bool TemHistoricoIndicidenteAcidente { get; set; }

        [Column("STR_HISTORICO_INCIDENTE_ACIDENTE")]
        [MaxLength(255)]
        public string? HistoricoIndicidenteAcidente { get; set; }

        [Column("INT_ANO_ULTIMA_REFORMA")]
        public int AnoUltimaReforma { get; set; }

        [ForeignKey("BARRAGEM_ID")]
        [Column(Order = 1)]
        public int BarragemId { get; set; }
        public virtual Barragem? Barragem { get; set; }

        [Column("DT_DATA_CADASTRO")]
        [MaxLength(50)]
        public DateTime DataCadastro { get; set; }

        [Column("DT_DATA_ALTERACAO")]
        [MaxLength(50)]
        public DateTime DataAlteracao { get; set; }


    }
}
