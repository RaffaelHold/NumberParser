namespace NumberParser.Common
{
	using System.Collections.ObjectModel;

	/// <summary>
	/// Static class which makes all errors bindable from the view.
	/// </summary>
	public static class ErrorHandler
	{
		/// <summary>
		/// Initializes the <see cref="Errors"/> collection
		/// </summary>
		static ErrorHandler()
		{
			Errors = new ObservableCollection<string>();
		}

		/// <summary>
		/// A collection of all errors.
		/// </summary>
		public static ObservableCollection<string> Errors { get; set; }

		/// <summary>
		/// Adds an errors.
		/// </summary>
		public static void Add(string error)
		{
			Errors.Add(error);
		}

		/// <summary>
		/// Reset all errors.
		/// </summary>
		public static void Reset()
		{
			Errors.Clear();
		}
	}
}
