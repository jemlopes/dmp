using DesafioMundipagg.Models;

namespace DesafioMundipagg.Services
{
    public interface ITemplateService
    {
        TemplateDTO GetTemplate(string identifier);
    }
}