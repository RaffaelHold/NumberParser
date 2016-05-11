namespace NumberParser.Data.DataReader
{
	internal interface IDataReader
	{
		/// <summary>
		/// Reads data from a file.
		/// </summary>
		/// <returns>Read data or an empty array</returns>
		string[] ReadData();
	}
}
