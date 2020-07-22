using System;
using System.ComponentModel.DataAnnotations;
using Domain.Enum;

namespace Domain.Entities
{
    public class Remessa
    {
        public int? IdArquivo { get; set; }
        EObrigacaoTipoEnvio EObrigacaoTipoEnvio { get; set; }
        public string CodigoOrgao { get; set; }
        public int? IdEnvioRemessa { get; set; }
        public int? IdOrgao { get; set; }
        public DateTime DataInclusao { get; set; }
        public string NomeResponsavelEnvio { get; set; }
        public string CPFResponsavelEnvio { get; set; }
        public string NomeResponsavelInformacao { get; set; }
        public string CPFResponsavelInformacao { get; set; }   

        //Responsáveis do SiaiDp Legado
        public string rgnome { get; set; }
        public string rgcpf { get; set; }
        public string renome { get; set; }
        public string recpf { get; set; }

        // Responsáveis do Siai Fiscal
        public string NomeResponsavelPreenchimento { get; set; }
        public string CPFResponsavelPreenchimento { get; set; }

        // Opcionalidade do RGF
        public int idTipoPeriodoRGF { get; set; }
        
    }
}