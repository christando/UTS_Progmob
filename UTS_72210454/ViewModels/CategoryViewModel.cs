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
    public partial class CategoryViewModel : ObservableObject
    {

        private readonly ApiService _apiService;

        [ObservableProperty]
        ObservableCollection<Categories> _categories;

        [ObservableProperty]
        bool _isRefreshing = false;

        [ObservableProperty]
        bool _isBusy = false;

        [ObservableProperty]
        Categories _selectedCategory;

        public CategoryViewModel()
        {
            _apiService = new ApiService();
            _categories = new ObservableCollection<Categories>();
            WeakReferenceMessenger.Default.Register<RefreshMessage>(this, async (r, m) =>
            {
                await LoadData();
            });
            Task.Run(LoadData);
        }

        [RelayCommand]
        async Task CategoriesSelected()
        {
            if (SelectedCategory == null)
                return;

            var navigationParameter = new Dictionary<string, object>
            {
                { "Categories", SelectedCategory }
            };

            await Shell.Current.GoToAsync("addCategory", navigationParameter);

            MainThread.BeginInvokeOnMainThread(() => SelectedCategory = null);
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

                var CategoryCollection = await _apiService.GetCategoriesAsync();

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Categories.Clear();
                    foreach (Categories category in CategoryCollection)
                    {
                        Categories.Add(category);
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
        async Task AddNewCategory()
        {
            await Shell.Current.GoToAsync("addCategory");
        }

    }
}
