
namespace UTS_72210454;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

	private async void OnLoginClicked(object sender, EventArgs e)
	{
		string username = usernameEntry.Text;
		string password = passwordEntry.Text;

		if (username == "admin" && password == "password")
		{
            Application.Current.MainPage = new AppShell();
        }
		else
		{
            
            MessageLabel.Text = "invalid Username or Password";
            MessageLabel.IsVisible = true;
        }
	}
}