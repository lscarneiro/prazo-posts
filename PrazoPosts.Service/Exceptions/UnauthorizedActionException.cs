using System;
namespace PrazoPosts.Service.Exceptions
{
    public class UnauthorizedActionException : Exception
    {
        public UnauthorizedActionException() : base("Opreração não permitida")
        {
        }
    }
}
