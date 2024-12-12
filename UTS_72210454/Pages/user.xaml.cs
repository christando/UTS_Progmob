using UTS_72210454.Data;
using UTS_72210454.ViewModels;

namespace UTS_72210454.Pages;

public partial class user : ContentPage
{

	private readonly ApiService _ApiService;
	private readonly ProfileViewModel _viewModel;
	public user()
	{
		InitializeComponent();

		_ApiService = new ApiService();
		_viewModel = new ProfileViewModel();
		BindingContext = new ProfileViewModel();

		Loaded += async (s, e) => await _viewModel.LoadProfileCommand.ExecuteAsync(null);
	}
}