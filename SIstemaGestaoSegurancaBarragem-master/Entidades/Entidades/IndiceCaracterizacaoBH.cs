using Entidades.Notificacoes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Entidades
{
    [Table("INDICE_CARACTERIZACAO_BH")]
    public class IndiceCaracterizacaoBH : Notifica
    {
        [Column("ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// A: Área da bacia hidrográfica (Km²)
        /// </summary>
        [Column("DB_AREA_BACIA_HIDROGRAFICA")]
        public double AreaBaciaHidrografica { get; set; }

        /// <summary>
        /// P: Perímetro (Km)
        /// </summary>
        [Column("DB_PERIMETRO")]
        public double Perimetro { get; set; }

        /// <summary>
        /// Lc: Comprimento do rio principal (Km)
        /// </summary>
        [Column("DB_COMPRIMENTO_RIO_PRINCIPAL")]
        public double ComprimentoRioPrincipal { get; set; }

        /// <summary>
        /// Lv: Comprimento vetorial do rio principal (Km)
        /// </summary>
        [Column("DB_COMPRIMENTO_VETORIAL")]
        public double ComprimentoVetorialRioPrincipal { get; set; }

        /// <summary>
        /// Amin: Altitude Mínima da bacia (m)
        /// </summary>
        [Column("DB_ALTITUDE_VETORIAL")]
        public double AltitudeVetorialRioPrincipal { get; set; }

        /// <summary>
        /// Lt: Comprimento total dos rios da bacia(Km)
        /// </summary>
        [Column("DB_COMPRIMENTO_TOTAL_RIO")]
        public double ComprimentoTotalRioBacia { get; set; }

        
        /// <summary>
        /// Amax: Altitude Máxima da bacia (m)
        /// </summary>
        [Column("DB_ALTITUDE_MAXIMA_BACIA")]
        public double AltitudeMaximaBacia { get; set; }


        /// <summary>
        /// Amax: Altitude Máxima da bacia (m)
        /// </summary>
        [Column("DB_ALTITUDE_MINIMA_BACIA")]
        public double AltitudeMinimaBacia { get; set; }

        /// <summary>
        /// Hm: Amplitude altimétrica da bacia (m)
        /// </summary>
        [Column("DB_ALTITUDE_ALTIMETRICA_BACIA_M")]
        public double AltitudeAltimetricaBaciaM { get; set; }
        /// <summary>
        /// Hm: Amplitude altimétrica da bacia (Km)
        /// </summary>
        [Column("DB_ALTITUDE_ALTIMETRICA_BACIA_KM")]
        public double AltitudeAltimetricaBaciaKM { get; set; }

        /// <summary>
        /// L: Comprimento axial da bacia(Km)
        /// </summary>
        [Column("DB_COMPRIMENTO_AXIAL_BACIA")]
        public double ComprimentoAxialBacia { get; set; }

        /// <summary>
        /// Resultado Índice de circularidade (Ic)  - Miller (1953)
        /// </summary>
        [Column("DB_RESULTADO_INDICE_CIRCULARIDADE")]
        public double ResultadoIndiceCircularidade { get; set; }

        /// <summary>
        /// Fator de forma (F) - Horton (1945)
        /// </summary>
        [Column("DB_FATOR_FORMA")]
        public double ResultadoFatorForma{ get; set; }

        /// <summary>
        ///Coeficiente de compacidade (Kc) - Lima (1969)
        /// </summary>
        [Column("DB_COEFICIENTE_COMPACIDADE")]
        public double ResultadoCoeficienteCompacidade{ get; set; }


        /// <summary>
        ///Densidade de drenagem (Dd) - Horton (1945)
        /// </summary>
        [Column("DB_DENSIDADE_DRENAGEM")]
        public double ResultadoDensidadeDrenagem{ get; set; }

        /// <summary>
        ///Coeficiente de manutenção (Cm)
        /// </summary>
        [Column("DB_COEFICIENTE_MANUTENCAO")]
        public double ResultadoCoeficienteManutencao{ get; set; }

        /// <summary>
        /// Gradiente de canais (Gc)
        /// </summary>
        [Column("DB_GRADIENTE_CANAIS")]
        public double ResultadoGradienteCanais{ get; set; }


        /// <summary>
        /// Relação de relevo (Rr)  - Christofoletti  (1969)
        /// </summary>
        [Column("DB_RELACAO_RELEVO")]
        public double ResultadoRelacaoRelevo{ get; set; }


        /// <summary>
        /// Índice de rugosidade (IR) - Christofoletti   (1969)
        /// </summary>
        [Column("DB_INDICE_RUGOSIDADE")]
        public double ResultadoIndiceRugosidade{ get; set; }

        /// <summary>
        /// Sinuosidade do curso d’água principal (S) - Schumm (1963)
        /// </summary>
        [Column("DB_SINUOSIDADE_CURSO_DAGUA")]
        public double ResultadoSinuosidadeCursoDagua{ get; set; }


        /// <summary>
        /// Barragem Id
        /// </summary>
        [Column("INT_BARRAGEM_ID")]
        public int Barragem_ID { get; set; }

        [Column("DT_DATA_CADASTRO")]
        [MaxLength(50)]
        public DateTime DataCadastro { get; set; }

        [Column("DT_DATA_ALTERACAO")]
        [MaxLength(50)]
        public DateTime DataAlteracao { get; set; }


    }
}
