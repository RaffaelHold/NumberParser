namespace NumberParser.Data.DataReader
{
	using System.IO;
	using System.Text;

	public class FileReader : IDataReader
	{
		/// <summary>
		/// Path of the file to be read
		/// </summary>
		private string filePath;

		/// <summary>
		/// Constructs a new <see cref="FileReader"/> and sets its <see cref="filePath"/>
		/// </summary>
		/// <param name="filePath">Path to the file to be read</param>
		public FileReader(string filePath)
		{
			this.filePath = filePath;
		}

		/// <summary>
		/// Asynchronously reads data from a file.
		/// </summary>
		/// <returns>Read data as string array</returns>
		public string[] ReadData()
		{
			try
			{
				return File.ReadAllLines(filePath, Encoding.UTF8);
			}
			catch (FileNotFoundException ex)
			{
				// ToDo: Pass an error message to the view

				return new string[0];
			}
		}
	}
}
