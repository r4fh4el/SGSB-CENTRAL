using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SGSB.AppPopulacao
{
	public class MaskedInputConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
				return string.Empty;

			string inputValue = value.ToString();
			string mask = parameter?.ToString();

			if (string.IsNullOrEmpty(mask))
				return inputValue;

			return ApplyMask(inputValue, mask);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value;
		}

		private string ApplyMask(string value, string mask)
		{
			// Implement your masking logic here
			// This is a simple example for a phone number mask
			if (value.Length == 10)
			{
				return Regex.Replace(value, @"(\d{3})(\d{3})(\d{4})", "$1-$2-$3");
			}

			return value;
		}
	}
}
