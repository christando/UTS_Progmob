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
    public partial class AddCategoryViewModel : ObservableObject
    {

        private readonly ApiService _ApiService = new();
        [ObservableProperty]
        int _categoryId;
        [ObservableProperty]
        string _name;
        [ObservableProperty]
        string _description;

        public AddCategoryViewModel()
        {
            _ApiService = new ApiService();
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
            await _ApiService.addCategory(Name, Description);
            WeakReferenceMessenger.Default.Send(new RefreshMessage(true));
            await Shell.Current.GoToAsync("..");
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

            await _ApiService.UpdateCategory(category);
            WeakReferenceMessenger.Default.Send(new RefreshMessage(true));
            await Shell.Current.GoToAsync("..");
        }
        [RelayCommand]
        async Task deleteCategory()
        {

            if (CategoryId == 0)
                return;

            try
            {
                await _ApiService.DeleteCategory(CategoryId);
                WeakReferenceMessenger.Default.Send(new RefreshMessage(true));
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting category: {ex.Message}"); // Log the error
            }
            
        }
        [RelayCommand]
        async Task DoneEdit()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
