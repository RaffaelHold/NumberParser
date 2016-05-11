namespace NumberParser.Common
{
	using System;
	using System.Windows.Input;

	// Taken from another project

	/// <summary>
	/// <see cref="ICommand"/> implementation that wraps an <see cref="Action"/> delegate.
	/// </summary>
	public sealed class ActionCommand : ICommand
	{
		private readonly Action<object> action;
		private readonly Predicate<object> predicate;
		public event EventHandler CanExecuteChanged;

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionCommand"/> class.
		/// </summary>
		/// <param name="action">The <see cref="Action"/> delegate to wrap.</param>
		public ActionCommand(Action<object> action)
			: this(action, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionCommand"/> class.
		/// </summary>
		/// <param name="action">The <see cref="Action"/> delegate to wrap.</param>
		/// <param name="predicate">The <see cref="Predicate{Object}"/> that determines whether the action delegate may be invoked.</param>
		public ActionCommand(Action<object> action, Predicate<object> predicate)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action", "You must specify an Action<T>.");
			}

			this.action = action;
			this.predicate = predicate;
		}

		/// <summary>
		/// Defines the method that determines whether the command can execute in its current state.
		/// </summary>
		/// <returns>
		/// true if this command can be executed; otherwise, false.
		/// </returns>
		/// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
		public bool CanExecute(object parameter)
		{
			if (predicate == null)
			{
				return true;
			}

			return predicate(parameter);
		}

		/// <summary>
		/// Defines the method to be called when the command is invoked.
		/// </summary>
		/// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
		public void Execute(object parameter)
		{
			action(parameter);
		}

		/// <summary>
		/// Executes the action delegate without any parameters.
		/// </summary>
		public void Execute()
		{
			Execute(null);
		}

		/// <summary>
		/// Invokes the reevaluation of the CanExecute method
		/// </summary>
		public void RaiseCanExecuteChanged()
		{
			EventHandler handler = CanExecuteChanged;
			if (handler != null)
			{
				handler(this, new EventArgs());
			}
		}
	}
}
