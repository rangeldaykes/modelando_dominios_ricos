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
        public void StudentTest()
        {
            var name = new Name("Teste", "Teste");
            foreach (var not in name.Notifications)
            {
                System.Console.WriteLine(not.Property);
                System.Console.WriteLine(not.Message);
            }            
        }
    }
}