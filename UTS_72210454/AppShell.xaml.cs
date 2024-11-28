using CommunityToolkit.Mvvm.Input;
using UTS_72210454.Pages;

namespace UTS_72210454
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("addCategory", typeof(AddCategory));
            Routing.RegisterRoute("addCourse", typeof(AddCourse));
            Routing.RegisterRoute("detailCourseHome", typeof(DetailCourseHome));
            Routing.RegisterRoute("addCategoryLite", typeof(AddCategoryLite));

            Routing.RegisterRoute("LoginPage", typeof(LoginPage));

        }

        private async void OnLogOutClicked(object sender, EventArgs e)
        {
            // Navigate to LoginPage
            Application.Current.MainPage = new LoginPage();
        }

    }
}
