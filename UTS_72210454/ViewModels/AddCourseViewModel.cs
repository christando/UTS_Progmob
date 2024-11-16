using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UTS_72210454.Data;
using System.Collections.ObjectModel;


namespace UTS_72210454.ViewModels
{
    public partial class AddCourseViewModel : ObservableObject
    {

        private readonly ApiService _ApiService = new();

        [ObservableProperty]
        int _courseId;

        [ObservableProperty]
        string _name;

        [ObservableProperty]
        string _imageName;

        [ObservableProperty]
        int _duration;

        [ObservableProperty]
        string _description;

        [ObservableProperty]
        int _categoryId;

        [ObservableProperty]
        Categories _selectedCategory;

        // Update Categories to be an ObservableCollection
        public ObservableCollection<Categories> Categories { get; set; } = new ObservableCollection<Categories>();

        public AddCourseViewModel()
        {
            _ApiService = new ApiService();
            LoadCategories();
        }

        async void LoadCategories()
        {
            // Retrieve categories and set them to the ObservableCollection
            var categories = await _ApiService.GetCategoriesAsync();
            Categories.Clear();
            foreach (var category in categories)
            {
                Categories.Add(category);
            }
        }

        [RelayCommand]
        async Task SaveData()
        {
            if (CourseId == 0)
            {
                CategoryId = SelectedCategory.categoryId;
                await insertCourse();
            }
            else
            {
                CategoryId = SelectedCategory.categoryId;
                await updateCourse();
            }
                
        }

        [RelayCommand]
        async Task insertCourse()
        {
            await _ApiService.AddCourse(Name, ImageName, Duration, Description, CategoryId);
            WeakReferenceMessenger.Default.Send(new RefreshMessage(true));
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        async Task updateCourse()
        {
            var Updatecourse = new UpdateCourse
            {
                courseId = CourseId,
                name = Name,
                imageName = ImageName,
                duration = Duration,
                description = Description,
                categoryId = CategoryId
            };

            await _ApiService.UpdateCourse(Updatecourse);
            WeakReferenceMessenger.Default.Send(new RefreshMessage(true));
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        async Task DeleteCourse()
        {
            if(CourseId == 0)
                return;

            try
            {
                await _ApiService.DeleteCourse(CourseId);
                WeakReferenceMessenger.Default.Send(new RefreshMessage(true));
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting Courses: {ex.Message}"); // Log the error
            }

        }

        [RelayCommand]
        async Task DoneEditing()
        {
            await Shell.Current.GoToAsync("..");
        }

    }
}
