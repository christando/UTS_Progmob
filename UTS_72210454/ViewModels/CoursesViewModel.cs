using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.ComponentModel;
using UTS_72210454.Data;

namespace UTS_72210454.ViewModels
{
    public partial class CoursesViewModel : ObservableObject
    {
        private readonly ApiService _apiService;

        [ObservableProperty]
        ObservableCollection<Courses> _courses;

        [ObservableProperty]
        bool _isRefreshing = false;

        [ObservableProperty]
        bool _isBusy = false;

        [ObservableProperty]
        Courses _selectedCourse;

        [ObservableProperty]
        string _searchText;

        public CoursesViewModel()
        {
            _apiService = new ApiService();
            _courses = new ObservableCollection<Courses>();
            WeakReferenceMessenger.Default.Register<RefreshMessage>(this, async (r, m) =>
            {
                await LoadData();
            });
            Task.Run(LoadData);
        }

        [RelayCommand]
        async Task CoursesSelected()
        {
            if (SelectedCourse == null)
                return;

            var navigationParameter = new Dictionary<string, object>
            {
                { "Courses", SelectedCourse }
            };

            await Shell.Current.GoToAsync("addCourse", navigationParameter);

            MainThread.BeginInvokeOnMainThread(() => SelectedCourse = null);
        }

        [RelayCommand]
        async Task LoadData()
        {
            if (IsBusy)
                return;

            try
            {
                IsRefreshing = true;
                IsBusy = true;

                var CoursesCollection = await _apiService.GetCoursesAsync();

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Courses.Clear();
                    foreach (Courses course in CoursesCollection)
                    {
                        Courses.Add(course);
                    }
                });
            }
            finally
            {
                IsRefreshing = false;
                IsBusy = false;
            }
        }

        [RelayCommand]
        async Task AddNewCourse()
        {
            await Shell.Current.GoToAsync("addCourse");
        }

        [RelayCommand]
        async Task SearchCourse(string Cname)
        {
            if (IsBusy || string.IsNullOrWhiteSpace(Cname))
                return;

            try
            {
                IsBusy = true;

                // Fetch filtered courses based on the name
                var filteredCourses = await _apiService.GetByName(Cname);

                // Update the Courses ObservableCollection
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Courses.Clear();
                    foreach (var course in filteredCourses)
                    {
                        Courses.Add(course);
                    }
                });
            }
            catch (Exception ex)
            {
                // Log or handle the error appropriately
                Console.WriteLine($"Error searching for courses: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
