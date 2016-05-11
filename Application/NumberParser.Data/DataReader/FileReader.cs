namespace NumberParser.Data.DataReader
{
	using System;
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
		public FileReader()
		{
			this.filePath = DataSettings.Default.FileName;
		}

		/// <summary>
		/// Reads data from a file.
		/// </summary>
		/// <returns>Read data or an empty array</returns>
		public string[] ReadData()
		{
			try
			{
				return File.ReadAllLines(filePath, Encoding.UTF8);
			}
			catch (DirectoryNotFoundException ex)
			{
				// ToDo: Pass an error message to the view
			}
			catch (FileNotFoundException ex)
			{
				// ToDo: Pass an error message to the view
			}
			catch(Exception ex)
			{
				// Since we set the file path ourself the path should always be valid, except if the directory or the file has been deleted.
				// If we decide to let the user select a path we might have to check for additional exceptions

				// ToDo: Pass an error message to the view
			}

			return new string[0];
		}
	}
}
