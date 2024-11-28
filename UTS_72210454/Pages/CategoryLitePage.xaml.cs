using UTS_72210454.Data;
using UTS_72210454.ViewModels;
using System;
using Microsoft.Maui.Controls;

namespace UTS_72210454.Pages;

public partial class CategoryLitePage : ContentPage
{
	private readonly DatabaseHelper _databaseHelper;
    public CategoryLitePage()
	{
		InitializeComponent();
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "UTS.db3");
        _databaseHelper = new DatabaseHelper(dbPath);
        BindingContext = new CategoryLiteViewModel();
    }
}