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
    [Table("DESCARREGADOR_FUNDO")]
    public class DescarregadorFundo : Notifica
    {
        [Column("ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("BARRAGEM_ID")]
        [Column(Order = 1)]
        public int BarragemId { get; set; }
        [Column("STR_TIPO")]
        [MaxLength(255)]
        public string? Tipo { get; set; }

        [Column("STR_LOCALIZACAO")]
        [MaxLength(255)]
        public string? Localizacao { get; set; }

        [Column("DB_COTA_SOLEIRA_ENTRADA")]
        public double CotaSoleiraEntrada { get; set; }

        [Column("DB_COMPRIMENTO")]
        public double Comprimento { get; set; }


        [Column("DB_DIMENSAO")]
        public double Dimensao { get; set; }


        [Column("STR_TIPO_COMPORTA")]
        [MaxLength(255)]
        public string? TipoComporta { get; set; }


        [Column("BOOL_TIPO_ACIONAMENTO_COMPORTA")]
        public bool TipoAcionamentoComporta { get; set; }

        [Column("BOOL_FONTE_ALTERNATIVA_ENERGIA")]
        public bool FonteAlternativaEnergia { get; set; }

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
