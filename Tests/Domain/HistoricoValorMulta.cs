using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Domain.Entities
{
    public class HistoricoValorMulta
    {
        public HistoricoValorMulta() { }
        public HistoricoValorMulta(int idHistoricoValorMulta, int idObrigacao, decimal valorMulta, DateTime dataInicio, DateTime? dataFinal, string justificativa)
        {
            IdHistoricoValorMulta = idHistoricoValorMulta;
            IdObrigacao = idObrigacao;
            ValorMulta = valorMulta;
            DataInicio = dataInicio;
            DataFinal = dataFinal;
            Justificativa = justificativa;
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdHistoricoValorMulta { get; set; }
        public int IdObrigacao { get; set; }
        public Decimal ValorMulta { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFinal { get; set; }
        public string Justificativa { get; set; }
        public DateTime? DataInativacao { get; set; }
        public DateTime DataInclusao { get; set; }
        public int IdSessao { get; set; }
        public int? IdSessaoOperacao { get; set; }
    }
}