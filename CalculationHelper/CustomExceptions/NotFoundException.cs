using System;

namespace CalculationHelper.CustomExceptions
{
    public class ElementNotFoundException: ApplicationException
    {
        public ElementNotFoundException(string message, Exception exception)
            : base(message, exception)
        {
        }
    }
}
