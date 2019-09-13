using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Keboho.Shared
{
    public static class Utils
    {
        private readonly static NumberFormatInfo CurrencyFormat;

        static Utils()
        {
            var culture = CultureInfo.CreateSpecificCulture("en-US");
            var currencyFormat = (NumberFormatInfo)culture.NumberFormat.Clone();
            currencyFormat.CurrencyNegativePattern = 0;
            CurrencyFormat = currencyFormat;
        }

        public static string ToCurrencyString(this decimal amount)
        {
            return amount.ToString("C2", CurrencyFormat);
        }
    }
}
