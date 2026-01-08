using System;

namespace SGSB.AppPopulacao.MVVM.Models
{
    public class TicketModel
    {

        public int Id { get; set; }


        public string TIcket { get; set; }


        public string Descricao { get; set; }


        public int Status { get; set; }

        public string Titulo { get; set; }


        public int IdUsuario { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime DataAlteracao { get; set; }
    }
}
