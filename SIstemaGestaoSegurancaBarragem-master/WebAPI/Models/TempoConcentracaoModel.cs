namespace WebAPI.Models
{
    public class TempoConcentracaoModel
    {

        public int Id { get; set; }

        /// <summary>
        /// Comprimento do rio principal (L)
        /// </summary>

        public int BarragemId { get; set; }

        /// <summary>
        /// Comprimento do rio principal (L)
        /// </summary>

        public double ComprimentoRioPrincipal_L { get; set; }

        /// <summary>
        /// Declividade da bacia (S)
        /// </summary>

        public double DeclividadeBacia_S { get; set; }


        /// <summary>
        /// Área de drenagem (A)
        /// </summary>

        public double AreaDrenagem_A { get; set; }

        /// <summary>
        /// Kirpich (1940)
        /// </summary>

        public double ResultadoKirpich { get; set; }

        /// <summary>
        /// Corps Engineers (1946)
        /// </summary>

        public double ResultadoCorpsEngineers { get; set; }

        /// <summary>
        /// Carter (1961)
        /// </summary>

        public double ResultadoCarter { get; set; }

        /// <summary>
        /// Dooge (1956)
        /// </summary>

        public double ResultadoDooge { get; set; }

        /// <summary>
        /// Ven te Chow (1962)
        /// </summary>

        public double ResultadoVenTeChow { get; set; }


        public DateTime DataCadastro { get; set; }


        public DateTime DataAlteracao { get; set; }
    }
}
