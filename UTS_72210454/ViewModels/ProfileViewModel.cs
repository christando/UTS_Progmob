using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTS_72210454.Data;

namespace UTS_72210454.ViewModels
{
    public partial class ProfileViewModel : ObservableObject
    {
        private readonly ApiService _apiService;

        [ObservableProperty]
        string _userName;

        [ObservableProperty]
        string _fullName;

        [ObservableProperty]
        string _email;

        public ProfileViewModel()
        {
            _apiService = new ApiService();
        }

        [RelayCommand]
        public async Task LoadProfile()
        {
            var profile = await _apiService.GetProfiles();

            _userName = profile.userName;
            Email = profile.email;
            FullName = profile.fullName;
        }
    }
}
