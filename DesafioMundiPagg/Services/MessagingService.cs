using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioMundiPagg.Services
{
    public class MessagingService : IMessagingService
    {

        public string ProcessMessage(string identifier, string content)
        {
            return "string";
        }


        private string parseXML(string content)
        {
            return "string";
        }

        private string parseJSON(string content)
        {
            return "string";
        }

        private string buildResult(string content)
        {
            return "string";
        }




        public bool TryParse(string content)
        {
            if (content == null)
            {
                return false;
            }
            return true;
        }

    }
}
