using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Notificacoes
{
    /// <summary>
    /// Classe Notifica
    /// </summary>
    public class Notifica
    {

        /// <summary>
        /// Construtor Notifica
        /// </summary>
        public Notifica()
        {
            Notificacoes = new List<Notifica>();
        }

        /// <summary>
        /// Nome da Propriedade 
        /// </summary>
        [NotMapped]
        public string? NomePropriedade { get; set; }

        /// <summary>
        /// Mensagem
        /// </summary>
        [NotMapped]
        public string? Mensagem { get; set; }

        /// <summary>
        /// Listagem de Notificação
        /// </summary>
        [NotMapped]
        public List<Notifica> Notificacoes { get; set; }

        public bool ValidadePropriedadeSring(string valor, string nomePropriedade)
        {
            if (string.IsNullOrWhiteSpace(valor) || string.IsNullOrWhiteSpace(nomePropriedade))
            {
                Notificacoes.Add(new Notifica { NomePropriedade = nomePropriedade, Mensagem = "Campo Obrigatório" });

                return false;
            }
            return true;
        }


        public bool ValidadePropriedadeDecimal(decimal valor, string nomePropriedade)
        {
            if (valor < 1 || string.IsNullOrWhiteSpace(nomePropriedade))
            {
                Notificacoes.Add(new Notifica { NomePropriedade = nomePropriedade, Mensagem = "Valor deve ser maior que 0" });

                return false;
            }
            return true;
        }

    }
}
