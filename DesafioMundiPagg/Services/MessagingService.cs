using DesafioMundiPagg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioMundiPagg.Services
{
    public class MessagingService : IMessagingService
    {

        public Result ProcessMessage(string identifier, string content)
        {
            Result result = null;
            //Define tipo de request

            try
            {


            } catch (InvalidCastException ex)
            {
                throw ex;
            }

            return result;
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

        public bool ValidateInput(string content)
        {
            if (content == null)
            {
                return false;
            }
            return true;
        }

     }
}
