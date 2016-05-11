namespace NumberParser.Data.DataReader
{
	internal interface IDataReader
	{
		/// <summary>
		/// Reads data from a source.
		/// </summary>
		/// <returns>Read data or an empty array</returns>
		string[] ReadData();
	}
}
