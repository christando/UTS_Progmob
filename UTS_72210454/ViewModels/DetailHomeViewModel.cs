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
    public partial class DetailHomeViewModel : ObservableObject
    {
        private readonly ApiService _apiService;

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
        string _categoryName;


        public DetailHomeViewModel()
        {
            _apiService = new ApiService();
        }
    }
}
