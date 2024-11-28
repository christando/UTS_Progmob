using Android.OS.Strictmode;
using UTS_72210454.Data;
using UTS_72210454.Pages;

namespace UTS_72210454
{
    public partial class App : Application
    {
        private static ApiService _apiService = new ApiService();
        
        public static DatabaseHelper CategoryRepo { get; private set; }

        public App(DatabaseHelper repo)
        {
            InitializeComponent();

            DependencyService.Register<ApiService>();

            //MainPage = new AppShell();

            MainPage = new NavigationPage(new LoginPage());

            CategoryRepo = repo;
        }
    }
}
