using System;
using DesafioMundipagg.Models;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace DesafioMundipagg.Services
{
    public class TemplateService : ITemplateService
    {
        private readonly IMessagingRepository _repo;

        public TemplateService(IMessagingRepository repo)
        {
            _repo = repo;
        }

        public TemplateDTO GetTemplate(string identifier)
        {
            var t = GetDbTemplateByStateCode(identifier);
            return ParseTemplate(t.Content);
        }

        private TemplateDTO ParseTemplate(string t)
        {
            var deserializer = new DeserializerBuilder()
                 .WithNamingConvention(new CamelCaseNamingConvention())
                 .Build();
            return deserializer.Deserialize<TemplateDTO>(t);
        }

        private Template GetDbTemplateByStateCode(string identifier)
        {
            Template template = null;
            var state = _repo.GetStateByIdentifier(identifier);
            if (state == null)
            {
                throw new Exception("Estado informado não é valido");
            }

            template = _repo.GetTemplateByCode(state.TemplateCode);
            if (template == null)
            {
                throw new Exception("Estado informado não é valido");
            }
            return template;
        }





    }
}
