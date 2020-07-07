using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Domain.Entities
{
    public class HistoricoPeriodicidade
    {
        private HistoricoPeriodicidade() {}
        public HistoricoPeriodicidade(int idHistoricoPeriodicidade, int idObrigacao, int periodicidade, string tipoPeriodicidade, DateTime dataInicio, DateTime? dataFinal, string justificativa)
        {
            IdHistoricoPeriodicidade = idHistoricoPeriodicidade;
            IdObrigacao = idObrigacao;
            Periodicidade = periodicidade;
            TipoPeriodicidade = tipoPeriodicidade;
            DataInicio = dataInicio;
            DataFinal = dataFinal;
            Justificativa = justificativa;
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdHistoricoPeriodicidade { get; set; }
        public int IdObrigacao { get; set; }
        public int Periodicidade { get; set; }
        public string TipoPeriodicidade { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFinal { get; set; }
        public string Justificativa { get; set; }
        public DateTime? DataInativacao { get; set; }
        public DateTime DataInclusao { get; set; }
        public int IdSessao { get; set; }
        public int? IdSessaoOperacao { get; set; }
    }
}