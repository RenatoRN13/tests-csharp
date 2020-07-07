using System;
using Domain.Enum;

namespace Domain.Entities
{
    public class Remessa
    {
        public Remessa(int? idArquivo, 
                       string codigoOrgao, 
                       int? idEnvioRemessa, 
                       int? idOrgao, 
                       DateTime dataInclusao, 
                       string nomeResponsavelEnvio, 
                       string cpfResponsavelEnvio, 
                       string nomeResponsavelInformacao, 
                       string cpfResponsavelInformacao)

        {
            IdArquivo = idArquivo;
            CodigoOrgao = codigoOrgao;
            IdEnvioRemessa = idEnvioRemessa;
            IdOrgao = idOrgao;
            DataInclusao = dataInclusao;
            NomeResponsavelEnvio = nomeResponsavelEnvio;
            CPFResponsavelEnvio = cpfResponsavelEnvio;
            NomeResponsavelInformacao = nomeResponsavelInformacao;
            CPFResponsavelInformacao = cpfResponsavelInformacao;
        }

        EObrigacaoTipoEnvio EObrigacaoTipoEnvio { get; set; }

        public int? IdArquivo { get; set; }
        public string CodigoOrgao { get; set; }
        public int? IdEnvioRemessa { get; set; }
        public int? IdOrgao { get; set; }
        public DateTime DataInclusao { get; set; }
        public string NomeResponsavelEnvio { get; set; }
        public string CPFResponsavelEnvio { get; set; }
        public string NomeResponsavelInformacao { get; set; }
        public string CPFResponsavelInformacao { get; set; }
        
        public string rgnome { get; set; }
        public string rgcpf { get; set; }
        public string renome { get; set; }
        public string recpf { get; set; }

        

    }
}