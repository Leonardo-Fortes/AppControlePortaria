using AppPortariaControle.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AppPortariaControle.Dal
{
    [Table("Veiculo")]
    public class Veiculo
    {
        [Column("ID")]
        [Key]
        public int ID { get; set; }
        [Column("Placa")]
        
        public string Placa { get; set; } = string.Empty;
        [Column("Tipo")]
        public string Tipo { get; set; } = string.Empty;
        [Column("Modelo")]
        public string Modelo { get; set; } = string.Empty;
        [Column("Motorista")]
        public string? Motorista { get; set; } = string.Empty;

        [Column("UserAdd")]
        public string UserAdd { get; set; } = string.Empty;

        [Column("DataAdd")]
        public DateTime DataAdd { get; set; } = DateTime.UtcNow;

        [ForeignKey("Funcionario")]
        [Column("ID_Func")]
        public int IDFunc { get; set; }

        public Funcionario? Funcionario { get; set; }

    }
}
