using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using UTS_72210454.Data;

namespace UTS_72210454.ViewModels
{
    public partial class HomePageViewModel : ObservableObject
    {
        private readonly ApiService _apiService;

        [ObservableProperty]
        ObservableCollection<GroupedCourse> _groupedCourses;  // Store grouped courses here

        [ObservableProperty]
        ObservableCollection<GroupedCourse> _filteredGroupedCourses;

        [ObservableProperty]
        string _searchText;

        [ObservableProperty]
        bool _isRefreshing = false;

        [ObservableProperty]
        bool _isBusy = false;

        [ObservableProperty]
        Courses _selectedCourse;

        public HomePageViewModel()
        {
            _apiService = new ApiService();
            _groupedCourses = new ObservableCollection<GroupedCourse>();
            _filteredGroupedCourses = new ObservableCollection<GroupedCourse>();
            WeakReferenceMessenger.Default.Register<RefreshMessage>(this, async (r, m) =>
            {
                await LoadData();
            });
            Task.Run(LoadData);
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

                // Clear existing data
                GroupedCourses.Clear();
                FilteredGroupedCourses.Clear();

                // Get grouped data from ApiService
                var groupedData = await _apiService.GetCoursesByCategoryAsync();

                // Group by category and add to GroupedCourses
                foreach (var group in groupedData)
                {
                    var newGroup = new GroupedCourse(group.Key, group.Value);
                    GroupedCourses.Add(newGroup);
                    FilteredGroupedCourses.Add(newGroup); // Also populate FilteredGroupedCourses
                }
            }
            finally
            {
                IsRefreshing = false;
                IsBusy = false;
            }
        }

        [RelayCommand]
        partial void OnSearchTextChanged(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                // If search text is empty, show all courses
                FilteredGroupedCourses.Clear();
                foreach (var group in GroupedCourses)
                {
                    // Add all courses from each group
                    FilteredGroupedCourses.Add(group);
                }
                return;
            }

            // Filter the GroupedCourses based on CategoryName or Course name
            var filtered = GroupedCourses
                .Select(group => new GroupedCourse(
                    group.CategoryName, // Keep the same category name
                    group.Where(course =>
                        course.name.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                        group.CategoryName.Contains(searchText, StringComparison.OrdinalIgnoreCase)).ToList() // Filter courses and categories
                ))
                .Where(group => group.Count > 0) // Only include groups that have matching courses
                .ToList();

            // Update the FilteredGroupedCourses collection
            FilteredGroupedCourses.Clear();
            foreach (var group in filtered)
            {
                FilteredGroupedCourses.Add(group);
            }
        }
    }


}
