using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.DTO;
using Domain.Enum;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class ObrigacaoSiaiFiscalRGF : Obrigacao
    {
        public ObrigacaoSiaiFiscalRGF(byte tipoObrigacao, 
                             string nomeObrigacao, 
                             List<HistoricoPeriodicidade> listaHistoricoPeriodicidade, 
                             List<HistoricoValorMulta> listaHistoricoValorMulta, 
                             List<Rota> rotas) : base(tipoObrigacao, nomeObrigacao, listaHistoricoPeriodicidade, listaHistoricoValorMulta, rotas)
        {
        }

        public override void ValidarMonitoramento(int mes, int ano) {
            var dataHoje = DateTime.Now;
            
            if(mes % 2 != 0 || mes == 2 || mes == 10){
                throw new ValidationException("Não há pendências a serem geradas para este mês.");
            }
            if(ano > dataHoje.Year){
                throw new ValidationException("Não é possível gerar pendências para datas futuras.");
            }
            
            if (ano == dataHoje.Year && mes >= dataHoje.Month){
                throw new ValidationException("Não é possível gerar pendências para o mês atual ou datas futuras.");
            }
        }

        public override bool VerificarSeExisteDebitoDoOrgao(Orgao orgao, DadosMonitoramentoDTO dadosMonitoramentoDTO)
        {
            

            InformacaoEnviada informacaoOpcionalidade = orgao.InformacoesEnviadas.FindLast(x => x.ObrigacaoTipoEnvio == EObrigacaoTipoEnvio.ENVIO_OPCIONALIDADE_RGF);
            Remessa remessaOpcionalidade = informacaoOpcionalidade != null ? informacaoOpcionalidade.Remessa : null;

            InformacaoEnviada informacaoAnexo15 = orgao.InformacoesEnviadas.FindLast(x => x.ObrigacaoTipoEnvio == EObrigacaoTipoEnvio.ENVIO_ANEXO_15);
            Remessa remessaAnexo15 = informacaoOpcionalidade != null ? informacaoOpcionalidade.Remessa : null;

            if((remessaOpcionalidade == null || remessaOpcionalidade.idTipoPeriodoRGF == (int) ETipoPeriodicidade.QUADRIMESTRE) && dadosMonitoramentoDTO.Mes != 6){
                if(remessaAnexo15 == null || remessaAnexo15.DataInclusao > this.GetDataVencimento(dadosMonitoramentoDTO.Mes, dadosMonitoramentoDTO.Ano)){
                        return true;
                }
                return false;
            } else if((remessaOpcionalidade != null && remessaOpcionalidade.idTipoPeriodoRGF == (int) ETipoPeriodicidade.SEMESTRE) && dadosMonitoramentoDTO.Mes % 6 == 0){
                if(remessaAnexo15 == null || remessaAnexo15.DataInclusao > this.GetDataVencimento(dadosMonitoramentoDTO.Mes, dadosMonitoramentoDTO.Ano)){
                        return true;
                }
                return false;
            }

            return false;
        }
    }
}