using System.Windows;

namespace RightCrowd.CompareTool
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ApplicationView app = new ApplicationView();
            ApplicationViewModel context = ApplicationViewModel.Instance;
            app.DataContext = context;
            app.Show();
        }
    }
}
