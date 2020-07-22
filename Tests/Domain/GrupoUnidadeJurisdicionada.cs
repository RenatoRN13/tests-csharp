using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class GrupoUnidadeJurisdicionada
    {
        private GrupoUnidadeJurisdicionada() {}
        public GrupoUnidadeJurisdicionada(byte idGrupoUnidadeJurisdicionada, 
                                          List<Orgao> orgaos)
        {
            this.IdGrupoUnidadeJurisdicionada = idGrupoUnidadeJurisdicionada;

            this.Orgaos = orgaos;
        }
        public byte IdGrupoUnidadeJurisdicionada { get; set; }
        public string NomeGrupoUnidadeJurisdicionada { get; set; }
        public virtual List<Orgao> Orgaos { get; set; }
    }
}