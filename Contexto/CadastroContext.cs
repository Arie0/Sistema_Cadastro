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
        public CadastroContext() : base(@"data source=Ariele\sqltest;initial catalog=sistema;integrated security=True;encrypt=False;")
        {
            Database.SetInitializer<CadastroContext>(null);
        }
        public DbSet<Produtos> Produtos { get; set; }
        public DbSet<Fornecedores> Fornecedor { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

    }
    }

