using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioMundipagg.Models
{
    public class TemplateContext : DbContext
    {
        public TemplateContext(DbContextOptions<TemplateContext> options) : base(options)
        { }

        public DbSet<Template> Templates { get; set; }
        public DbSet<State> States { get; set; }

        public State GetStateByIdentifier(string id)
        {
            return States.SingleOrDefault(x => x.StateCode.ToLower() == id.ToLower());
        }

        public Template GetTemplateByCode(string templateCode)
        {
            return Templates.SingleOrDefault(x => x.Code == templateCode);
        }
    }
}
