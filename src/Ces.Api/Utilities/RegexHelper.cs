using System.Text.RegularExpressions;

namespace Ces.Api.Utilities
{
    public class RegexHelper
    {
        public static Regex RegexSimbolos()
        {
            return new Regex(@"[?!\@#$%&*-_=+;.\/\\\n]", RegexOptions.None, TimeSpan.FromSeconds(5));
        }
    }
}
