using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.DTO;
using Domain.Enum;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class ObrigacaoSiaiFiscalRREO : Obrigacao
    {
        public ObrigacaoSiaiFiscalRREO(byte tipoObrigacao, 
                             string nomeObrigacao, 
                             List<HistoricoPeriodicidade> listaHistoricoPeriodicidade, 
                             List<HistoricoValorMulta> listaHistoricoValorMulta, 
                             List<Rota> rotas) : base(tipoObrigacao, nomeObrigacao, listaHistoricoPeriodicidade, listaHistoricoValorMulta, rotas)
        {
        }

        public override bool VerificarSeExisteDebitoDoOrgao(Orgao orgao, DadosMonitoramentoDTO dadosMonitoramentoDTO)
        {
            InformacaoEnviada informacaoEnviada = orgao.InformacoesEnviadas.FindLast(x => x.ObrigacaoTipoEnvio == EObrigacaoTipoEnvio.ENVIO_ANEXO_01);
            Remessa remessaAnexo01 = informacaoEnviada != null ? informacaoEnviada.Remessa : null;
            
            if(remessaAnexo01 == null || remessaAnexo01.DataInclusao > this.GetDataVencimento(dadosMonitoramentoDTO.Mes, dadosMonitoramentoDTO.Ano)){
                return true;
            }
            return false;
        }

        public override void ValidarMonitoramento(int mes, int ano) {
            var dataHoje = DateTime.Now;
            
            if(mes % 2 != 0){
                throw new ValidationException("Não há pendências a serem geradas para este mês.");
            }
            if(ano > dataHoje.Year){
                throw new ValidationException("Não é possível gerar pendências para datas futuras.");
            }
            
            if (ano == dataHoje.Year && mes >= dataHoje.Month){
                throw new ValidationException("Não é possível gerar pendências para o mês atual ou datas futuras.");
            }
        }
    }
}