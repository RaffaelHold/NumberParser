namespace NumberParser.Business.BusinessModels
{
	using System.Collections.Generic;
	using System.Text;

	/// <summary>
	/// A FIFO Collection of type <see cref="Number"/>.
	/// </summary>
	public class NumberCollection
	{
		private Queue<Number> numbers;

		public NumberCollection()
		{
			numbers = new Queue<Number>();
		}

		/// <summary>
		/// Overrides the ToString() method to display the enqueued numbers.
		/// </summary>
		/// <returns>String representation of the enqueued numbers</returns>
		public override string ToString()
		{
			var sb = new StringBuilder();
			
			foreach(var number in numbers)
			{
				sb.Append(number.GetNumber());
				sb.Append(" ");
			}
			
			return sb.ToString().TrimEnd();
		}
	}
}
