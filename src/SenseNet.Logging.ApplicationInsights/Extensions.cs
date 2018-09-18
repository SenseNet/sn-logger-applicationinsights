using System;

namespace SenseNet.Logging.ApplicationInsights
{
    internal static class Extensions
    {
        public static string MaximizeLength(this string value, int maxLength)
        {
            if (maxLength < 0)
                throw new ArgumentOutOfRangeException(nameof(maxLength), "Max length cannot be less than zero.");

            return value?.Substring(0, Math.Min(value.Length, maxLength));
        }
    }
}
