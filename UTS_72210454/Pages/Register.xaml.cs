using UTS_72210454.Data;

namespace UTS_72210454.Pages;

public partial class Register : ContentPage
{
	public Register()
	{
		InitializeComponent();
	}

	private async void OnRegisClicked(object sender, EventArgs e)
	{
		string email = emailEntry.Text;
		string username = usernameEntry.Text;
		string password = passwordEntry.Text;
		string fullName = fullNameEntry.Text;

		try
		{
			var apiService = new ApiService();
			await apiService.regisuser(email, username, password, fullName);

            Application.Current.MainPage = new LoginPage();
        }
		catch (Exception ex)
		{
			await DisplayAlert("Error", ex.Message, "OK");

		}
	}

	private async void OnLoginClicked(object sender, EventArgs e)
	{
		Application.Current.MainPage = new LoginPage();
	}
}