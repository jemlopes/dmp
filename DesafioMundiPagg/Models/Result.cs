using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioMundiPagg.Models
{
    public class Result
    {
        public List<Cidade> Cidades { get; set; }
    }

    public class Cidade
    {
        public string Nome { get; set; }
        public int Habitantes { get; set; }
        public List<Bairro> Bairros { get; set; }
    }

    public class Bairro
    {
        public string Nome { get; set; }
        public int Habitantes { get; set; }
    }
}
