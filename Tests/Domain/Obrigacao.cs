using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enum;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Obrigacao
    {
        public Obrigacao() {
        }

        public int IdObrigacao { get; set; }
        public Byte TipoObrigacao { get; set; }
        public string NomeObrigacao { get; set; }
        public string DescricaoAutuacao { get; set; }        
        public bool Ativo { get; set; }
        public int IdSessao { get; set; }
        public int? IdSessaoOperacao { get; set; }
        public DateTime? DataInativacao { get; set; }
        public DateTime DataInclusao { get; set; }
        public virtual List<HistoricoPeriodicidade> ListaHistoricoPeriodicidade { get; set; }
        public virtual List<HistoricoValorMulta> ListaHistoricoValorMulta { get; set; }

        [NotMapped]
        public virtual List<Rota> Rotas { get; private set; }

        public void AdicionarRota(EObrigacaoTipoEnvio eObrigacaoTipoEnvio, string url){
            this.Rotas.Add(new Rota(eObrigacaoTipoEnvio, url));
        }

        public void InicializarRotas(){
            switch (this.TipoObrigacao)
            {
                case (Byte) ETipoObrigacao.SIAI_DP_LEGADO:
                    this.AdicionarRota(EObrigacaoTipoEnvio.ENVIO_ARQUIVO_SIAI_DP_LEGADO, "PEGAR URL DA ROTA NO APPSETTINGS");
                    this.AdicionarRota(EObrigacaoTipoEnvio.FOLHA_PAGAMENTO_SIAI_DP_LEGADO, "PEGAR URL DA ROTA NO APPSETTINGS");
                    this.AdicionarRota(EObrigacaoTipoEnvio.QUADRO_PESSOAL_SIAI_DP_LEGADO, "PEGAR URL DA ROTA NO APPSETTINGS");
                    break;
                case (Byte) ETipoObrigacao.SIAI_DP:
                default:
                    throw new ValidationException("Obrigação inválida.");
            }
        }

        public Decimal GetValorMultaVigente(){
            var hoje = DateTime.Now;
            HistoricoValorMulta objetoValorMultaVigente = this.ListaHistoricoValorMulta.Find(x => (hoje >= x.DataInicio)  && (hoje <= x.DataFinal || x.DataFinal == null));
            
            if(objetoValorMultaVigente == null){
                throw new ValidationException("Obrigação não possui valor de multa vigente.");    
            }
            return objetoValorMultaVigente.ValorMulta;
        }

        public HistoricoPeriodicidade GetPeriodicidadeVigente(){
            var hoje = DateTime.Now;
            HistoricoPeriodicidade objetoPeriodoVigente = this.ListaHistoricoPeriodicidade.Find(x => (hoje >= x.DataInicio)  && (hoje <= x.DataFinal || x.DataFinal == null));
            
            if(objetoPeriodoVigente == null){
                throw new ValidationException("A obrigação " + this.NomeObrigacao + " não possui periodicidade vigente.");
            }
            return objetoPeriodoVigente;
        }

        public DateTime? GetDataVencimento(int anoMonitorado, int mesMonitorado) {
            // Fixado dia 20 para as obrigações da DDP
            int diasDeCarencia = 20;

            HistoricoPeriodicidade historicoPeriodicidade = this.GetPeriodicidadeVigente();
            DateTime dataMonitoramento = new DateTime (anoMonitorado, mesMonitorado, DateTime.DaysInMonth(anoMonitorado, mesMonitorado));
            DateTime primeiroDiaDoAno = new DateTime(anoMonitorado, 1, 1);

            DateTime? dataDeVencimento = null;
            
            switch (historicoPeriodicidade.TipoPeriodicidade) {
                // case "D":
                //     for (DateTime dataVencimentoObrigacao = primeiroDiaDoAno; dataVencimentoObrigacao <= dataMonitoramento; dataVencimentoObrigacao = dataVencimentoObrigacao.AddMonths (historicoPeriodicidade.Periodicidade)) {
                //         if (dataVencimentoObrigacao.Day == dataMonitoramento.Day) {
                //             dataDeVencimento = dataVencimentoObrigacao.AddDays(diasDeCarencia); //FIXME: historicoPeriodicidade.DiasDeCarencia
                //         }   
                //     }
                //     break;
                case "M":
                    for(int mes = historicoPeriodicidade.Periodicidade; mes <= 12; mes += historicoPeriodicidade.Periodicidade){
                        if(mes == mesMonitorado){
                            //TODO: adicionar campo DiasDeCarencia em historicoPeriodicidade.
                            dataDeVencimento = new DateTime (anoMonitorado, mesMonitorado, DateTime.DaysInMonth(anoMonitorado, mesMonitorado)).AddDays(diasDeCarencia);
                        }
                    }
                    break;
                case "A":
                    if(mesMonitorado == 12) {
                        //TODO: adicionar campo DiasDeCarencia em historicoPeriodicidade.
                        var ultimoDiaDoAnoAnalisado =  new DateTime (anoMonitorado, 12, 31);
                        dataDeVencimento = ultimoDiaDoAnoAnalisado.AddDays(diasDeCarencia);
                    }
                    break;
            }

            return dataDeVencimento;
        }
    }
}