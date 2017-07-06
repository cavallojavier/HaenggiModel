using System.IO;
using System.Reflection;

namespace HaenggiModel.CalculationHelper.CalculationTables
{
    public class ReadFunctions
    {
        private const string NameSpace = "CalculationHelper.CalculationTables.CalculationReferences.";

        public static StreamReader ReadFile(string fileName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = NameSpace + fileName;

            return new StreamReader(assembly.GetManifestResourceStream(resourceName));
        }
    }
}
