using DesafioMundipagg.Models;
using DesafioMundipagg.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;


namespace DesafioMundipaggTests
{
    [TestFixture]
    public class MessagingServiceTest
    {
        private string xml = "<body><test>teste</test></body>";
        private string json = "{\"cities\":[]}";


        private MessagingService GetDefaultService()
        {
            var mock = new Mock<ITemplateService>();
            return new MessagingService(mock.Object);
        }

        [Test]
        public void TestValidateWithStringNull()
        {
            var result = GetDefaultService().ValidateInput(null);

            Assert.False(result);
        }

        [Test]
        public void TestValidateWithValidString()
        {
            var result = GetDefaultService().ValidateInput(xml);

            Assert.True(result);
        }


        [Test]
        public void TestProcessJsonMessage()
        {
            var json = @"
{
    'cidades':[ 
        {
        'nome': 'Rio Branco', 
        'populacao':576589,
        'bairros':[
                {
                    'nome': 'Habitasa',
                    'populacao':7503
                },
                {
                    'nome': 'Bairro 2',
                    'populacao':2503
                },
            ]
        }
    ],
    'teste': {
        'cidades':[ { 'nome': 'teste' , 'populacao' :1000 , 'bairros': [{'nome': 'b1' , 'populacao':1}]} ],
    }
 }
";
            ValidateResultJson(TestServiceJsonMessage(json, "cidades"));
        }


        [Test]
        public void TestProcessXmlMessage()
        {
            var xml = @"
<corpo> 
   <cidade> 
       <nome> Rio de Janeiro</nome>
       <populacao>10345678</populacao>
       <bairros>
           <bairro> 
               <nome> Tijuca</nome>
               <regiao>Zona Norte</regiao>
               <populacao >135678</populacao>
           </bairro>
           <bairro>
               <nome> Botafogo</nome>
               <regiao>Zona Sul</regiao>
               <populacao>105711</populacao>
           </bairro>
       </bairros> 
   </cidade> 
   <cidade> 
       <nome>Nova Friburgo</nome>
       <populacao>1034678</populacao>
       <bairros>
           <bairro> 
               <nome>Centro</nome>
               <populacao >13578</populacao>
           </bairro>
       </bairros> 
   </cidade> 
</corpo> 
";



            ValidateResultXml(TestServiceXmlMessage(xml, "cidades"));
        }

        [Test]
        public void TestResultSerialization()
        {
            var xml = @"
<corpo> 
   <cidade> 
       <nome> Rio de Janeiro</nome>
       <populacao>10345678</populacao>
       <bairros>
           <bairro> 
               <nome> Tijuca</nome>
               <regiao>Zona Norte</regiao>
               <populacao >135678</populacao>
           </bairro>
           <bairro>
               <nome> Botafogo</nome>
               <regiao>Zona Sul</regiao>
               <populacao>105711</populacao>
           </bairro>
       </bairros> 
   </cidade> 
   <cidade> 
       <nome>Nova Friburgo</nome>
       <populacao>1034678</populacao>
       <bairros>
           <bairro> 
               <nome>Centro</nome>
               <populacao >13578</populacao>
           </bairro>
       </bairros> 
   </cidade> 
</corpo> 
";
            var mock = new Mock<ITemplateService>();
            var t = new TemplateDTO() { Bairro = "bairro", HabitantesBairro = "populacao", HabitantesCidade = "populacao", Input = "xml", Codigo = "0001", Cidade = "cidade", ListaBairros = "bairros", ListaCidades = "cidades", NomeBairro = "nome", NomeCidade = "nome", RegiaoBairro = "regiao" };
            mock.Setup(s => s.GetTemplate(It.IsAny<string>()))
    .Returns(t);
            var service = new MessagingService(mock.Object);
            var r =  service.ProcessMessage("RJ", xml);
            var json = service.SerializeResult(r);

            Assert.NotNull(json);
        }

        private Result TestServiceXmlMessage(string xml, string listaCidades)
        {

            var mock = new Mock<ITemplateService>();
            var t = new TemplateDTO() { Bairro = "bairro", HabitantesBairro = "populacao", HabitantesCidade = "populacao", Input = "xml", Codigo = "0001", Cidade = "cidade", ListaBairros = "bairros", ListaCidades = listaCidades, NomeBairro = "nome", NomeCidade = "nome", RegiaoBairro = "regiao" };
            mock.Setup(s => s.GetTemplate(It.IsAny<string>()))
    .Returns(t);
            var service = new MessagingService(mock.Object);

            return service.ProcessMessage("RJ", xml);
        }



        private Result TestServiceJsonMessage(string json, string listaCidades)
        {

            var mock = new Mock<ITemplateService>();
            var t = new TemplateDTO() { Bairro = "bairro", HabitantesBairro = "populacao", HabitantesCidade = "populacao", Input = "json", Codigo = "0001", ListaBairros = "bairros", ListaCidades = listaCidades, NomeBairro = "nome", NomeCidade = "nome", RegiaoBairro = "regiao" };
            mock.Setup(s => s.GetTemplate(It.IsAny<string>()))
    .Returns(t);
            var service = new MessagingService(mock.Object);

            return service.ProcessMessage("AC", json);
        }


        private void ValidateResultJson(Result r)
        {
            Assert.NotNull(r);
            Assert.That(r.Cidades.Count == 2);
            Assert.That(r.Cidades[0].Nome == "Rio Branco");
            Assert.That(r.Cidades[0].Habitantes == 576589);
            Assert.That(r.Cidades[0].Bairros.Count == 2);
            Assert.That(r.Cidades[0].Bairros[0].Nome == "Habitasa");
            Assert.That(r.Cidades[0].Bairros[0].Habitantes == 7503);
            Assert.That(r.Cidades[0].Bairros[1].Nome == "Bairro 2");
            Assert.That(r.Cidades[0].Bairros[1].Habitantes == 2503);
        }

        private void ValidateResultXml(Result r)
        {
            Assert.NotNull(r);
            Assert.That(r.Cidades.Count == 2);
            Assert.That(r.Cidades[0].Nome == "Rio de Janeiro");
            Assert.That(r.Cidades[0].Habitantes == 10345678);
            Assert.That(r.Cidades[0].Bairros.Count == 2);
            Assert.That(r.Cidades[0].Bairros[0].Nome == "Tijuca");
            Assert.That(r.Cidades[0].Bairros[0].Habitantes == 135678);
            Assert.That(r.Cidades[0].Bairros[1].Nome == "Botafogo");
            Assert.That(r.Cidades[0].Bairros[1].Habitantes == 105711);
        }

    }
}


