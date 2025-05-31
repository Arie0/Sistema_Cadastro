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
        public CadastroContext() : base(@"data source=Ariele\sqlexpress;initial catalog=CAD;integrated security=True;encrypt=False;")
        {
            Database.SetInitializer<CadastroContext>(null);
        }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

    }
    }

