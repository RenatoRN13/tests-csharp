using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.DTO;
using Domain.Enum;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class ObrigacaoSiaiDP : Obrigacao
    {
        public ObrigacaoSiaiDP(byte tipoObrigacao, 
                                     string nomeObrigacao, 
                                     List<HistoricoPeriodicidade> listaHistoricoPeriodicidade, 
                                     List<HistoricoValorMulta> listaHistoricoValorMulta, 
                                     List<Rota> rotas) : base(tipoObrigacao, nomeObrigacao, listaHistoricoPeriodicidade, listaHistoricoValorMulta, rotas)
        {
        }

        public override void ValidarMonitoramento(int mes, int ano)
        {
            throw new NotImplementedException();
        }

        public override bool VerificarSeExisteDebitoDoOrgao(Orgao orgao, DadosMonitoramentoDTO dadosMonitoramentoDTO)
        {
            InformacaoEnviada informacaoEnviada = orgao.InformacoesEnviadas.FindLast(x => x.ObrigacaoTipoEnvio == EObrigacaoTipoEnvio.ENVIO_ARQUIVO_SIAI_DP);
            
            if(informacaoEnviada == null || informacaoEnviada.Remessa.DataInclusao > this.GetDataVencimento(dadosMonitoramentoDTO.Mes, dadosMonitoramentoDTO.Ano)){
                return true;
            }
            return false;
        }
    }
}