namespace NumberParser.Business.BusinessObjects
{
	using System.Collections.Generic;
	using BusinessModels;
	using Data.DataReader;

	/// <summary>
	/// Encapsulates logic to get content and parse it for encoded numbers
	/// </summary>
	public class NumberParser
	{
		/// <summary>
		/// Gets a file and parses it.
		/// </summary>
		/// <param name="path">Path to the file</param>
		/// <returns>A <see cref="NumberCollection"/></returns>
		public NumberCollection Parse(string path)
		{
			var content = GetContent(path);

			return ParseContent(content);
		}

		/// <summary>
		/// Gets a file using a <see cref="FileReader"/>.
		/// </summary>
		/// <param name="path">Path to the file</param>
		/// <returns>File content as array</returns>
		private string[] GetContent(string path)
		{
			var fileReader = new FileReader(path);
			return fileReader.ReadData();
		}

		/// <summary>
		/// Parses a string array for encoded numbers.
		/// </summary>
		/// <param name="content">String array to be parsed</param>
		/// <returns>A <see cref="NumberCollection"/>with found numbers</returns>
		private NumberCollection ParseContent(string[] content)
		{
			if(content.Length == 0)
			{
				return new NumberCollection();
			}

			var queue = new Queue<string[]>();
			string[] queueItem;

			while (content[0].IndexOf(' ') != -1)
			{
				int rightBoundary = GetNumbersRightBoundary(content);

				queueItem = GetNumberEncodingSubstring(content, rightBoundary);
				queue.Enqueue(queueItem);

				RemoveLeadingWhiteSpace(content);
			}

			// Get the last remaining number
			queueItem = GetNumberEncodingSubstring(content);
			queue.Enqueue(queueItem);

			return CreateNumberCollection(queue);
		}

		/// <summary>
		/// Gets the right boundary of the first number.
		/// </summary>
		/// <param name="content">String array to be parsed</param>
		/// <returns>Index of right boundary</returns>
		private int GetNumbersRightBoundary(string[] content)
		{
			int rightBoundary = 0;

			// Get the right boundary of the first number
			for (int i = 0; i < content.Length; i++)
			{
				var tempIndex = content[i].IndexOf(' ');

				if (tempIndex > rightBoundary)
				{
					rightBoundary = tempIndex;
				}
			}

			return rightBoundary;
		}

		/// <summary>
		/// Gets the left boundary of the first number.
		/// </summary>
		/// <param name="content">String array to be parsed</param>
		/// <returns>Index of left boundary</returns>
		private int GetNumbersLeftBoundary(string[] content)
		{
			// Set leftBoundary to the first content rows length.
			// This assumes that rows are the same length
			int leftBoundaryIndex = content[0].Length;

			// Get the left boundary of the next number
			for (int i = 0; i < content.Length; i++)
			{

				// Get the rightmost space in every row
				for(int j = 0; j < content[i].Length; j++)
				{
					if(content[i][j] != ' ' && leftBoundaryIndex > j)
					{
						leftBoundaryIndex = j;
						break;
					}
				}
			}

			return leftBoundaryIndex;
		}

		/// <summary>
		/// Gets the last encoded number in the array.
		/// </summary>
		/// <param name="content">String array to be parsed</param>
		/// <returns>String array containing an encoded number</returns>
		private string[] GetNumberEncodingSubstring(string[] content)
		{
			var tempArr = new string[4];

			// Get the highest indexed first whitespace character
			for (int i = 0; i < content.Length; i++)
			{
				tempArr[i] = content[i].Substring(0).Replace(" ", "");
				content[i] = content[i].Remove(0);
			}

			return tempArr;
		}

		/// <summary>
		/// Gets the first encoded number in the array.
		/// </summary>
		/// <param name="content">String array to be parsed</param>
		/// <param name="index">Right boundary of the first number</param>
		/// <returns>String array containing an encoded number</returns>
		private string[] GetNumberEncodingSubstring(string[] content, int index)
		{
			var tempArr = new string[4];

			// Get the highest indexed first whitespace character
			for (int i = 0; i < content.Length; i++)
			{
				tempArr[i] = content[i].Substring(0, index).Replace(" ", "");
				content[i] = content[i].Remove(0, index);
			}

			return tempArr;
		}

		/// <summary>
		/// Removes the leading whitespace in the array.
		/// </summary>
		/// <param name="content">String array to be parsed</param>
		private void RemoveLeadingWhiteSpace(string[] content)
		{
			int leftBoundary = GetNumbersLeftBoundary(content);

			// Remove leading whitespace
			for (int i = 0; i < content.Length; i++)
			{
				content[i] = content[i].Remove(0, leftBoundary);
			}
		}

		/// <summary>
		/// Creates a <see cref="NumberCollection"/> from the parsed items
		/// </summary>
		/// <param name="queue">Queue with all found numbers as arrays</param>
		/// <returns>A collection of all found numbers</returns>
		private NumberCollection CreateNumberCollection(Queue<string[]> queue)
		{
			var numbers = new NumberCollection();
			while (queue.Count > 0)
			{
				var item = queue.Dequeue();
				var number = new Number(item[0], item[1], item[2], item[3]);
				numbers.Add(number);
			}

			return numbers;
		}
	}
}
