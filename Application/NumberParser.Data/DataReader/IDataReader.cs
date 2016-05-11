namespace NumberParser.Data.DataReader
{
	internal interface IDataReader
	{
		/// <summary>
		/// Asynchronously reads data from a source.
		/// </summary>
		/// <returns>Read data as string array</returns>
		string[] ReadData();
	}
}
