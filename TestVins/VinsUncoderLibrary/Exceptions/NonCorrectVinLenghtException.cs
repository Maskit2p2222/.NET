using System;
using System.Runtime.Serialization;

namespace VinsUncoderLibrary
{
    [Serializable]
    internal class NonCorrectVinLenghtException : Exception
    {
        public NonCorrectVinLenghtException()
        {
        }

        public NonCorrectVinLenghtException(string message) : base(message)
        {
        }

        public NonCorrectVinLenghtException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NonCorrectVinLenghtException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}