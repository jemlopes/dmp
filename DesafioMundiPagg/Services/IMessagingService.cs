using DesafioMundipagg.Models;

namespace DesafioMundipagg.Services
{
    public interface IMessagingService
    {
        Result ProcessMessage(string identifier, string content);
        bool ValidateInput(string content);
    }
}