using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enum;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class PendenciaSiaiDPLegado : Pendencia
    {
        public PendenciaSiaiDPLegado(Orgao orgao, int ano, Byte mes, ResponsavelApuracao responsavelApuracao): base(orgao, ano, mes, responsavelApuracao) {
            // TODO Colocar chamda em um metodo para gerar a pendencia
            //this.SetDataEnvioObrigacao();
        }
        
        protected override void SetDataEnvioObrigacao(){
            Remessa envioArquivoSiaiDP = this.Orgao.InformacoesEnviadas.Find(x => x.ObrigacaoTipoEnvio == EObrigacaoTipoEnvio.ENVIO_ARQUIVO_SIAI_DP_LEGADO).Remessa;
            if(envioArquivoSiaiDP != null)
                this.DataEnvioObrigacao = envioArquivoSiaiDP.DataInclusao;
        }

        public override void SetResponsaveis()
        {
            Remessa folhaPagamentoSiaiDP = this.Orgao.InformacoesEnviadas.Find(x => x.ObrigacaoTipoEnvio == EObrigacaoTipoEnvio.FOLHA_PAGAMENTO_SIAI_DP_LEGADO).Remessa;
            Remessa quadroPessoalSiaiDP = this.Orgao.InformacoesEnviadas.Find(x => x.ObrigacaoTipoEnvio == EObrigacaoTipoEnvio.QUADRO_PESSOAL_SIAI_DP_LEGADO).Remessa;
            
            if(folhaPagamentoSiaiDP != null){
                this.NomeResponsavelRemessa = folhaPagamentoSiaiDP.renome;
                this.CpfResponsavelRemessa = folhaPagamentoSiaiDP.recpf;
                this.NomeDestinatarioComunicacao = folhaPagamentoSiaiDP.rgnome;
                this.CpfNomeDestinatarioComunicacao = folhaPagamentoSiaiDP.rgcpf;
            } else if (quadroPessoalSiaiDP != null) {
                this.NomeResponsavelRemessa = quadroPessoalSiaiDP.renome;
                this.CpfResponsavelRemessa = quadroPessoalSiaiDP.recpf;
                this.NomeDestinatarioComunicacao = quadroPessoalSiaiDP.rgnome;
                this.CpfNomeDestinatarioComunicacao = quadroPessoalSiaiDP.rgcpf;
            }

            if(!folhaPagamentoSiaiDP.rgcpf.Equals(quadroPessoalSiaiDP.rgcpf) && folhaPagamentoSiaiDP.rgcpf != null && quadroPessoalSiaiDP.rgcpf != null){
                this.NomeResponsavelRemessa = folhaPagamentoSiaiDP.renome.TrimEnd() + ", CPF: " + folhaPagamentoSiaiDP.recpf;
                this.NomeResponsavelRemessa += (" | " + quadroPessoalSiaiDP.renome.TrimEnd () + ", CPF: " + quadroPessoalSiaiDP.recpf);
                this.NomeDestinatarioComunicacao = folhaPagamentoSiaiDP.rgnome.TrimEnd () + ", CPF: " + folhaPagamentoSiaiDP.rgnome;
                this.NomeDestinatarioComunicacao += (" | " + quadroPessoalSiaiDP.rgnome.TrimEnd () + ", CPF: " + quadroPessoalSiaiDP.rgcpf);
            }
        }

        public override bool VerificarSeExistePendenciaDoOrgao()
        {
            InformacaoEnviada informacaoEnviada = this.Orgao.InformacoesEnviadas.Find(x => x.ObrigacaoTipoEnvio == EObrigacaoTipoEnvio.ENVIO_ARQUIVO_SIAI_DP_LEGADO);
            
            if(informacaoEnviada == null || informacaoEnviada.Remessa.DataInclusao > this.ResponsavelApuracao.Obrigacao.GetDataVencimento(this.Ano, this.Mes)){
                return true;
            }
            return false;
        }
    }
}