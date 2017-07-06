using System;

namespace HaenggiModel.CalculationHelper
{
    public static class MessureResultFormatter
    {
        private static int Apot = 270;
        private static int Res = 2048;
        private static int Larm = 65;
        private static double Vpot = 3.324;
        private static double Vref = 2.024;
        private static double RadiansConverter = 0.01745;
        private static double EffectiveAngule = Apot * (Vref / Vpot);

        /// <summary>
        /// Formats the result.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static double FormatResult(string value)
        {
            int parsedValue;

            if (int.TryParse(value, out parsedValue))
            {
                return GetMessureLength(parsedValue);
            }

            return parsedValue;
        }

        /// <summary>
        /// Gets the length of the messure.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private static double GetMessureLength(int value)
        {
            var grads = value * (EffectiveAngule / Res);

            var radians = grads * RadiansConverter;

            var lenght = Larm * Math.Sin(radians / 2);

            var result = Calculators.CalculationBase.RoundUpResult((decimal)lenght);
            return Convert.ToDouble(result);
        }
    }
}
