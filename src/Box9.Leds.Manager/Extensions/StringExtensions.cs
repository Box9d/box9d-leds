namespace Box9.Leds.Manager.Extensions
{
    public static class StringExtensions
    {
        public static bool IsInteger(this string value)
        {
            var result = default(int);
            if (!string.IsNullOrEmpty(value) && !int.TryParse(value, out result))
            {
                return false;
            }

            return true;
        }

        public static int? EnsureIsInteger(this string value)
        {
            var result = string.Empty;

            if (!string.IsNullOrEmpty(value))
            {
                foreach (var character in value.ToCharArray())
                {
                    short numberCharacter;
                    if (short.TryParse(character.ToString(), out numberCharacter))
                    {
                        result += character;
                    }
                }

                if (!string.IsNullOrEmpty(result))
                {
                    return int.Parse(result);
                }  
            }

            return null;
        }
    }
}
