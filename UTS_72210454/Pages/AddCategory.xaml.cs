using UTS_72210454.Data;
using UTS_72210454.ViewModels;

namespace UTS_72210454.Pages;

[QueryProperty("PartToDisplay", "Categories")]

public partial class AddCategory : ContentPage
{
    AddCategoryViewModel viewModel;
    public AddCategory()
	{
		InitializeComponent();
        viewModel = new AddCategoryViewModel();
        BindingContext = viewModel;
    }

    Categories _PartToDisplay;

    public Categories PartToDisplay
    {
        get => _PartToDisplay;
        set
        {
            if (_PartToDisplay == value)
                return;

            _PartToDisplay = value;

            if (_PartToDisplay != null)
            {
                viewModel.CategoryId = _PartToDisplay.categoryId;
                viewModel.Name = _PartToDisplay.name;
                viewModel.Description = _PartToDisplay.description;
            }
            
        }
    }
}