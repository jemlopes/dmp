using Microsoft.EntityFrameworkCore;

namespace DesafioMundipagg.Models
{
    public interface ITemplateContext
    {
        DbSet<State> States { get; set; }
        DbSet<Template> Templates { get; set; }

        State GetStateByIdentifier(string id);
        Template GetTemplateByCode(string templateCode);
    }
}