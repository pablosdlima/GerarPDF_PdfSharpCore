using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using relatorios.Models;

namespace relatorios.Data
{
    public class relatoriosContext : DbContext
    {
        public relatoriosContext (DbContextOptions<relatoriosContext> options)
            : base(options)
        {
        }

        public DbSet<relatorios.Models.Teste> Teste { get; set; }
    }
}
