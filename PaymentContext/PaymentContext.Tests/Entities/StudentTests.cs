using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests
{
    [TestClass]
    public class StudentTests
    {

        [TestMethod]
        public void ShouldReturnErrorWhenHadActiveSubscription()
        {
            var name = new Name("José", "lélé");
            var document = new Document("35111507795", Domain.Enums.EDocumentType.CPF);
            var email = new Email("zelele@fazenda.com");
            var address = new Address("Rua 1", "123", "bairro", "cidade", "PR", "BR", "13400000");
            var student = new Student(name, document, email);
            var subscription = new Subscription(null);
            var payment = new PayPalPayment(
                "12345678", 
                DateTime.Now, 
                DateTime.Now.AddDays(5), 
                10, 
                10, 
                "fazendas", 
                document,
                address,
                email);

            

            Assert.Fail();
        }

        [TestMethod]
        public void ShouldReturnErrorWhenHadSubscriptionHasNoPayment()
        {
            var name = new Name("José", "lélé");
            var document = new Document("35111507795", Domain.Enums.EDocumentType.CPF);
            var email = new Email("zelele@fazenda.com");
            var student = new Student(name, document, email);
            Assert.Fail();
        }


        [TestMethod]
        public void ShouldReturnSuccessWhenHadActiveSubscription()
        {
            Assert.Fail();
        }
    }
}