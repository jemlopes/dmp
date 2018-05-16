using DesafioMundiPagg.Services;
using NUnit.Framework;
using System;
using Moq;
using DesafioMundiPagg.Models;
using DesafioMundiPagg.Controllers;
using System.Collections.Generic;

namespace DesafioMundPagg_Tests
{
    [TestFixture]
    public class APIControllerTest
    {
        private string xml = "<body><test>teste</test></body>";

        [Test]
        public void TestTranslateValido()
        {
            // Arrange
            var mockService = new Mock<IMessagingService>();
            var mockResult = preparaResultValido();
            mockService.Setup(srv => srv.ProcessMessage(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(mockResult);
            mockService.Setup(srv => srv.ValidateInput(It.IsAny<string>()))
                .Returns(true);

            var controller = new APIController(mockService.Object);
            var result = controller.Translate("RJ", xml);

            Assert.That( result.GetType() == typeof(Microsoft.AspNetCore.Mvc.OkObjectResult));

        }

        [Test]
        public void TestTranslateRequestInvalido()
        {
            // Arrange
            var mockService = new Mock<IMessagingService>();
            mockService.Setup(srv => srv.ValidateInput(It.IsAny<string>()))
                .Returns(false);

            var controller = new APIController(mockService.Object);
            var result = controller.Translate("RJ", xml);

            Assert.That(result.GetType() == typeof(Microsoft.AspNetCore.Mvc.BadRequestObjectResult));
        }

        [Test]
        public void TestTranslateArquivoInvalido()
        {
            // Arrange
            var mockService = new Mock<IMessagingService>();
            var mockResult = preparaResultValido();
            mockService.Setup(srv => srv.ProcessMessage(It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new InvalidCastException());

            mockService.Setup(srv => srv.ValidateInput(It.IsAny<string>()))
                .Returns(true);

            var controller = new APIController(mockService.Object);
            var result = controller.Translate("RJ", xml);

            Assert.That(result.GetType() == typeof(Microsoft.AspNetCore.Mvc.BadRequestObjectResult));
        }

        private Result preparaResultValido()
        {
            var result = new Result();
            result.Cidades = new List<Cidade>();

            var cidade = new Cidade() { Habitantes = 10000, Nome = "Rio de Janeiro" };
            cidade.Bairros = new List<Bairro>();
            cidade.Bairros.Add(new Bairro() { Nome = "Centro", Habitantes = 60000 });
            cidade.Bairros.Add(new Bairro() { Nome = "Botafogo", Habitantes = 10000 });
            cidade.Bairros.Add(new Bairro() { Nome = "Tijuca", Habitantes = 45000 });

            result.Cidades.Add(cidade);

            return result;
        }

    }
}
