using System.Data.Common;
using System.Net.Sockets;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NUnit.Framework;
using Domain.Entities;

namespace Tests.Domain.Entities {
    public class ObrigacaoTest {
        Obrigacao obrigacao;
        HistoricoPeriodicidade historicoPeriodicidadeEsperado;

        [SetUp]
        public void Init () {
            obrigacao = new ObrigacaoSiaiDPLegado(1, "obrigacao", null, null, null);
            historicoPeriodicidadeEsperado = new HistoricoPeriodicidade (2, 1, 3, "M", 20, new DateTime (2020, 6, 30), new DateTime (2020, 7, 30), "Segunda Periodicidade");
        }

        [Test]
        public void Deve_retornar_o_historico_periodicidade_vigente_da_obrigacao () {
            List<HistoricoPeriodicidade> listaDePeriodicidade = new List<HistoricoPeriodicidade> ();
            listaDePeriodicidade.Add (new HistoricoPeriodicidade (1, 1, 2, "M", 20, new DateTime (2020, 1, 1), new DateTime (2020, 6, 30), "Primeira Periodicidade"));
            listaDePeriodicidade.Add (new HistoricoPeriodicidade (2, 1, 3, "M", 20, new DateTime (2020, 6, 30), new DateTime (2020, 7, 30), "Segunda Periodicidade"));
            listaDePeriodicidade.Add (new HistoricoPeriodicidade (3, 1, 4, "M", 20, new DateTime (2020, 7, 31), null, "Terceira Periodicidade"));

            obrigacao.ListaHistoricoPeriodicidade = listaDePeriodicidade;

            var resultado = obrigacao.GetPeriodicidadeVigente();

            Assert.AreEqual (historicoPeriodicidadeEsperado.IdHistoricoPeriodicidade, resultado.IdHistoricoPeriodicidade);
        }

        [Test]
        public void Deve_lancar_uma_excecao_caso_nao_existe_historico_periodicidade_vigente () {
            List<HistoricoPeriodicidade> listaDePeriodicidade = new List<HistoricoPeriodicidade> ();
            listaDePeriodicidade.Add (new HistoricoPeriodicidade (1, 1, 2, "M", 20, new DateTime (2020, 1, 1), new DateTime (2020, 6, 30), "Primeira Periodicidade"));
            listaDePeriodicidade.Add (new HistoricoPeriodicidade (3, 1, 4, "M", 20, new DateTime (2020, 7, 31), null, "Terceira Periodicidade"));

            obrigacao.ListaHistoricoPeriodicidade = listaDePeriodicidade;

            Assert.Throws<ValidationException> (() => obrigacao.GetPeriodicidadeVigente ());
        }

        [Test]
        public void Deve_retornar_o_valor_de_multa_vigente_da_obrigacao () {
            List<HistoricoValorMulta> listaDeValorMulta = new List<HistoricoValorMulta> ();
            listaDeValorMulta.Add (new HistoricoValorMulta (1, 1, 1000, new DateTime (2020, 1, 1), new DateTime (2020, 6, 30), "Primeira Multa"));
            listaDeValorMulta.Add (new HistoricoValorMulta (2, 1, 2500, new DateTime (2020, 6, 30), new DateTime (2020, 7, 30), "Segunda Multa"));
            listaDeValorMulta.Add (new HistoricoValorMulta (3, 1, 7000, new DateTime (2020, 7, 31), null, "Terceira Multa"));

            obrigacao.ListaHistoricoValorMulta = listaDeValorMulta;

            var resultado = obrigacao.GetValorMultaVigente ();

            Assert.AreEqual (2500, resultado);
        }

        [Test]

        public void Deve_lancar_uma_excecao_caso_nao_existe_multa_vigente () {
            List<HistoricoValorMulta> listaDeValorMulta = new List<HistoricoValorMulta> ();
            listaDeValorMulta.Add (new HistoricoValorMulta (1, 1, 1000, new DateTime (2020, 1, 1), new DateTime (2020, 6, 30), "Primeira Multa"));
            listaDeValorMulta.Add (new HistoricoValorMulta (3, 1, 7000, new DateTime (2020, 7, 31), null, "Terceira Multa"));

            obrigacao.ListaHistoricoValorMulta = listaDeValorMulta;

            Assert.Throws<ValidationException> (() => obrigacao.GetValorMultaVigente ());
        }
    }
}