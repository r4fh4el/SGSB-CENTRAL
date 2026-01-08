
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
    [Table("ZONA")]
    public class Zona : Notifica
    {
        [Column("ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("STR_ID_ZONA_NOME")]
        [MaxLength(255)]
        public string idZonaNome { get; set; }

        [Column("STR_CPF")]
        [MaxLength(255)]
        public string cpf { get; set; }
       
        [Column("STR_ENDERECO")]
        [MaxLength(255)]
        public string endereco { get; set; }
        [Column("STR_NOME_CIDADAO")]
        [MaxLength(255)]
        public string nomeCidadao { get; set; }
        [Column("STR_NUMERO_TELEFONE")]
        [MaxLength(255)]

        public string numeroTelefone { get; set; }
        [Column("STR_NUMERO_PESSOAS")]
        [MaxLength(255)]

        public int numeroPessoas { get; set; }
        [Column("STR_OBSERVACAO")]
        [MaxLength(255)]

        public string observacao { get; set; }

        [Column("STR_USUARIO")]
        [MaxLength(255)]
        public string usuario { get; set; }

        [Column("STR_RENDA")]
        [MaxLength(255)]

        public string renda { get; set; }
        [Column("STR_AREA")]
        [MaxLength(255)]

        public string area { get; set; }

        [Column("DT_DATA_NASCIMENTO")]
        [MaxLength(50)]
        public DateTime dataNascimento { get; set; }

   


    }
}
