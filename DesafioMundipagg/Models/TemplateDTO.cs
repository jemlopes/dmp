using YamlDotNet.Serialization;


namespace DesafioMundipagg.Models
{
    public class TemplateDTO
    {
        public string Input { get; set; }
        public string Codigo { get; set; }
        public string ListaCidades { get; set; }
        public string Cidade { get; set; }
        public string NomeCidade { get; set; }
        public string HabitantesCidade { get; set; }
        public string ListaBairros { get; set; }
        public string Bairro { get; set; }
        public string NomeBairro { get; set; }
        public string RegiaoBairro { get; set; }
        public string HabitantesBairro { get; set; }
    }
}
