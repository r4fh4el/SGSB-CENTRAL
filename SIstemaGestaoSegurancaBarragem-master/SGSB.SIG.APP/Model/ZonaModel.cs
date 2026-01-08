using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGSB.SIG.APP.Model
{
    public class ZonaModel
    {
        public int id { get; set; }
        public string idZonaNome { get; set; }
        public string cpf { get; set; }
        public string endereco { get; set; }
        public string nomeCidadao { get; set; }
        public string numeroTelefone { get; set; }
        public int numeroPessoas{ get; set; }
        public string observacao { get; set; }
        public string renda { get; set; }
        public string area { get; set; }
        public string usuario { get; set; }
        public DateTime dataNascimento { get; set; }
    }
}
