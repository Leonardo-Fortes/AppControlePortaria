using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPortariaControle.Dal
{
    [Table("Funcionarios")]
    public class Funcionario
    {
        [Column("IDFunc")]
        [Key]
        public int ID { get; set; }
        [Column("nome")]
        public string Nome { get; set; } = string.Empty;
    }
}
