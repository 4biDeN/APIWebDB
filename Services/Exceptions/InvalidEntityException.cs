using System;

namespace apiWebDB.Services.Exceptions

{
    public class InvalidEntityException : Exception
    {
        public InvalidEntityException(string message) : base(message) { }
    }
}
