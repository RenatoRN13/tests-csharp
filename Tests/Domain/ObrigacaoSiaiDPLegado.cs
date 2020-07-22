using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.DTO;
using Domain.Enum;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class ObrigacaoSiaiDPLegado : Obrigacao
    {
        public ObrigacaoSiaiDPLegado(byte tipoObrigacao, 
                                     string nomeObrigacao, 
                                     List<HistoricoPeriodicidade> listaHistoricoPeriodicidade, 
                                     List<HistoricoValorMulta> listaHistoricoValorMulta, 
                                     List<Rota> rotas) : base(tipoObrigacao, nomeObrigacao, listaHistoricoPeriodicidade, listaHistoricoValorMulta, rotas)
        {
        }

        public override bool VerificarSeExisteDebitoDoOrgao(Orgao orgao, DadosMonitoramentoDTO dadosMonitoramentoDTO)
        {
            InformacaoEnviada informacaoEnviada = orgao.InformacoesEnviadas.FindLast(x => x.ObrigacaoTipoEnvio == EObrigacaoTipoEnvio.ENVIO_ARQUIVO_SIAI_DP_LEGADO);
            
            if(informacaoEnviada == null || informacaoEnviada.Remessa.DataInclusao > this.GetDataVencimento(dadosMonitoramentoDTO.Mes, dadosMonitoramentoDTO.Ano)){
                return true;
            }
            return false;
        }

        public override void ValidarMonitoramento(int mes, int ano) {
            var dataHoje = DateTime.Now;
            HistoricoPeriodicidade periodicidade = this.GetPeriodicidadeVigente();
            
            if(ano > dataHoje.Year){
                throw new ValidationException("Não é possível gerar pendências para datas futuras.");
            }
            else if (ano == dataHoje.Year)
            {
                if (mes == dataHoje.AddMonths(-1).Month){
                    if(dataHoje.Day <= periodicidade.DiasCarencia) {
                        throw new ValidationException("Não é possível gerar pendências antes do dia " + (periodicidade.DiasCarencia + 1)  + " para este mês.");
                    }
                }
                else if (mes >= dataHoje.Month)
                {
                    throw new ValidationException("Não é possível gerar pendências para o mês atual ou datas futuras.");
                }
            }
        }
    }
}