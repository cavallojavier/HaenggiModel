using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using HaenggiModel.CalculationHelper.Cache;
using HaenggiModel.CalculationHelper.Resources;

namespace HaenggiModel.CalculationHelper.CalculationTables
{
    public static class PontTable
    {
        private const string PontFileName = "PontReferences.txt";

        /// <summary>
        /// Finds the moyer superior value.
        /// </summary>
        /// <param name="referenceValue">The reference value.</param>
        /// <returns></returns>
        public static decimal? FindPontValue(decimal referenceValue)
        {
            try
            {
                var table = GetPontTable();
                var closest = table.OrderBy(item => Math.Abs(referenceValue - item.Item1)).First();

                if(Math.Abs(referenceValue - closest.Item1) > 1)
                {
                    return null;
                }

                return closest.Item2;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the moyers superior.
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<Tuple<decimal, decimal>> GetPontTable()
        {
            List<Tuple<decimal, decimal>> result;

            if (CacheHelper.Get(CacheKey.PontTableKey, out result))
            {
                return result;
            }

            result = new List<Tuple<decimal, decimal>>();

            using (var reader = new StreamReader(new MemoryStream(ResourceFiles.PontReferences)))
            {
                string line;
                while ((line = reader.ReadLine()) != null && !string.IsNullOrEmpty(line))
                {
                    var items = line.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                    var item1 = decimal.Parse(items[0].Trim(), CultureInfo.InvariantCulture);
                    var item2 = decimal.Parse(items[1].Trim(), CultureInfo.InvariantCulture);

                    result.Add(new Tuple<decimal, decimal>(item1, item2));
                }
            }

            CacheHelper.Add(result, CacheKey.PontTableKey);

            return result;
        }
    }
}
