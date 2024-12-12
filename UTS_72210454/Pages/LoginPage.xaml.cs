
using System.Linq;
using UTS_72210454.Data;

namespace UTS_72210454.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

	private async void OnLoginClicked(object sender, EventArgs e)
	{
		string email = emailEntry.Text;
		string password = passwordEntry.Text;

        try
        {
            var apiService = new ApiService();
            var loginResponse = await apiService.LoginAsync(email, password);

            if (loginResponse != null && !string.IsNullOrEmpty(loginResponse.token))
            {
                // Simpan token ke SecureStorage
                await SecureStorage.Default.SetAsync("Auth_token", loginResponse.token);

                // Simpan userName ke SecureStorage
                await SecureStorage.Default.SetAsync("userName", loginResponse.userName);

                // Simpan email ke SecureStorage
                await SecureStorage.Default.SetAsync("email", loginResponse.email);

                // Navigasi ke halaman utama
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                MessageLabel.Text = "Invalid Username or Password"; // Pesan error
                MessageLabel.IsVisible = true;
            }
        }
        catch (Exception ex)
        {
            // Menampilkan error
            MessageLabel.Text = ex.Message;
            MessageLabel.IsVisible = true;
        }
    }

    private async void OnRegisClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new Register();
    }
}