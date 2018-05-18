using DesafioMundipagg.Models;
using DesafioMundipagg.Services;
using Moq;
using NUnit.Framework;


namespace DesafioMundipaggTests
{
    [TestFixture]
    public class TemplateServiceTest
    {
        private string xml = "<body><test>teste</test></body>";
        private string json = "{\"cities\":[]}";


        private TemplateService GetDefaultService()
        {
            var mockRepo = new Mock<IMessagingRepository>();
            return new TemplateService(mockRepo.Object);
        }

        [Test]
        public void TestGetTemplateOk()
        {
            var id = "RJ";
            var content = @"
---
input: xml
codigo: 0001
listaCidades: cidades
cidade: cidade
nomeCidade: nome
habitantesCidade: habitantesC
listaBairros: bairros
bairro: bairro
nomeBairro: nomeB
regiaoBairro: regiao
habitantesBairro: habitantesB
";

            var mockRepo = new Mock<IMessagingRepository>();
            var state = new State() { Id = 1, StateCode = "RJ", TemplateCode = "T001" };
            var template = new Template() { Id = 1, Code = "T001", Content = content, Type = Template.Types.JSON };
            mockRepo.Setup(ctx => ctx.GetStateByIdentifier(It.IsAny<string>()))
    .Returns(state);
            mockRepo.Setup(ctx => ctx.GetTemplateByCode(It.IsAny<string>()))
    .Returns(template);
            var service = new TemplateService(mockRepo.Object);

            var t = service.GetTemplate(id);

            Assert.NotNull(t);
            Assert.That(t.Input == "xml");
            Assert.That(t.Codigo == "0001");
            Assert.That(t.ListaCidades == "cidades");
            Assert.That(t.NomeCidade == "nome");
            Assert.That(t.HabitantesCidade == "habitantesC");
            Assert.That(t.ListaBairros == "bairros");
            Assert.That(t.Bairro == "bairro");
            Assert.That(t.NomeBairro == "nomeB");
            Assert.That(t.RegiaoBairro == "regiao");
            Assert.That(t.HabitantesBairro == "habitantesB");
        }
    }
}
