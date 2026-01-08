using Model.Notificacoes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Table("TICKET")]
    public class Ticket : Notifica
    {
        [Column("ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("STR_CODIDO")]
        [MaxLength(255)]
        public string? TIcket { get; set; }

        [Column("STR_DESCRICAO")]
        [MaxLength(255)]
        public string? Descricao { get; set; }

        [Column("INT_STATUS")]
        [MaxLength(255)]
        public int Status { get; set; }

        [Column("STR_TITULO")]
        [MaxLength(255)]
        public string? Titulo { get; set; }

        [Column("INT_ID_USUARIO")]
        [MaxLength(255)]
        public int IdUsuario { get; set; }

        [Column("DT_DATA_CADASTRO")]
        [MaxLength(50)]
        public DateTime DataCadastro { get; set; }

        [Column("DT_DATA_ALTERACAO")]
        [MaxLength(50)]
        public DateTime DataAlteracao { get; set; }


    }
}
