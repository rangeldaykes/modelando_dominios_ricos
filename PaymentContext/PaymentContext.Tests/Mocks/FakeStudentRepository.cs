using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Repositories;

namespace PaymentContext.Tests.Mocks
{
    public class FakeStudntRepository : IStudentRepository
    {
        public void CreateSubscription(Student student)
        {
            
        }

        public bool DocumentExists(string document)
        {
            if (document == "12345678999")
                return true;

            return false;
        }

        public bool EmailExists(string email)
        {
            if (email == "hello@hello.com")
                return true;

            return false;
        }
    }
}