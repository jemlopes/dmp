namespace DesafioMundipagg.Models
{
    public interface IMessagingRepository
    {
        State GetStateByIdentifier(string id);
        Template GetTemplateByCode(string templateCode);
    }
}