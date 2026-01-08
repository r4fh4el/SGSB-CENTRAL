namespace WebAPI.Models
{
    public class CategoriaRiscoModel
    {

      
        public int Id { get; set; }

        
        public int CT_A { get; set; }

        public int CT_B { get; set; }

        public int CT_C { get; set; }

        public int CT_D { get; set; }

        public int CT_E { get; set; }

        public int EC_H { get; set; }

        public int EC_I { get; set; }


        public int EC_J { get; set; }


        public int EC_L { get; set; }


        public int PS_N { get; set; }

        public int PS_O { get; set; }

        public int PS_P { get; set; }

        public int PS_Q { get; set; }

        public int PS_R { get; set; }

        public int ValorTotal { get; set; }

        public int ValorTotalEC { get; set; }



        public int BarragemId { get; set; }
        public virtual BarragemModel? BarragemModel { get; set; }

    }
}
