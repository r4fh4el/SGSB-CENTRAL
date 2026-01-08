using Entidades.Notificacoes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Entidades
{
    [Table("CARACTERISTICA_BACIA")]
    public class CaracteristicaBacia : Notifica
    {
        [Column("ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        /// <summary>
        /// Nome da Bacia Hidrográfica
        /// </summary>
        [Column("TXT_NOME")]
        public string nome { get; set; }
       
        /// <summary>
        /// Nome do curso hídrico onde encontra-se a Barragem
        /// </summary>
        [Column("TXT_CURSO_HIDRICO")]
        public string cursoHidrico { get; set; }

        /// <summary>
        /// Característica predominante da Bacia
        /// </summary>
        [Column("INT_CARACTERISTICA_PREDOMINANTE")]
        public int EnumCaracteristicaPredominanteBacia { get; set; }

        [ForeignKey("USO_SOLO_PREDOMINANTE_ID")]
        [Column(Order = 1)]
        public int UsoSoloPredominanteId { get; set; }
        public virtual UsoSoloPredominante? UsoSoloPredominante { get; set; }

        /// <summary>
        /// Área da Bacia Hidrográfica(Km²)
        /// </summary>
        [Column("DB_AREA_BACIA")]
        public double AreaBacia { get; set; }
        /// <summary>
        /// Área de drenagem (Km²)
        /// </summary>
        [Column("DB_AREA_DRENAGEM")]
        public double AreaDrenagem { get; set; }

        /// <summary>
        /// Perímetro (Km)
        /// </summary>
        [Column("DB_PERIMETRO")]
        public double Perimetro { get; set; }

        /// <summary>
        /// Comprimento do rio principal (Km) 
        /// </summary>
        [Column("DB_COMPRIMENTO_RIO_PRINCIPAL")]
        public double ComprimentoRioPrincipal { get; set; }

        /// <summary>
        /// Comprimento vetorial do rio principal (Km)
        /// </summary>
        [Column("DB_COMPRIMENTO_VETORIAL_RIO_PRINCIPAL")]
        public double ComprimentoVetorialRioPrincipal { get; set; }

        /// <summary>
        /// Comprimento total dos rios da bacia (Km)
        /// </summary>
        [Column("DB_COMPRIMENTO_TOTAL_RIO")]
        public double ComprimentototalRio { get; set; }

        /// <summary>
        /// Comprimento axial da bacia (Km)
        /// </summary>
        [Column("DB_COMPRIMENTO_AXIAL")]
        public double ComprimentoAxial { get; set; }
        /// <summary>
        /// Altitude Mínima da bacia (m)
        /// </summary>
        [Column("DB_ALTITUDE_MINIMA")]
        public double AltitudeMinima { get; set; }

        /// <summary>
        /// Altitude Máxima da bacia (m)
        /// </summary>
        [Column("DB_ALTITUDE_MAXIMA")]
        public double AltitudeMaxima { get; set; }

        /// <summary>
        /// Amplitude altimétrica da bacia (m)
        /// </summary>
        [Column("DB_ALTITUDE_ALTIMETRICA_M")]
        public double AltitudeAltimetricaM { get; set; }
       
        /// <summary>
        /// Amplitude altimétrica da bacia (Km)
        /// </summary>
        [Column("DB_ALTITUDE_ALTIMETRICA_KM")]
        public double AltitudeAltimetricaKm { get; set; }
        /// <summary>
        /// Declividade da bacia (m/m)
        /// </summary>
        [Column("DB_DECLIVIDADE")]
        public double Declividade { get; set; }

        /// <summary>
        /// Município da Nascente do rio principal
        /// </summary>
        [Column("TXT_MUNICIPIO_NASCENTE_RIO")]
        public string MunicipioNascenteRio { get; set; }

        /// <summary>
        /// Município da Foz do rio principal
        /// </summary>
        [Column("TXT_MUNICIPIO_FOZ")]
        public string MunicipioFozRio { get; set; }

        /// <summary>
        /// Definição do padrão de drenagem da rede hidrográfica
        /// </summary>
        [Column("TXT_DEFINICAO_PADRAO_DRENAGEM")]
        public string DefinicaoPadraoDrenagem { get; set; }


        /// <summary>
        /// Obs: Ao terminar o preenchimento perguntar se pretende adicionar mais uma sub-bacia 
        /// </summary>
        [NotMapped]
        public bool FlagSubBacia { get; set; }


    }
}
