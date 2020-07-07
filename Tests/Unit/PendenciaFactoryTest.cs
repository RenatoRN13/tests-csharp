using System.Reflection;
using System.Net.Security;
using System.Data.Common;
using System.Net.Sockets;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NUnit.Framework;
using Domain.Entities;
using Domain.Factories;
using Moq;
using Domain.Enum;

namespace Tests.Domain.Entities
{
    public class PendenciaFactoryTest
    {
        Mock<ResponsavelApuracao> MockResponsavelApuracao;
        ResponsavelApuracao ResponsavelApuracao;

        [SetUp]
        public void Init () {
            ResponsavelApuracao = new ResponsavelApuracao(new Obrigacao());
        }

        [Test]
        [TestCase (typeof(PendenciaSiaiDPLegado), ETipoObrigacao.SIAI_DP_LEGADO)]
        public void Deve_retornar_uma_pendencia_com_tipo_valido(Type type, ETipoObrigacao tipoObrigacao) {
            ResponsavelApuracao.Obrigacao.TipoObrigacao = (byte) tipoObrigacao;
            
            Pendencia Pendencia = PendenciaFactory.GerarPendencia(new Orgao(), 2020, 1, ResponsavelApuracao);

            Assert.IsInstanceOf(type, Pendencia);
        }        
    }
}