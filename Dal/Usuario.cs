using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPortariaControle.Dal
{
    [Table("Usuario")]
    public class Usuario
    {
        [Column("ID")]
        [Key]
        public int ID { get; set; }
        [Column("Usuario")]
        public string? Login { get; set; }
        [Column("Senha")]
        public string? Senha { get; set; }
        [Column("Nome")]
        public string? Nome { get; set; }
        [Column("Turno")]
        public string? Turno { get; set; }
        

    }
}
