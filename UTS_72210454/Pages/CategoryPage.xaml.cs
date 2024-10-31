using System;
using Microsoft.Maui.Controls;
using UTS_72210454.Data;
using UTS_72210454.ViewModels;

namespace UTS_72210454.Pages;

public partial class CategoryPage : ContentPage
{
    private readonly ApiService _ApiService;
    public CategoryPage()
	{
		InitializeComponent();
        _ApiService = new ApiService();
        BindingContext = new CategoryViewModel();
    }
    
}