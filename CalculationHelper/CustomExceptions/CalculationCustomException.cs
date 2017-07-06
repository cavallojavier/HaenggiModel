using System;

namespace CalculationHelper.CustomExceptions
{
    public class CalculationCustomException : ApplicationException
    {
        public CalculationCustomException(string message, Exception exception)
            : base(message, exception)
        {
        }
    }
}
