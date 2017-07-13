
namespace Common.Extensions
{
    public static class ParseExtensions
    {
        public static int ParseIntFast(this string s)
        {
            int r = 0;
            for (var i = 0; i < s.Length; i++)
            {
                char letter = s[i];
                r = 10 * r;
                r = r + (int)char.GetNumericValue(letter);
            }
            return r;
        }

        public static int IntParseFast(char value)
        {
            int result = 0;
            result = 10 * result + (value - 48);
            return result;
        }

    }
}
