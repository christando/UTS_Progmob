using UTS_72210454.Data;

namespace UTS_72210454
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Register<ApiService>();

            MainPage = new AppShell();

            //MainPage = new NavigationPage(new LoginPage());
        }
    }
}
