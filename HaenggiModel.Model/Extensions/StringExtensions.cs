namespace HaenggiModel.Model.Extensions
{
    public static class StringExtensions
    {
        public static string ResultToString(this decimal? result)
        {
            return result.HasValue ? result.Value.ToString() : "-";
        }
    }
}
