namespace WebAPI.Models
{
    public class DanoPotencialAssociadoModel
    {
      
        public int Id { get; set; }

        public int VTR_Q1 { get; set; }

      
        public int VTR_Q2 { get; set; }

        public int VTR_Q3 { get; set; }

        public int VTR_Q4 { get; set; }

        public int VTR_Resposta { get; set; }

        public int PPV_Q1 { get; set; }

        public int PPV_Q2 { get; set; }


        public int PPV_Q3 { get; set; }

        public int PPV_Q4 { get; set; }

        public int PPV_Resposta { get; set; }
    
        public int IA_Q1 { get; set; }

        public int IA_Q2 { get; set; }

        public int IA_Resposta { get; set; }

        public int ISE_Q1 { get; set; }

        public int ISE_Q2 { get; set; }

        public int ISE_Q3 { get; set; }

        public int ISE_Resposta { get; set; }

        public int DpaTotal { get; set; }

        public int BarragemId { get; set; }
        public virtual BarragemModel? BarragemModel { get; set; }

    }
}
