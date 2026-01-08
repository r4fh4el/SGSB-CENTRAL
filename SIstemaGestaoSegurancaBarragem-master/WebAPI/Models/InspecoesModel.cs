using System;

namespace WebAPI.Models
{
    public class InspecoesModel
    {
        public int Id { get; set; }

       
        public string? Nome_Setor { get; set; }

        public string? EnumFrequencia { get; set; }

        public DateTime? DataUltimaInspecaoEspecial { get; set; }

        public DateTime? DataRevisaoPeriodicaRecente { get; set; }

       
        public bool PossuiPae { get; set; }


        public DateTime? DataPlanoAcaoEmergencia { get; set; }

       
        public bool PossuiEstudoRompimento { get; set; }

        public DateTime? DataEstudoRompimento { get; set; }

        public int BarragemId { get; set; }
        public virtual BarragemModel? Barragem { get; set; }

       
        public DateTime DataCadastro { get; set; }

        public DateTime DataAlteracao { get; set; }


    }
}
