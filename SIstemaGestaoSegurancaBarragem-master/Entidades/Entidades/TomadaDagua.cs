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
    [Table("TOMADA_DAGUA")]
    public class TomadaDagua : Notifica
    {
        [Column("ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("STR_LOCALIZACAO")]
        [MaxLength(255)]
        public string? Localizacao { get; set; }

        [Column("DB_COMPRIMENTO")]
        public double Comprimento { get; set; }

        [Column("STR_CONTROLE_ENTRADA")]
        [MaxLength(255)]
        public string? ControleEntrada { get; set; }
        [ForeignKey("BARRAGEM_ID")]
        [Column(Order = 1)]
        public int BarragemId { get; set; }

        [Column("STR_CONTROLE_Saida")]
        [MaxLength(255)]
        public string? ControleSaida{ get; set; }

        [Column("DB_COTAS_TOMADAS_DAGUA_ENTRADA")]
        public double CotasTomadasDaguaEntrada { get; set; }

        [Column("BOOL_FONTE_ALTERNATIVA_ENERGIA")]
        public bool FonteAlternativaEnergia { get; set; }

        [Column("BOOL_POSSIBILIDADE_MANOBRA_MANUAL")]
        public bool PossibilidadeManobraManual { get; set; }

        [Column("BOOL_COMANDO_DISTANCIA")]
        public bool ComandoDistancia { get; set; }

        [Column("DT_DATA_CADASTRO")]
        [MaxLength(50)]
        public DateTime DataCadastro { get; set; }

        [Column("DT_DATA_ALTERACAO")]
        [MaxLength(50)]
        public DateTime DataAlteracao { get; set; }


    }
}
