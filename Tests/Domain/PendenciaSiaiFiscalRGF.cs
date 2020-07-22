using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enum;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class PendenciaSiaiFiscalRGF : Pendencia
    {
        public PendenciaSiaiFiscalRGF(Orgao orgao, 
                                     int ano, 
                                     Byte mes, 
                                     ResponsavelApuracao responsavelApuracao): base(orgao, ano, mes, responsavelApuracao) { }
        
        protected override void SetDataEnvioObrigacao(){
            InformacaoEnviada informacaoEnviada = this.Orgao.InformacoesEnviadas.Find(x => x.ObrigacaoTipoEnvio == EObrigacaoTipoEnvio.ENVIO_ANEXO_15);
            Remessa anexo01 = informacaoEnviada != null ? informacaoEnviada.Remessa : null;
            if(anexo01 != null)
                this.DataEnvioObrigacao = anexo01.DataInclusao;
        }

        protected override void SetResponsaveis() {
            InformacaoEnviada informacaoEnviada = this.Orgao.InformacoesEnviadas.Find(x => x.ObrigacaoTipoEnvio == EObrigacaoTipoEnvio.ENVIO_ANEXO_15);
            Remessa anexo01 = informacaoEnviada != null ? informacaoEnviada.Remessa : null;
            
            if(anexo01 != null){
                this.NomeResponsavelRemessa = anexo01.NomeResponsavelPreenchimento;
                this.CpfResponsavelRemessa = anexo01.CPFResponsavelPreenchimento;
                this.NomeDestinatarioComunicacao = anexo01.NomeResponsavelInformacao;
                this.CpfNomeDestinatarioComunicacao = anexo01.CPFResponsavelInformacao;
            }
        }
    }
}