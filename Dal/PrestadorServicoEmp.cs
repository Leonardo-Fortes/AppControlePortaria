using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPortariaControle.Dal
{
    [Table("PrestadorServicoEmp")]
    public class PrestadorServicoEmp
    {
        [Column("ID")]
        public int ID { get; set; }

        [Column("NomeEmp")]
        public string? NomeEmp { get; set; }

        [Column("CNPJ")]
        public string? CNPJ { get; set; }

        public ICollection<PrestadorServicoFunc> PrestadorServicoFuncs { get; set; }
    }
}
