using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTS_72210454.Data;

namespace UTS_72210454.ViewModels
{
    public partial class AddCategoryLiteViewModel : ObservableObject
    {
        private readonly DatabaseHelper _databaseHelper;

        [ObservableProperty]
        int _categoryId;
        [ObservableProperty]
        string _name;
        [ObservableProperty]
        string _description;

        public AddCategoryLiteViewModel()
        {
            
        }

        [RelayCommand]
        async Task SaveData()
        {
            if (CategoryId == 0)
            {
                await insertCategory();
            }
            else
            {
                await updateCategory();
            }
        }

        [RelayCommand]
        async Task insertCategory()
        {

            try
            {
                await App.CategoryRepo.addCategoryAsync(Name, Description);
                WeakReferenceMessenger.Default.Send(new RefreshMessage(true));
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [RelayCommand]
        async Task updateCategory()
        {
            Categories category = new()
            {
                categoryId = CategoryId,
                name = Name,
                description = Description
            };

            try
            {
                await App.CategoryRepo.updateCategoryAsync(category);
                WeakReferenceMessenger.Default.Send(new RefreshMessage(true));
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [RelayCommand]
        async Task deleteCategory()
        {
            if (CategoryId == 0)
                return;

            try
            {
                await App.CategoryRepo.DeleteCategoryAsync(CategoryId);
                WeakReferenceMessenger.Default.Send(new RefreshMessage(true));
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [RelayCommand]
        async Task DoneEdit()
        {
            await Shell.Current.GoToAsync("..");
        }

    }
}
