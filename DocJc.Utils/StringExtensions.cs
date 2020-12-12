namespace DocJc.Utils
{
    using System;

    public static class StringExtensions
    {
        public static bool ContainsCaseInsensitive(this string container, string value)
        {
            return container.IndexOf(value, StringComparison.InvariantCultureIgnoreCase) >= 0;
        }
    }
}
