

using UTS_72210454.Data;
using UTS_72210454.ViewModels;

namespace UTS_72210454;

public partial class HomePage : ContentPage
{

    private readonly ApiService _ApiService;

    public HomePage()
	{
        
        InitializeComponent();

        _ApiService = new ApiService();
        BindingContext = new HomePageViewModel();

    }
    
}