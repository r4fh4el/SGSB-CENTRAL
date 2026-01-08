using Entidades.Notificacoes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Entidades
{
    [Table("INSPECOES")]
    public class Inspecoes : Notifica
    {
        [Column("ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("STR_NOME_SETOR")]
        [MaxLength(255)]
        public string? Nome_Setor{ get; set; }


        [Column("STR_FREQUENCIA")]
        [MaxLength(255)]
        public string? EnumFrequencia { get; set; }

        [Column("DT_DATA_ULTIMA_INSPECAO_ESPECIAL")]
        [MaxLength(50)]
        public DateTime? DataUltimaInspecaoEspecial { get; set; }


        [Column("DT_DATA_REVISAO_PERIODICA_RECENTE")]
        [MaxLength(50)]
        public DateTime? DataRevisaoPeriodicaRecente { get; set; }

        [Column("BOOL_POSSUI_PAE")]
        public bool PossuiPae { get; set; }


        [Column("DT_DATA_PLANO_ACAO_EMERGENCIA")]
        [MaxLength(50)]
        public DateTime? DataPlanoAcaoEmergencia { get; set; }
       
        [Column("BOOL_POSSUI_ESTUDO_ROMPIMENTO")]
        public bool PossuiEstudoRompimento { get; set; }


        [Column("DT_DATA__ESTUDO_ROMPIMENTO")]
        [MaxLength(50)]
        public DateTime? DataEstudoRompimento { get; set; }

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
