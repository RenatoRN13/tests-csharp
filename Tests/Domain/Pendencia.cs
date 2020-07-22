using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using Domain.ViewEntities;
using Domain.Entities;
using System.Collections.Generic;
using Domain.Enum;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Pendencia
    {
        private Pendencia(){
            this.Status = EPendenciaStatus.NOVA;
        }

        public Pendencia(Orgao orgao, int ano, Byte mes, ResponsavelApuracao responsavelApuracao){
            this.Status = EPendenciaStatus.NOVA;

            this.Orgao = orgao;
            this.IdOrgao = orgao.IdOrgao;
            this.NomeOrgao = orgao.NomeOrgao;
            this.Ano = ano;
            this.Mes = mes;
            this.IdResponsavelApuracao = responsavelApuracao.IdResponsavelApuracao;
            this.DescricaoAutuacao = responsavelApuracao.Obrigacao.DescricaoAutuacao;
            
            this.Observacao = "Pendência Gerada Automaticamente, em: " + DateTime.Now + ".";
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPendencia { get; set; }
        public int IdResponsavelApuracao { get; set; }
        public int? IdProcesso { get; set; }
        public int IdOrgao { get; set; }
        public string NomeOrgao { get; set; }
        public string NomeResponsavelRemessa { get; set; }
        public string NomeDestinatarioComunicacao { get; set; }
        public string CpfResponsavelRemessa { get; set; }
        public string CpfNomeDestinatarioComunicacao { get; set; }
        public int Ano { get; set; }
        public Byte Mes { get; set; }
        public string Observacao { get; set; }
        public string NumeroProcesso { get; set; }
        public string AnoProcesso { get; set; }
        public int IdSessao { get; set; }
        public int? IdSessaoOperacao { get; set; }
        public string MotivoBaixa { get; set; }
        public string DescricaoAutuacao { get; set; }
        public string SugestaoCorrecao { get; set; }
        public string NomeArquivoRascunhoInformacaoInicial { get; set; }
        public Decimal? ValorMulta { get; set; }
        public DateTime? DataInativacao { get; set; }
        public DateTime DataInclusao { get; set; }
        public string UsuarioInformacaoInicial { get; set; }
        public Boolean DespachoDiretor { get; set; }
        public DateTime? DataEnvioObrigacao { get; set; }
        public virtual ResponsavelApuracao ResponsavelApuracao { get; set; }
        
        [NotMapped]
        public virtual Orgao Orgao { get; set; }

        [NotMapped]
        public virtual EPendenciaStatus Status { get; set; }
        public Boolean Comparar(Pendencia pendencia){
            if(this.IdOrgao == pendencia.IdOrgao &&
                this.Mes == pendencia.Mes &&
                this.Ano == pendencia.Ano &&
                this.IdResponsavelApuracao == pendencia.IdResponsavelApuracao){
                    return true;
            }
            return false;
        }

        private Boolean IdentificarSePendenciaPrecisaSerAtualizada(Pendencia pendenciaAntiga){
            if(this.DataEnvioObrigacao != null && pendenciaAntiga.DataEnvioObrigacao == null)
                return true;
            return false;
        }

        public void VerificarCadastro(IEnumerable<Pendencia> pendenciasJaCadastradas){
            
            foreach (Pendencia pendenciaJaCadastrada in pendenciasJaCadastradas)
            {
                if(this.Comparar(pendenciaJaCadastrada) == true){
                    if(this.IdentificarSePendenciaPrecisaSerAtualizada(pendenciaJaCadastrada)){
                        this.IdPendencia = pendenciaJaCadastrada.IdPendencia;
                        this.IdSessao = pendenciaJaCadastrada.IdSessao;
                        this.DataInclusao = pendenciaJaCadastrada.DataInclusao;

                        this.Status = EPendenciaStatus.A_SER_ATUALIZADA;
                        break;
                    }
                    else {
                        this.Status = EPendenciaStatus.JA_CADASTRADA;
                        break;
                    }
                }
            }
        }

        public int? CalcularDiasDeAtraso () {
            DateTime? dataVencimentoObrigacao = this.ResponsavelApuracao.Obrigacao.GetDataVencimento(this.Mes, this.Ano);
            DateTime dataEnvio = DateTime.Now;
            DateTime dataVencimentoObrigacaoNaoNula;

            if (dataVencimentoObrigacao == null) {
                return null;
            } else {
                dataVencimentoObrigacaoNaoNula = Convert.ToDateTime (dataVencimentoObrigacao);
            }

            if (this.DataEnvioObrigacao != null) {
                dataEnvio = (DateTime) this.DataEnvioObrigacao;
                return (dataEnvio.Date - dataVencimentoObrigacaoNaoNula.Date).Days;
            }

            return (dataEnvio.Date - dataVencimentoObrigacaoNaoNula).Days;

        }

        public void SetInformacoes(Obrigacao obrigacao) {
            this.SetResponsaveis();
            this.SetDataEnvioObrigacao();
            this.SetValorMulta(obrigacao);
        }

        protected void SetValorMulta(Obrigacao obrigacao){
            this.ValorMulta = obrigacao.GetValorMultaVigente();
        }
        protected virtual void SetResponsaveis() {}
        protected virtual void SetDataEnvioObrigacao() {}
    }
}