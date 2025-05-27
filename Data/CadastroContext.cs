using proj1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proj1.Data
{
    public class CadastroContext : DbContext
    {
        public CadastroContext() : base("Server=ARIELE\\SQLEXPRESS;Database=CAD;Trusted_Connection=True;Encrypt=False") {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<CadastroContext>());
        }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Fornecedor> Fornecedor { get; set; }
         
        }
    }

