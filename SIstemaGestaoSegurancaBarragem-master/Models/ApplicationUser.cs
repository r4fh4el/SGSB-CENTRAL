using Microsoft.AspNet.Identity.EntityFramework;
using Model.Enums;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ApplicationUser : IdentityUser
    {
        [Column("USR_NOME")]
        public string? Nome { get; set; }

        [Column("USR_IDADE")]
        public int? Idade { get; set; }

        [Column("USR_CELULAR")]
        public string? Celular { get; set; }

        [Column("USR_TIPO")]
        public TipoUsuario? Tipo { get; set; }
    }
}
