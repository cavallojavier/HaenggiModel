using System;

namespace HaenggiModel.CalculationHelper.CustomExceptions
{
    public class ElementNotFoundException: ApplicationException
    {
        public ElementNotFoundException(string message, Exception exception)
            : base(message, exception)
        {
        }
    }
}
