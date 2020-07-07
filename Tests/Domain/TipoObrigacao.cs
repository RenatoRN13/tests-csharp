using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Domain.Entities
{
    public class TipoObrigacao
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
    }
}