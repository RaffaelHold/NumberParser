﻿namespace NumberParser.Data.DataReader
{
	using System;
	using System.IO;
	using System.Text;
	using Common;

	/// <summary>
	/// Reads a file
	/// </summary>
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
				ErrorHandler.Add("Das Verzeichnis wurde nicht gefunden");
			}
			catch (FileNotFoundException ex)
			{
				ErrorHandler.Add("Die Datei wurde nicht gefunden");
			}
			catch(Exception ex)
			{
				ErrorHandler.Add(ex.Message);
			}

			return new string[0];
		}
	}
}
