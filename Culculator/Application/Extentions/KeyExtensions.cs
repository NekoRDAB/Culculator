using System.Collections.Generic;
using Castle.Core.Internal;

namespace UserInterface
{
    public static class KeyExtensions
    {
        public static bool IsNumeric(this string key, string currentText, bool isDotAllow = false, bool isZeroFirstAllowed = false)
        {
            if (isZeroFirstAllowed && key == "D0")
                return true;
            if (isDotAllow && key == "OemPeriod" && !currentText.IsNullOrEmpty() && currentText.Contains('.'))
                return false;
            if (!isZeroFirstAllowed && key == "D0" && currentText.IsNullOrEmpty())
                return false;
            if (!isZeroFirstAllowed && key == "D0" && currentText.Length > 0)
                return false;
            if (isDotAllow && key == "OemPeriod" && currentText.IsNullOrEmpty())
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