using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioMundiPagg.Models;

namespace DesafioMundiPagg.Models
{
    public class TemplateContext : DbContext
    {
        public TemplateContext(DbContextOptions<TemplateContext> options) : base(options)
        { }

        public DbSet<Template> Templates { get; set; }
        public DbSet<State> State { get; set; }
    }
}
