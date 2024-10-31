using UTS_72210454.Data;
using UTS_72210454.ViewModels;

namespace UTS_72210454.Pages;

[QueryProperty("PartToDisplay", "Courses")]

public partial class DetailCourseHome : ContentPage
{

    DetailHomeViewModel viewModel;
    public DetailCourseHome()
	{
		InitializeComponent();
        viewModel = new DetailHomeViewModel();
        BindingContext = viewModel;
    }
    
    Courses _PartToDisplay;
    public Courses PartToDisplay
    {
        get => _PartToDisplay;
        set
        {
            if (_PartToDisplay == value)
                return;

            _PartToDisplay = value;

            if (_PartToDisplay != null)
            {
                viewModel.CourseId = _PartToDisplay.courseId;
                viewModel.Name = _PartToDisplay.name;
                viewModel.ImageName = _PartToDisplay.imageName;
                viewModel.Duration = _PartToDisplay.duration;
                viewModel.Description = _PartToDisplay.description;
                viewModel.CategoryName = _PartToDisplay.Category.name;
                
            }

        }
    }
}