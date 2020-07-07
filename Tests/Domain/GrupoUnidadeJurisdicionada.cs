using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class GrupoUnidadeJurisdicionada
    {
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