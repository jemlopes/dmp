using DesafioMundipagg.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DesafioMundipagg.Services
{
    public class MessagingService : IMessagingService
    {
        private readonly ITemplateService _service;

        public MessagingService(ITemplateService service)
        {
            _service = service;
        }

        public Result ProcessMessage(string identifier, string content)
        {
            try
            {
                var template = _service.GetTemplate(identifier);
                switch (template.Input.ToLower())
                {
                    case "json":
                        return ParseJSON(content, template);
                    case "xml":
                        return ParseXML(content, template);
                    default:
                        throw new NotImplementedException("Tipo de arquivo nao suportado");
                }
            }
            catch (YamlDotNet.Core.SyntaxErrorException ex)
            {
                throw ex;
            }
        }

        public string SerializeResult(Result result)
        {

            return JsonConvert.SerializeObject(result, Formatting.Indented);
        }


        private Result ParseXML(string content, TemplateDTO t)
        {
            var result = new Result();
            var root = XElement.Parse(content);

            foreach(var xc in root.Descendants(t.Cidade))
            {
                var c = new Cidade() {
                    Habitantes = (int) xc.Element(t.HabitantesCidade),
                    Nome = ((string) xc.Element(t.NomeCidade)).Trim()
                };

                foreach (var xb in xc.Descendants(t.Bairro))
                {
                    c.Bairros.Add(new Bairro() { Habitantes = (int) xb.Element(t.HabitantesBairro),
                        Nome = ((string)xb.Element(t.NomeBairro)).Trim() });
                }

                result.Cidades.Add(c);
            }
            return result;
        }

        private Result ParseJSON(string content, TemplateDTO t)
        {
            var result = new Result();


            IList<JProperty> jcidades = null;

            JObject o = JObject.Parse(content);
            jcidades = o.Descendants().
                Where(tk => tk.Type == JTokenType.Property && ((JProperty)tk).Name == t.ListaCidades).
                Select(x => (JProperty)x).ToList();
            var cidades = new List<Cidade>();
            foreach (var jct in jcidades)
            {
                var jc = jct.Value.ToObject<JArray>()[0];


                var c = new Cidade() { Habitantes = (int)jc[t.HabitantesCidade], Nome = (string)jc[t.NomeCidade] };
                var jbairros = jc[t.ListaBairros].Select(x => (JObject)x).ToList();
                foreach (var jb in jbairros)
                {
                    c.Bairros.Add(new Bairro() { Habitantes = (int)jb[t.HabitantesBairro], Nome = (string)jb[t.NomeBairro] });
                }
                result.Cidades.Add(c);
            }
            return result;
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
