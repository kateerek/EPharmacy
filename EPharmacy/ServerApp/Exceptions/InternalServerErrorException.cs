using System;

namespace EPharmacy.ServerApp.Exceptions
{
    public class InternalServerErrorException : Exception
    {
        public string ErrorMessage { get; }

        public InternalServerErrorException(string errorMessage, string exceptionMessage)
            :base(exceptionMessage)
        {
            ErrorMessage = errorMessage;
        }

        public InternalServerErrorException(string errorMessage, string exceptionMessage, Exception innerException)
            : base(exceptionMessage, innerException)
        {
            ErrorMessage = errorMessage;
        }
    }
}
