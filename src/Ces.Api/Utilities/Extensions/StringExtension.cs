using Ces.Api.Utilities;

namespace Ces.Api.Utilities.Extensions
{
    public static class StringExtension
    {

        public static bool ContainsText(this string target)
        {
            string aux = RegexHelper.RegexSimbolos().Replace(target, "");
            return !string.IsNullOrEmpty(aux);
        }

    }
}
