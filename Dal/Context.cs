using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AppPortariaControle.Dal.Context;

namespace AppPortariaControle.Dal
{
    public class Context : DbContext
    {   

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Controle_Portaria;Data Source=PROG06-KALPA; TrustServerCertificate=True;");
        }
        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<AcessoInterno> AcessoInternos { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }
        

    }
        
}
