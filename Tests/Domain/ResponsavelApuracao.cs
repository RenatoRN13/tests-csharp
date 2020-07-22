using System.Threading.Tasks.Dataflow;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using Domain.ViewEntities;

namespace Domain.Entities
{
    public class ResponsavelApuracao
    {
        public ResponsavelApuracao() { }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdResponsavelApuracao { get; set; }
        public int IdGrupoUnidadeJurisdicionada { get; set; }
        public int IdObrigacao { get; set; }
        public int IdSetor { get; set; }
        public int IdSessao { get; set; }
        public int? IdSessaoOperacao { get; set; }
        public DateTime? DataInativacao { get; set; }
        public DateTime DataInclusao { get; set; }
        public virtual Obrigacao Obrigacao { get; set; }
        
        [NotMapped]
        public virtual GrupoUnidadeJurisdicionada GrupoUnidadeJurisdicionada { get; private set; }

        public void SetGrupoUnidadeJurisdionada(GrupoUnidadeJurisdicionada grupoUnidadeJurisdicionada) {
            this.GrupoUnidadeJurisdicionada = grupoUnidadeJurisdicionada;
        }

    }
}