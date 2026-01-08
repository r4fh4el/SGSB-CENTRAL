namespace WebAPI.Models
{
    public class IndiceCaracterizacaoBHModel
    {

        public int Id { get; set; }

        /// <summary>
        /// A: Área da bacia hidrográfica (Km²)
        /// </summary>

        public double AreaBaciaHidrografica { get; set; }

        /// <summary>
        /// P: Perímetro (Km)
        /// </summary>

        public double Perimetro { get; set; }

        /// <summary>
        /// Lc: Comprimento do rio principal (Km)
        /// </summary>

        public double ComprimentoRioPrincipal { get; set; }

        /// <summary>
        /// Lv: Comprimento vetorial do rio principal (Km)
        /// </summary>

        public double ComprimentoVetorialRioPrincipal { get; set; }

        /// <summary>
        /// Amin: Altitude Mínima da bacia (m)

        public double AltitudeVetorialRioPrincipal { get; set; }

        /// <summary>
        /// Lt: Comprimento total dos rios da bacia(Km)
        /// </summary>

        public double ComprimentoTotalRioBacia { get; set; }


        /// <summary>
        /// Amax: Altitude Máxima da bacia (m)
        /// </summary>
        ///
        public double AltitudeMaximaBacia { get; set; }


        /// <summary>
        /// Amax: Altitude Máxima da bacia (m)
        /// </summary>

        public double AltitudeMinimaBacia { get; set; }

        /// <summary>
        /// Hm: Amplitude altimétrica da bacia (m)
        /// </summary>

        public double AltitudeAltimetricaBaciaM { get; set; }
        /// <summary>
        /// Hm: Amplitude altimétrica da bacia (Km)
        /// </summary>

        public double AltitudeAltimetricaBaciaKM { get; set; }

        /// <summary>
        /// L: Comprimento axial da bacia(Km)
        /// </summary>

        public double ComprimentoAxialBacia { get; set; }

        /// <summary>
        /// Resultado Índice de circularidade (Ic)  - Miller (1953)
        /// </summary>

        public double ResultadoIndiceCircularidade { get; set; }

        /// <summary>
        /// Fator de forma (F) - Horton (1945)
        /// </summary>

        public double ResultadoFatorForma { get; set; }

        /// <summary>
        ///Coeficiente de compacidade (Kc) - Lima (1969)
        /// </summary>

        public double ResultadoCoeficienteCompacidade { get; set; }


        /// <summary>
        ///Densidade de drenagem (Dd) - Horton (1945)
        /// </summary>

        public double ResultadoDensidadeDrenagem { get; set; }

        /// <summary>
        ///Coeficiente de manutenção (Cm)
        /// </summary>

        public double ResultadoCoeficienteManutencao { get; set; }

        /// <summary>
        /// Gradiente de canais (Gc)
        /// </summary>
        /// [
        public double ResultadoGradienteCanais { get; set; }


        /// <summary>
        /// Relação de relevo (Rr)  - Christofoletti  (1969)
        /// </summary>

        public double ResultadoRelacaoRelevo { get; set; }


        /// <summary>
        /// Índice de rugosidade (IR) - Christofoletti   (1969)
        /// </summary>

        public double ResultadoIndiceRugosidade { get; set; }

        /// <summary>
        /// Sinuosidade do curso d’água principal (S) - Schumm (1963)
        /// </summary>

        public double ResultadoSinuosidadeCursoDagua { get; set; }


        /// <summary>
        /// Barragem Id
        /// </summary>

        public int Barragem_ID { get; set; }



    }
}
