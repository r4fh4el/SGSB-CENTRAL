using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SGSB.AppPopulacao.Infra
{
    public static class Constantes
    {

        public static List<string> FiguraJuridica = new List<string>()
        {
            "Pessoa Física","Empresa Privada","Empresa Pública","Sociedade de economia","Autarquia", "Administração" ,"Outro"
    };
        public static List<string> TipoAcionamento = new List<string>()
        {
            "Manual" , "automática"
        };

        public static List<string> TipoLocal = new List<string>()
        {
            "Zona Urbana" , "Zona Rural"
        };
        public static List<string> TipoEntidade = new List<string>()
        {
            "Fazendas" , "Sítios", "Cidades", "Igrejas", "Hospitais"
        };
        public static List<string> TipoFrequenciaRegulares = new List<string>()
        {
            "Trimestral" , "Semestral", "Anual", "Bienal", "Outros"
        };

        //   public static string URI = @"https://localhost:7042";
        public static string URI = @"https://api.sgsb.com.br";
        public static string ONESIGNAL_APP_ID = @"b00d5a16-6c5d-4a2d-b9d0-43c37518565e";


    }
}
