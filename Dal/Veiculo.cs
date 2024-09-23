using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
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
        public string? Placa { get; set; }
        [Column("Tipo")]
        public string? Tipo { get; set; }
        [Column("Modelo")]
        public string? Modelo { get; set; }
        [Column("Motorista")]
        public string? Motorista { get; set; }

        [ForeignKey("Funcionario")]
        [Column("ID_Func")]
        public int IDFunc { get; set; }

        public Funcionario? Funcionario { get; set; }

    }
}
