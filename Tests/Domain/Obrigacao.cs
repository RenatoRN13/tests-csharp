using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.DTO;
using Domain.Enum;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Obrigacao
    {
        public Obrigacao () {

        }

        protected Obrigacao(byte tipoObrigacao, 
                            string nomeObrigacao, 
                            List<HistoricoPeriodicidade> listaHistoricoPeriodicidade, 
                            List<HistoricoValorMulta> listaHistoricoValorMulta, 
                            List<Rota> rotas)
        {
            TipoObrigacao = tipoObrigacao;
            NomeObrigacao = nomeObrigacao;
            ListaHistoricoPeriodicidade = listaHistoricoPeriodicidade;
            ListaHistoricoValorMulta = listaHistoricoValorMulta;
            Rotas = rotas;
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

        public Decimal GetValorMultaVigente(){
            var hoje = DateTime.Now;
            HistoricoValorMulta objetoValorMultaVigente = this.ListaHistoricoValorMulta.Find(x => (hoje >= x.DataInicio)  && (hoje <= x.DataFinal || x.DataFinal == null));
            
            if(objetoValorMultaVigente == null){
                throw new ValidationException("Obrigação não possui valor de multa vigente.");    
            }
            return objetoValorMultaVigente.ValorMulta;
        }

        public virtual HistoricoPeriodicidade GetPeriodicidadeVigente(){
            var hoje = DateTime.Now;
            HistoricoPeriodicidade objetoPeriodoVigente = this.ListaHistoricoPeriodicidade.Find(x => (hoje >= x.DataInicio)  && (hoje <= x.DataFinal || x.DataFinal == null));
            
            if(objetoPeriodoVigente == null){
                throw new ValidationException("A obrigação " + this.NomeObrigacao + " não possui periodicidade vigente.");
            }
            return objetoPeriodoVigente;
        }

        public virtual DateTime? GetDataVencimento(int mesMonitorado, int anoMonitorado) {

            HistoricoPeriodicidade historicoPeriodicidadeVigente = this.GetPeriodicidadeVigente();
            DateTime dataMonitoramento = new DateTime (anoMonitorado, mesMonitorado, DateTime.DaysInMonth(anoMonitorado, mesMonitorado));
            DateTime primeiroDiaDoAno = new DateTime(anoMonitorado, 1, 1);

            DateTime? dataDeVencimento = null;

            int diasDeCarencia = historicoPeriodicidadeVigente.DiasCarencia;
            
            switch (historicoPeriodicidadeVigente.TipoPeriodicidade) {
                // case "D":
                //     for (DateTime dataVencimentoObrigacao = primeiroDiaDoAno; dataVencimentoObrigacao <= dataMonitoramento; dataVencimentoObrigacao = dataVencimentoObrigacao.AddMonths (historicoPeriodicidade.Periodicidade)) {
                //         if (dataVencimentoObrigacao.Day == dataMonitoramento.Day) {
                //             dataDeVencimento = dataVencimentoObrigacao.AddDays(diasDeCarencia); 
                //         }   
                //     }
                //     break;
                case "M":
                    for(int mes = historicoPeriodicidadeVigente.Periodicidade; mes <= 12; mes += historicoPeriodicidadeVigente.Periodicidade){
                        if(mes == mesMonitorado){
                            dataDeVencimento = new DateTime (anoMonitorado, mesMonitorado, DateTime.DaysInMonth(anoMonitorado, mesMonitorado), 23, 59, 59).AddDays(diasDeCarencia);
                        }
                    }
                    break;
                case "A":
                    if(mesMonitorado == 12) {
                        var ultimoDiaDoAnoAnalisado =  new DateTime (anoMonitorado, 12, 31, 23, 59, 59); 
                        dataDeVencimento = ultimoDiaDoAnoAnalisado.AddDays(diasDeCarencia);
                    }
                    break;
            }
            return dataDeVencimento;
        }

        public virtual bool VerificarSeExisteDebitoDoOrgao(Orgao orgao, DadosMonitoramentoDTO dadosMonitoramentoDTO)
        {
            throw new NotImplementedException();
        }

        public virtual void ValidarMonitoramento(int mes, int ano)
        {
            throw new NotImplementedException();
        }
    }
}