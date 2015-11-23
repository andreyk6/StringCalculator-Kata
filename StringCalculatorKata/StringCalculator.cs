using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculatorKata
{
    public class StringCalculator
    {
        private const int DefaultValue = 0;
        private const char DefaultSeparator = ',';

        public int Add(string numbers)
        {
            numbers = SanitizeNumbers(numbers);
            if (ShouldReturnDefaultNumber(numbers))
                return DefaultValue;
            if (ShouldConvertMultipleNumbers(numbers))
                return ConvertMultipleNumbers(numbers);

            return ConvertSingleNumber(numbers);
        }

        private static string SanitizeNumbers(string numbers)
        {
            //Add replace custom separator with default support
            if (CustomSeparatorExist(numbers))
            {
                numbers = ReplaceCustomSeparators(numbers);
            }

            return numbers.Replace('\n', DefaultSeparator);
        }

        private static string ReplaceCustomSeparators(string numbers)
        {
            //Get custom separator
            string[] separators = GetCustomSeparator(numbers);
            numbers = RemoveSeparator(numbers);
            foreach (var separator in separators)
            {
                numbers = numbers.Replace(separator, DefaultSeparator.ToString());
            }
            return numbers;
        }

        private static string RemoveSeparator(string numbers)
        {
            int separatorEnd = numbers.IndexOf("]\n");
            numbers = numbers.Substring(separatorEnd + 2);
            return numbers;
        }

        private static string[] GetCustomSeparator(string numbers)
        {
            int separatorsEnd = numbers.IndexOf("]\n") - 1;
            string separatorsStr = numbers.Substring(2, separatorsEnd);

            List<string> separators = new List<string>();

            while (separatorsStr.Length > 0)
            {
                int separatorEnd = separatorsStr.IndexOf(']');
                separators.Add(separatorsStr.Substring(1, separatorEnd - 1));
                separatorsStr = separatorsStr.Substring(separatorEnd + 1);
            }

            return separators.ToArray();
        }

        private static bool CustomSeparatorExist(string numbers)
        {
            return numbers.StartsWith("//");
        }

        private static int ConvertMultipleNumbers(string numbers)
        {
            var splitedNumbers = numbers.Split(DefaultSeparator);
            return splitedNumbers.Sum(x => ConvertSingleNumber(x)); 
        }

        private static bool ShouldConvertMultipleNumbers(string numbers)
        {
            if (numbers.Contains(DefaultSeparator))
                return true;
            else
                return false;
        }

        private static int ConvertSingleNumber(string numbers)
        {
            int result = Int32.Parse(numbers);

            if (result >= 1000) result = 0;

            if (result < 0) throw new ArgumentOutOfRangeException("Negative args");

            return result;
        }

        private static bool ShouldReturnDefaultNumber(string numbers)
        {
            return String.IsNullOrEmpty(numbers);
        }
    }
}
