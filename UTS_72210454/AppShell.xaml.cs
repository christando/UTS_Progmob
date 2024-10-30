using CommunityToolkit.Mvvm.Input;

namespace UTS_72210454
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("addCategory", typeof(AddCategory));
            Routing.RegisterRoute("addCourse", typeof(AddCourse));



        }
    }
}
