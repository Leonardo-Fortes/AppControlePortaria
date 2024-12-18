using AppPortariaControle.Enums;
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
    [Table("AcessosInterno")]
    public class AcessoInterno
    {
        [Column("ID")]
        [Key]
        public int ID { get; set; }


        [Column("Status")]
        public EStatus Status { get; set; } = EStatus.Saida;

        [Column("Entrada")]
        public DateTime Entrada { get; set; } 

        [Column("Saida")]
        public DateTime? Saida { get; set; }

        [Column("ResponsavelEntrada")]
        public string ResponsavelControleEntrada { get; set; } = string.Empty;

        [Column("ResponsavelSaida")]
        public string? ResponsavelControleSaida { get; set; }

        [ForeignKey("Veiculo")]
        [Column("Id_Veiculos")]
        public int Id_Veiculos { get; set; }

        public Veiculo? Veiculo { get; set; }


    }
}
