using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using HaenggiModel.CalculationHelper.Cache;
using HaenggiModel.CalculationHelper.Resources;

namespace HaenggiModel.CalculationHelper.CalculationTables
{
    public enum BoltonType
    {
        BoltonTotal = 1,
        BoltonPreviousRelation = 2,
    }

    public static class BoltonTable
    {
        private const string BoltonTotalFileName = "BoltonTotalReferences.txt";
        private const string BoltonPreviousFileName = "BoltonPreviousRelationReferences.txt";

        /// <summary>
        /// Finds the bolton total value.
        /// </summary>
        /// <param name="referenceValue">The reference value.</param>
        /// <returns></returns>
        public static decimal? FindBoltonTotalByMaxilarValue(decimal referenceValue)
        {
            try
            {
                var table = GetBoltonTotalTable();
                // var itemFound = table.FirstOrDefault(x => x.Item1.Equals(referenceValue));
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
        /// Finds the bolton total by mandibular value.
        /// </summary>
        /// <param name="referenceValue">The reference value.</param>
        /// <returns></returns>
        /// <exception cref="ElementNotFoundException">Element:  + referenceValue +  is not a valid indexer.</exception>
        public static decimal? FindBoltonTotalByMandibularValue(decimal referenceValue)
        {
            try
            {
                var table = GetBoltonTotalTable();
                var closest = table.OrderBy(item => Math.Abs(referenceValue - item.Item2)).First();

                if (Math.Abs(referenceValue - closest.Item2) > 1)
                {
                    return null;
                }

                return closest.Item3;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static T Closest<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector, TKey pivot) where TKey : IComparable<TKey>
        {
            return source.Where(x => pivot.CompareTo(keySelector(x)) <= 0).OrderBy(keySelector).FirstOrDefault();
        }

        /// <summary>
        /// Finds the previous relation bolton value.
        /// </summary>
        /// <param name="referenceValue">The reference value.</param>
        /// <returns></returns>
        public static decimal? FindPreviousRelationBoltonByMaxilarValue(decimal referenceValue)
        {
            try
            {
                var table = GetBoltonPreviousRelationTable();
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
        /// Finds the previous relation bolton value.
        /// </summary>
        /// <param name="referenceValue">The reference value.</param>
        /// <returns></returns>
        public static decimal? FindPreviousRelationBoltonByMandibularValue(decimal referenceValue)
        {
            try
            {
                var table = GetBoltonPreviousRelationTable();

                var closest = table.OrderBy(item => Math.Abs(referenceValue - item.Item2)).First();

                if (Math.Abs(referenceValue - closest.Item2) > 1)
                {
                    return null;
                }

                return closest.Item3;
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
        private static IEnumerable<Tuple<decimal, decimal, decimal>> GetBoltonTotalTable()
        {
            List<Tuple<decimal, decimal, decimal>> result;

            if (CacheHelper.Get(CacheKey.BoltonTotalTableKey, out result))
            {
                return result;
            }

            result = new List<Tuple<decimal, decimal, decimal>>();
            
            using (var reader = new StreamReader(new MemoryStream(ResourceFiles.BoltonTotalReferences)))
            {
                string line;
                while ((line = reader.ReadLine()) != null && !string.IsNullOrEmpty(line))
                {
                    var items = line.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                    var item1 = decimal.Parse(items[0].Trim(), CultureInfo.InvariantCulture);
                    var item2 = decimal.Parse(items[1].Trim(), CultureInfo.InvariantCulture);
                    var item3 = decimal.Parse(items[2].Trim(), CultureInfo.InvariantCulture);

                    result.Add(new Tuple<decimal, decimal, decimal>(item1, item2, item3));
                }
            }

            CacheHelper.Add(result, CacheKey.BoltonTotalTableKey);

            return result;
        }

        /// <summary>
        /// Gets the moyers superior.
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<Tuple<decimal, decimal, decimal>> GetBoltonPreviousRelationTable()
        {
            List<Tuple<decimal, decimal, decimal>> result;

            if (CacheHelper.Get(CacheKey.BoltonPreviousTableKey, out result))
            {
                return result;
            }

            result = new List<Tuple<decimal, decimal, decimal>>();

            using (var reader = new StreamReader(new MemoryStream(ResourceFiles.BoltonPreviousRelationReferences)))
            {
                string line;
                while ((line = reader.ReadLine()) != null && !string.IsNullOrEmpty(line))
                {
                    var items = line.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                    var item1 = decimal.Parse(items[0].Trim(), CultureInfo.InvariantCulture);
                    var item2 = decimal.Parse(items[1].Trim(), CultureInfo.InvariantCulture);
                    var item3 = decimal.Parse(items[2].Trim(), CultureInfo.InvariantCulture);
                    
                    result.Add(new Tuple<decimal, decimal, decimal>(item1, item2, item3));
                }
            }

            CacheHelper.Add(result, CacheKey.BoltonPreviousTableKey);

            return result;
        }
    }
}
