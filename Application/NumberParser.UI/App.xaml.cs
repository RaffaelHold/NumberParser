namespace NumberParser
{
	using System.Windows;
	using Microsoft.Practices.Unity;
	using MainView;

	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			using (var container = new UnityContainer())
			{
				container.RegisterType<MainView.MainView>();
				container.RegisterType<MainViewModel>();

				var view = container.Resolve<MainView.MainView>();
				view.MainViewModel = container.Resolve<MainViewModel>();

				view.ShowDialog();
			}
		}
	}
}
