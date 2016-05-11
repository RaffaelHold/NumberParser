using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberParser.Business.BusinessModels
{
	public class Number
	{
		private static Dictionary<string, int> encodedNumbers;

		/// <summary>
		/// Adds the known number encodings into <see cref="encodedNumbers"/>
		/// </summary>
		static Number()
		{
			encodedNumbers = new Dictionary<string, int>();
			encodedNumbers.Add("", 1);
			encodedNumbers.Add("", 2);
			encodedNumbers.Add("", 3);
			encodedNumbers.Add("", 4);
			encodedNumbers.Add("", 5);
		}

		private string encodedNumber;

		/// <summary>
		/// Creates a new <see cref="Number"/> object.
		/// </summary>
		/// <param name="line1">First line of the number encoding</param>
		/// <param name="line2">Second line of the number encoding</param>
		/// <param name="line3">Third line of the number encoding</param>
		/// <param name="line4">Fourth line of the number encoding</param>
		public Number(string line1, string line2, string line3, string line4)
		{
			var sb = new StringBuilder();

			sb.Append(line1);
			sb.Append(line2);
			sb.Append(line3);
			sb.Append(line4);

			this.encodedNumber = sb.ToString();
		}

		/// <summary>
		/// Returns the number the object encodes.
		/// </summary>
		/// <returns>Number the object encodes</returns>
		public string GetNumber()
		{
			int value;

			var found = encodedNumbers.TryGetValue(encodedNumber, out value);

			if (found)
			{
				return value.ToString();
			}
			else
			{
				// ToDo: Log error
				return "";
			}
		}
	}
}
