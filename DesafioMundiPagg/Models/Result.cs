using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioMundipagg.Models
{
    public class Result
    {
        [JsonProperty("result")]
        public List<Cidade> Cidades { get; set; }

        public Result()
        {
            Cidades = new List<Cidade>();
        }
    }

    public class Cidade
    {
        
        public Cidade()
        {
            Bairros = new List<Bairro>();
        }
        [JsonProperty("cidade")]
        public string Nome { get; set; }
        [JsonProperty("habitantes")]
        public int Habitantes { get; set; }
        [JsonProperty("bairros")]
        public List<Bairro> Bairros { get; set; }
    }

    public class Bairro
    {
        [JsonProperty("nome")]
        public string Nome { get; set; }
        [JsonProperty("habitantes")]
        public int Habitantes { get; set; }
    }
}
