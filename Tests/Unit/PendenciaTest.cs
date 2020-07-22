using System;
using NUnit.Framework;
using Domain.Entities;
using Moq;

namespace Tests.Domain.Entities
{
    
    public class PendenciaTest
    {
        ResponsavelApuracao ResponsavelApuracao;
        Pendencia Pendencia;
        Mock<Obrigacao> ObrigacaoMock;

        [SetUp]
        public void Init () {
        }

        [Test]
        [TestCase ("2/25/2020", 20, 1, 2020, 5)]
        [TestCase ("4/10/2020", 20, 2, 2020, 21)]
        public void Deve_calcular_os_dias_de_atraso_de_uma_pendencia(string dataEnvioObrigacaoString, int diasCarencia, byte mesMonitorado, int anoMonitorado, int? diasDeAtrasoEsperado) {
            DateTime dataEnvioObrigacao = DateTime.Parse(dataEnvioObrigacaoString);
            
            DateTime dataDeVencimento = new DateTime (anoMonitorado, mesMonitorado, DateTime.DaysInMonth(anoMonitorado, mesMonitorado), 23, 59, 59).AddDays(diasCarencia);

            ObrigacaoMock = new Mock<Obrigacao>();
            ObrigacaoMock.Setup(x => x.GetPeriodicidadeVigente()).Returns(new HistoricoPeriodicidade(1, 1, 1, "M", diasCarencia, new DateTime(2019, 1, 1), null, ""));
            ObrigacaoMock.Setup(x => x.GetDataVencimento(mesMonitorado, anoMonitorado)).Returns(dataDeVencimento);

            ResponsavelApuracao = new ResponsavelApuracao{Obrigacao = ObrigacaoMock.Object};
            
            Pendencia = new Pendencia(new Orgao(), anoMonitorado, mesMonitorado, ResponsavelApuracao);
            Pendencia.ResponsavelApuracao = ResponsavelApuracao;
            Pendencia.DataEnvioObrigacao = dataEnvioObrigacao;
            int? diasDeAtrasoCalculado = Pendencia.CalcularDiasDeAtraso();
            Assert.AreEqual(diasDeAtrasoCalculado, diasDeAtrasoEsperado);
        }        
    }
}