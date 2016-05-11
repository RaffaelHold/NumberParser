namespace NumberParser.Business.BusinessModels
{
	using System;
	using System.Collections.Generic;
	using System.Text;

	/// <summary>
	/// A Collection of type <see cref="Number"/>.
	/// </summary>
	public class NumberCollection
	{
		private List<Number> numbers;

		public NumberCollection()
		{
			numbers = new List<Number>();
		}

		/// <summary>
		/// Adds a new item to the collection.
		/// </summary>
		public void Add(Number item)
		{
			if(item == null)
			{
				throw new ArgumentNullException();
			}

			numbers.Add(item);
		}

		/// <summary>
		/// Bindeable property to display the enqueued numbers.
		/// </summary>
		public string NumbersString
		{
			get
			{
				return this.ToString();
			}
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
