using System;
using System.Collections.Generic;
using System.Linq;
using HaenggiModel.CalculationHelper.Cache;

namespace HaenggiModel.CalculationHelper.CalculationTables
{
    public static class MoyersTable
    {
        private const string MoyersFileName = "MoyersReference.txt";

        /// <summary>
        /// Finds the moyer superior value.
        /// </summary>
        /// <param name="referenceValue">The reference value.</param>
        /// <returns></returns>
        public static decimal? FindMoyerSuperiorValue(decimal referenceValue)
        {
            try
            {
                var table = GetMoyersTable();
                var closest = table.OrderBy(item => Math.Abs(referenceValue - item.Item1)).First();

                if (Math.Abs(referenceValue - closest.Item1) > 1)
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
        /// Finds the moyer inferior value.
        /// </summary>
        /// <param name="referenceValue">The reference value.</param>
        /// <returns></returns>
        public static decimal? FindMoyerInferiorValue(decimal referenceValue)
        {
            try
            {
                var table = GetMoyersTable();
                var closest = table.OrderBy(item => Math.Abs(referenceValue - item.Item3)).First();

                if (Math.Abs(referenceValue - closest.Item3) > 1)
                {
                    return null;
                }

                return closest.Item4;
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
        private static IEnumerable<Tuple<decimal, decimal, decimal, decimal>> GetMoyersTable()
        {
            List<Tuple<decimal, decimal, decimal, decimal>> result;

            if (CacheHelper.Get(CacheKey.MoyersTableKey, out result))
            {
                return result;
            }

            result = new List<Tuple<decimal, decimal, decimal, decimal>>();
            var references = ReadFunctions.ReadFile(MoyersFileName);
            
            using (var reader = references)
            {
                string line;
                while ((line = reader.ReadLine()) != null && !string.IsNullOrEmpty(line))
                {
                    var items = line.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    
                    var item1 = decimal.Parse(items[0].Trim());
                    var item2 = decimal.Parse(items[1].Trim());
                    var item3 = decimal.Parse(items[2].Trim());
                    var item4 = decimal.Parse(items[3].Trim());

                    result.Add(new Tuple<decimal, decimal, decimal, decimal>(item1, item2, item3, item4));
                }
            }

            CacheHelper.Add(result, CacheKey.MoyersTableKey);

            return result;
        }
    }
}
