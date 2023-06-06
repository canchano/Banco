using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiAplication.Domain.DTO
{
    [Table("logs")]
    public class Log
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("fecha")]
        public DateTime Fecha { get; set; }
        [Column("descripcion")]
        public string Descripcion { get; set; } = string.Empty;
    }
}

