using DesafioMundiPagg.Services;
using NUnit.Framework;
using System;


namespace DesafioMundPagg_Tests
{
    [TestFixture]
    public class MessagingServiceTest
    {
        private readonly MessagingService _service;
        private string xml = "<body><test>teste</test></body>";
        private string json = "{\"cities\":[]}";

        public MessagingServiceTest()
        {
            _service = new MessagingService();
        }

        [Test]
        public void TestValidateWithStringNull()
        {
            var result = _service.ValidateInput(null);

            Assert.False(result);
        }

        [Test]
        public void TestValidateWithValidString()
        {
            var result = _service.ValidateInput(xml);

            Assert.True(result);
        }


    }
}
