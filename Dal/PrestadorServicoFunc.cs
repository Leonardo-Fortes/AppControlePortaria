using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPortariaControle.Dal
{
    [Table("PrestadorServicoFunc")]
    public class PrestadorServicoFunc
    {
        [Column("ID")]
        [Key]
        public int ID { get; set; }

        [Column("NomeFunc")]
        public string? NomeFunc{ get; set; }

        [Column("CPF")]
        public string? CPF { get; set; }

        [Column("RG")]
        public string? RG { get; set; }

        [ForeignKey("PrestadorServicoEmp")]
        [Column("ID_Emp")]
        public int ID_Emp { get; set; }

        public PrestadorServicoEmp? PrestadorServicoEmp { get; set; }


    }
}
