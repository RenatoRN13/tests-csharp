using System.Reflection.Emit;
using System.Collections.Generic;
using System;
using Domain.Enum;
using Domain.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Orgao
    {
        public int IdOrgao { get; set; }
        public int? IdOrgaoSuperior { get; set; }
        public string CodigoOrgao { get; set; }
        public string NomeOrgao { get; set; }
        public string SiglaOrgao { get; set; }
        public string Cnpj { get; set; }
        public bool OrgaoAtivo { get; set; }
        public virtual List<Orgao> OrgaosSubornidados { get; set; }
        
        [NotMapped]
        public virtual List<InformacaoEnviada> InformacoesEnviadas { get; private set; }

        public void SetInformacoesEnviadas(EObrigacaoTipoEnvio eObrigacaoTipoEnvio, List<Remessa> remessas){
            if(this.InformacoesEnviadas == null)
                this.InformacoesEnviadas = new List<InformacaoEnviada>();
            foreach (Remessa remessa in remessas) {
                if(this.IdOrgao == remessa.IdOrgao || (remessa.CodigoOrgao != null && this.CodigoOrgao.Trim().Equals(remessa.CodigoOrgao.Trim()))){
                    this.InformacoesEnviadas.Add(new InformacaoEnviada(eObrigacaoTipoEnvio, remessa));
                }
            }
        }
    }
}