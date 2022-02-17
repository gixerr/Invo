using System;

namespace Invo.Shared.Abstractions.Exceptions
{
    public abstract class InvoException : Exception
    {
        public InvoException(string message) : base(message)
        {
        }
    }
}