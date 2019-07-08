using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;

namespace PaymentContext.Tests
{
    [TestClass]
    public class StudentTests
    {

        [TestMethod]
        public void StudentTest()
        {
            var subscription = new Subscription(null);
            var student = new Student("José");

            
        }
    }
}