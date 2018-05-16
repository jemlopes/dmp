using DesafioMundiPagg.Models;

namespace DesafioMundiPagg.Services
{
    public interface IMessagingService
    {
        Result ProcessMessage(string identifier, string content);
        bool ValidateInput(string content);
    }
}