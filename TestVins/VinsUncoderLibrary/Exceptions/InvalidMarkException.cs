using System;
using System.Runtime.Serialization;

namespace VinsUncoderLibrary
{
    [Serializable]
    internal class InvalidMarkException : Exception
    {
        public InvalidMarkException()
        {
        }

        public InvalidMarkException(string message) : base(message)
        {
        }

        public InvalidMarkException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidMarkException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}