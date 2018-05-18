using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioMundipagg.Models
{
    public class MessagingRepository : IMessagingRepository
    {
        private readonly TemplateContext _context;

        public MessagingRepository(TemplateContext context)
        {
            _context = context;
        }

        public State GetStateByIdentifier(string id)
        {
            return _context.States.SingleOrDefault(x => x.StateCode.ToLower() == id.ToLower());
        }

        public Template GetTemplateByCode(string templateCode)
        {
            return _context.Templates.SingleOrDefault(x => x.Code == templateCode);
        }


    }
}
