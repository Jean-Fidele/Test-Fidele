using System;

namespace Test_Fidele.Systems
{
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message)
        {
        }
    }
}