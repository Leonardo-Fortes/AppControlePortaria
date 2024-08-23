using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPortariaControle.Dal
{
    [Table("AcessoInterno")]
    public class AcessoInterno
    {
        [Column("ID")]
        [Key]
        public int ID { get; set; }

        [Column("Placa")]
        public string? Placa { get; set; }
        [Column("Tipo")]
        public string? Tipo { get; set; }
        [Column("Modelo")]
        public string? Modelo { get; set; }
        [Column("Motorista")]
        public string? Motorista { get; set; }
        [Column("Mes")]
        public string? Mes { get; set; }

        [Column("Entrada")]
        public DateTime Entrada { get; set; } 

        [Column("Saida")]
        public DateTime? Saida { get; set; }

        [Column("ResponsavelControleEntrada")]
        public string? ResponsavelControleEntrada { get; set; }

        [Column("ResponsavelControleSaida")]
        public string? ResponsavelControleSaida { get; set; }
    }
}
