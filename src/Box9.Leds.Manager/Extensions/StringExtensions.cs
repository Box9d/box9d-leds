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
    }
}
