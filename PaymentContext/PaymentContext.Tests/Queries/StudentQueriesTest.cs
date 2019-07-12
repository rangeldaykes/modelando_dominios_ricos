using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Queries;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.Handlers
{
    [TestClass]
    public class StudentQueriesTest
    {
        // Red, Green, Refactor
        private IList<Student> _students;

        public StudentQueriesTest()
        {
            for (int i = 0; i < 10; i++)
            {
                _students.Add(
                    new Student(
                        new Name("Aluno", i.ToString()),
                        new Document("1111111111" + i.ToString(), Domain.Enums.EDocumentType.CPF), 
                        new Email(i.ToString() + "@teste.com"))
                );                
            }
        }

        [TestMethod]
        public void SouldReturnNullWhenDocumentNotExists()
        {
            var exp = StudentQueries.GetStudentInfo("12345678911");
            var studn = _students.AsQueryable().Where(exp).FirstOrDefault();
            Assert.AreEqual(null, studn);
        }

        [TestMethod]
        public void SouldReturnNullWhenDocumentExists()
        {
            var exp = StudentQueries.GetStudentInfo("11111111111");
            var studn = _students.AsQueryable().Where(exp).FirstOrDefault();
            Assert.AreEqual(null, studn);
        }        
    }
}