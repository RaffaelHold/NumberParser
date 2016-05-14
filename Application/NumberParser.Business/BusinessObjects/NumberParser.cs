namespace NumberParser.Business.BusinessObjects
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using BusinessModels;
	using Common;
	using Data.DataReader;
	
	/// <summary>
	/// Encapsulates logic to get content and parse it for encoded numbers
	/// </summary>
	public class NumberParser
	{
		/// <summary>
		/// Describes how many rows encode a number
		/// </summary>
		private const int rowHeight = 4;
		private const char placeholderChar = '$';
		private IEnumerable<char> allowedChars = new[] { '-', '/', '\\', '_', '|' };

		/// <summary>
		/// Gets a file and parses it.
		/// </summary>
		/// <param name="path">Path to the file</param>
		/// <returns>A <see cref="NumberCollection"/></returns>
		public ICollection<NumberCollection> Parse(string path)
		{
			var content = GetContent(path);
			ReplaceIrrelevantChars(content);

			var numberCollections = new List<NumberCollection>();

			for (int i = 0; i < content.Length / rowHeight; i++)
			{
				var arr = content.Skip(i * rowHeight).Take(rowHeight).ToArray();
				numberCollections.Add(ParseContent(arr));
			}

			return numberCollections;
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
		/// Replace all irrelevant characters with a placeholder.
		/// </summary>
		/// <param name="content">Array to remove irrelevant characters from</param>
		private void ReplaceIrrelevantChars(string[] content)
		{
			// Replace all other disallowed characters
			// Tabs before spaces are double the width
			for (int i = 0; i < content.Length; i++)
			{
				content[i] = ReplaceTabs(content[i]);

				content[i] = new String(content[i].Select(p => !allowedChars.Contains(p) ? placeholderChar : p).ToArray());
			}
		}

		/// <summary>
		/// Replace tabs with the right number of spaces.
		/// </summary>
		/// <param name="input">String to operate on</param>
		/// <returns>Inout with tabs replaced</returns>
		private string ReplaceTabs(string input)
		{
			// All tabs followed by whitespace fill to the next tabstop (multiples of 8)
			while(input.IndexOf('\t') != -1)
			{
				int index = input.IndexOf('\t');

				// If the next char is not a space the tabstop is always 1 long
				// Otherwise the tabstop ranges to the next TabStop
				if(input[index + 1] != ' ')
				{
					input = input.Remove(index, 1).Insert(index, placeholderChar.ToString());
					continue;
				}

				string replacement = GetReplacement(index);
				input = input.Remove(index, 1).Insert(index, replacement);
			}

			return input;
		}

		/// <summary>
		/// Returns the correct amount of replacement characters for tab. 
		/// </summary>
		/// <param name="index">Index of the tabstop</param>
		/// <returns>String of replacement chaaracters</returns>
		private string GetReplacement(int index)
		{
			// TabStops are multiples of 8
			int nextTabStop = 8;
			while (index > nextTabStop)
			{
				nextTabStop += 8;
			}

			return new String(placeholderChar, nextTabStop - index);
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

			if(content.Length != rowHeight)
			{
				ErrorHandler.Add("Die Zeile ist unvollständig");
			}

			var queue = new Queue<string[]>();
			string[] queueItem;

			while (content[0].IndexOf(placeholderChar) != -1)
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
				var tempIndex = content[i].IndexOf(placeholderChar);

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
			// Set leftBoundary to int.MaxValue otherwise we wont find the first element
			int leftBoundaryIndex = int.MaxValue;

			// Get the left boundary of the next number
			for (int i = 0; i < rowHeight; i++)
			{

				// Get the rightmost space in every row
				for(int j = 0; j < content[i].Length; j++)
				{
					if(content[i][j] != placeholderChar && leftBoundaryIndex > j)
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
			var tempArr = new string[rowHeight];

			// Get the highest indexed first whitespace character
			for (int i = 0; i < rowHeight; i++)
			{
				tempArr[i] = content[i].Substring(0).Replace(placeholderChar.ToString(), String.Empty);
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
			var tempArr = new string[rowHeight];

			// Get the highest indexed first whitespace character
			for (int i = 0; i < rowHeight; i++)
			{
				tempArr[i] = content[i].Substring(0, index).Replace(placeholderChar.ToString(), String.Empty);
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
			for (int i = 0; i < rowHeight; i++)
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
