namespace DesafioMundiPagg.Services
{
    public interface IMessagingService
    {
        string ProcessMessage(string identifier, string content);
        bool TryParse(string content);
    }
}