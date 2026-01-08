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
    [Table("TALUDE")]
    public class Talude : Notifica
    {
        [Column("ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("BARRAGEM_ID")]
        [Column(Order = 1)]
        public int BarragemId { get; set; }
        /// <summary>
        /// Inclinação talude de montante
        /// </summary>
        [Column("DB_INCLINACAO_TALUDE_MONTANTE")]
        public double InclinacaoTaludeMontante { get; set; }
       
        /// <summary>
        /// Inclinação talude de montante
        /// </summary>
        [Column("DB_INCLINACAO_TALUDE_JUSANTE")]
        public double InclinacaoTaludeJusante { get; set; }


        [Column("STR_PROTECAO_SUPERFICIE_MONTANTE")]
        [MaxLength(255)]
        public string? TipoProtecaoSuperficieMontante { get; set; }
        

        [Column("STR_PROTECAO_SUPERFICIE_JUSANTE")]
        [MaxLength(255)]
        public string? TipoProtecaoSuperficieJusante { get; set; }

        [Column("DT_DATA_CADASTRO")]
        [MaxLength(50)]
        public DateTime DataCadastro { get; set; }

        [Column("DT_DATA_ALTERACAO")]
        [MaxLength(50)]
        public DateTime DataAlteracao { get; set; }


    }
}
