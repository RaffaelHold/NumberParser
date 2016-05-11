namespace NumberParser.MainView
{
	using System.ComponentModel;
	using System.Runtime.CompilerServices;
	using System.Windows.Input;
	using Business.BusinessModels;
	using Common;
	using Microsoft.Win32;

	/// <summary>
	/// ViewModel for <see cref="MainView"/>.
	/// </summary>
	internal class MainViewModel : INotifyPropertyChanged
	{
		private NumberCollection numbers;
		private string filePath;

		/// <summary>
		/// Collection of <see cref="Number"/>.
		/// </summary>
		public NumberCollection Numbers
		{
			get
			{
				return this.numbers;
			}
			set
			{
				this.numbers = value;
				NotifyPropertyChanged();
			}
		}

		/// <summary>
		/// Currently chosen file path.
		/// </summary>
		public string FilePath
		{
			get
			{
				return this.filePath;
			}
			set
			{
				this.filePath = value;
				NotifyPropertyChanged();
			}
		}

		#region Commands

		/// <summary>
		/// Starts parsing a new File.
		/// </summary>
		public ICommand ChooseFileCommand
		{
			get
			{
				return new ActionCommand(p => ChooseFile());
			}
		}

		/// <summary>
		/// Displays a FileDialog and parses the selected file if valid
		/// </summary>
		private void ChooseFile()
		{
			// Remove old errors
			ErrorHandler.Reset();

			var openFileDialog = new OpenFileDialog();
			openFileDialog.DefaultExt = ".txt";
			openFileDialog.CheckFileExists = true;
			openFileDialog.CheckPathExists = true;

			if (openFileDialog.ShowDialog() == false)
			{
				return;
			}

			FilePath = openFileDialog.FileName;

			var parser = new Business.BusinessObjects.NumberParser();
			Numbers = parser.Parse(FilePath);
		}

		#endregion

		#region INotifyPropertyChanged Members

		/// <summary>
		/// Raised when the value of a property has changed.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Raises <see cref="PropertyChanged"/> for the property whose name matches <see cref="propertyName"/>.
		/// </summary>
		/// <param name="propertyName">Optional. The name of the property whose value has changed.</param>
		protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion
	}
}
