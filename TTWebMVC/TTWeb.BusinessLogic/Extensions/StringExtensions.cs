using System.Linq;
using static System.Char;

namespace TTWeb.BusinessLogic.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveWhiteSpace(this string str)
        {
            return new string(str.ToCharArray()
                .Where(c => !IsWhiteSpace(c))
                .ToArray());
        }
    }
}
