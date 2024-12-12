using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UTS_72210454.Data;

namespace UTS_72210454.ViewModels
{
    public partial class DetailHomeViewModel : ObservableObject
    {
        private readonly ApiService _apiService;

        [ObservableProperty]
        ObservableCollection<Instructor> _instructors;

        ObservableCollection<LoginResponse> _loginResponses;

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

        [ObservableProperty]
        bool _canEnroll;

        [ObservableProperty]
        string _enrollButtonText = "Enroll";


        public DetailHomeViewModel()
        {
            _apiService = new ApiService();
            CheckEnrollmentStatus();
        }

        async void CheckEnrollmentStatus()
        {
            // Cek apakah pengguna sudah enroll ke kursus ini
            bool isEnrolled = await _apiService.IsUserEnrolled(CourseId);

            CanEnroll = !isEnrolled;
            EnrollButtonText = isEnrolled ? "Already Enrolled" : "Enroll";
        }

        [RelayCommand]
        async Task Enroll()
        {
            if (CanEnroll)
            {
                await _apiService.Enrollments(CourseId);
                WeakReferenceMessenger.Default.Send(new RefreshMessage(true));

                // Perbarui status setelah enroll
                CanEnroll = false;
                EnrollButtonText = "Already Enrolled";

                await Shell.Current.GoToAsync("..");
            }
        }
    }
}
