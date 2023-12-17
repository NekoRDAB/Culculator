using Castle.Core.Internal;

namespace Culculator.Application.Extensions
{
    public static class KeyExtensions
    {
        public static bool IsNumeric(this string key, string currentText,
            bool isDotAllow = false, bool isZeroFirstAllowed = false)
        {
            if (isZeroFirstAllowed && key == "D0")
                return true;
            if (isDotAllow && key == "OemPeriod" && !string.IsNullOrEmpty(currentText) && currentText.Contains('.'))
                return false;
            if (!isZeroFirstAllowed && key == "D0" && string.IsNullOrEmpty(currentText))
                return false;
            // if (!isZeroFirstAllowed && key == "D0" && currentText.Length > 0)
            //     return false;
            if (isDotAllow && key == "OemPeriod" && string.IsNullOrEmpty(currentText))
                return false;
            if (isDotAllow && key == "OemPeriod")
                return true;

            var numbers = new List<string> { "D0", "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "D9" };

            if (numbers.Contains(key) )
                return true;

            return false;
        }
    }
}