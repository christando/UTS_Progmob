using UTS_72210454.Data;
using UTS_72210454.ViewModels;
using System;
using Microsoft.Maui.Controls;

namespace UTS_72210454;

public partial class CoursePage : ContentPage
{

	private readonly ApiService _ApiService;
    public CoursePage()
	{
		InitializeComponent();
        _ApiService = new ApiService();
        BindingContext = new CoursesViewModel();
        
    }
}