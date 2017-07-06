using System;

namespace HaenggiModel.CalculationHelper.Calculators
{
    public static class CalculationBase
    {
        /// <summary>
        /// Rounds the number.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static decimal RoundNumber(decimal value)
        {
            return Math.Round(value, 2);
        }

        /// <summary>
        /// Rounds up result.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public static decimal RoundUpResult(decimal length)
        {
            //If result is less than 2ml, then return 0
            if (length < 2)
                return 0;

            //scenarios
            /*
             val = 5.10 - expected 5.0
             val = 5.36 - expected 5.5
             val = 5.70 - expected 6
            */
            var multipliedValue = (length * 2);
            var roundedResult = Math.Round(multipliedValue, MidpointRounding.AwayFromZero);
            return roundedResult / 2;
        }
    }
}
