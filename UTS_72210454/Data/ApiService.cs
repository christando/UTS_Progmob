using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text.Json;

namespace UTS_72210454.Data
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://actbackendseervices.azurewebsites.net/");
        }

        private async Task setAuthorizationHeader()
        {
            var token = await SecureStorage.Default.GetAsync("Auth_token");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                Console.WriteLine($"Authorization Header Set: Bearer {token}");
            }
            else
            {
                Console.WriteLine("No token found in SecureStorage.");
            }

            // Log the headers to verify
            foreach (var header in _httpClient.DefaultRequestHeaders)
            {
                Console.WriteLine($"Header: {header.Key}: {string.Join(", ", header.Value)}");
            }
        }


        //Categories
        public async Task<IEnumerable<Categories>> GetCategoriesAsync()
        {
            try
            {
                await setAuthorizationHeader();
                var response = await _httpClient.GetAsync("api/categories");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var categories = System.Text.Json.JsonSerializer.Deserialize<List<Categories>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    return categories ?? new List<Categories>();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine("Unauthorized: Token may have expired.");
                    throw new Exception("Session expired. Please login again.");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error Response: {errorContent}");
                    throw new Exception($"Failed to fetch categories: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in GetCategoriesAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<Categories> addCategory(string name, string description)
        {
            await setAuthorizationHeader();
            var category = new Categories()
            {
                name = name,
                description = description
            };

            var jsonCategory = JsonConvert.SerializeObject(category);

            var content = new StringContent(jsonCategory, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/categories", content);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var createdCategory = JsonConvert.DeserializeObject<Categories>(json);

                return createdCategory;
            }
            else
            {
                throw new Exception($"Failed to create category: {response.ReasonPhrase}");
            }

        }

        public async Task<Categories> GetById(int categoryId)
        {
            await setAuthorizationHeader();
            var response = await _httpClient.GetAsync($"api/categories/{categoryId}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var category = JsonConvert.DeserializeObject<Categories>(json);
                return category;
            }
            else
            {
                throw new Exception($"Failed to get category: {response.ReasonPhrase}");
            }
        }

        public async Task<Categories> UpdateCategory(Categories category)
        {
            await setAuthorizationHeader();
            // Create an HttpRequestMessage for the PUT request
            var jsonCategory = JsonConvert.SerializeObject(category);
            var content = new StringContent(jsonCategory, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/categories/{category.categoryId}", content);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var updatedCategory = JsonConvert.DeserializeObject<Categories>(json);
                return updatedCategory; // Ensure a value is returned
            }
            else
            {
                throw new Exception($"Failed to update category: {response.ReasonPhrase}");
            }
        }

        public async Task DeleteCategory(int categoryId)
        {
            await setAuthorizationHeader();
            var response = await _httpClient.DeleteAsync($"api/Categories/{categoryId}");


            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to delete category APIService: {response.ReasonPhrase}");
            }
        }


        //Courses
        public async Task<IEnumerable<Courses>> GetCoursesAsync()
        {
            await setAuthorizationHeader();
            var course = await _httpClient.GetFromJsonAsync<List<Courses>>("api/courses");
            return course;
        }

        public async Task<List<Enroll>> GetEnrollments()
        {
            try
            {
                // Ganti dengan endpoint API Anda
                var enrollments = await _httpClient.GetFromJsonAsync<List<Enroll>>("api/enrollments");

                // Jika data berhasil diambil, kembalikan hasilnya
                return enrollments ?? new List<Enroll>();
            }
            catch (Exception ex)
            {
                // Tangani error
                Console.WriteLine($"Error fetching enrollments: {ex.Message}");
                return new List<Enroll>();
            }
        }

        public async Task<bool> IsUserEnrolled(int courseId)
        {
            try
            {
                // Ambil daftar enrollments dari API
                var enrollments = await GetEnrollments();

                // Periksa apakah courseId ada dalam daftar enrollments
                return enrollments.Any(e => e.courseId == courseId);
            }
            catch (Exception ex)
            {
                // Tangani error
                Console.WriteLine($"Error checking enrollment: {ex.Message}");
                return false; // Jika terjadi error, asumsikan user belum enroll
            }
        }

        public async Task<AddCourse> AddCourse(string name, string imageName, int duration, string description, int categoryId)
        {
            await setAuthorizationHeader();
            var Course = new AddCourse()
            {
                name = name,
                imageName = imageName,
                duration = duration,
                description = description,
                categoryId = categoryId
            };
            var jsonCourse = JsonConvert.SerializeObject(Course);
            var content = new StringContent(jsonCourse, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/courses", content);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var createdCourse = JsonConvert.DeserializeObject<AddCourse>(json);
                return createdCourse;
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Failed to create course: {response.ReasonPhrase}, details: {errorMessage}");
            }
        }

        public async Task<Courses> UpdateCourse(UpdateCourse course)
        {
            await setAuthorizationHeader();
            var jsonCategory = JsonConvert.SerializeObject(course);
            var content = new StringContent(jsonCategory, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/courses/{course.courseId}", content);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var updatedCourse = JsonConvert.DeserializeObject<Courses>(json);
                return updatedCourse; // Ensure a value is returned
            }
            else
            {
                throw new Exception($"Failed to update course: {response.ReasonPhrase}");
            }
        }

        public async Task DeleteCourse(int courseId)
        {
            await setAuthorizationHeader();
            var response = await _httpClient.DeleteAsync($"api/courses/{courseId}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to delete course: {response.ReasonPhrase}");
            }
        }

        public async Task<Dictionary<string, List<Courses>>> GetCoursesByCategoryAsync()
        {
            await setAuthorizationHeader();
            // Get the courses from the API
            var courses = await _httpClient.GetFromJsonAsync<List<Courses>>("api/courses");

            // Group the courses by the category's name
            var groupedCourses = courses.Where(c => c.Category != null) // Ensure the category object exists
                .GroupBy(c => c.Category.name)  // Use the 'name' from the Category object
                .ToDictionary(g => g.Key, g => g.ToList());

            return groupedCourses;
        }

        public async Task<List<Courses>> GetByName(string name)
        {
            await setAuthorizationHeader();
            var response = await _httpClient.GetAsync($"api/courses/search/{name}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var course = JsonConvert.DeserializeObject<List<Courses>>(json);
                return course;
            }
            else
            {
                throw new Exception($"Failed to get course: {response.ReasonPhrase}");
            }
        }

        //register user
        public async Task<User> regisuser(string email, string userName, string password, string fullName)
        {
            var user = new User()
            {
                email = email,
                userName = userName,
                password = password,
                fullName = fullName
            };
            var jsonUser = JsonConvert.SerializeObject(user);
            var content = new StringContent(jsonUser, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/users", content);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var createdUser = JsonConvert.DeserializeObject<User>(json);
                return createdUser;
            }
            else
            {
                throw new Exception($"Failed to create user: {response.ReasonPhrase}");
            }
        }

        public async Task<Profiles> GetProfiles()
        {
            await setAuthorizationHeader();
            List<Instructor> instructors = await GetInstructorsFromApi();
            List<User> user = await GetUserFromApi();
            string username = await SecureStorage.Default.GetAsync("userName");
            string email = await SecureStorage.Default.GetAsync("email");
            var fullName = instructors
                .Where(i => i.userName == username)
                .Select(i => i.fullName)
                .FirstOrDefault();

            var profile = new Profiles
            {
                email = email,
                userName = username,
                fullName = fullName
            };
            return profile;
        }

        //Login
        public async Task<LoginResponse> LoginAsync(string email, string password)
        {
            try
            {
                // Membuat objek login data dari model Login
                var loginData = new Login
                {
                    userName = email,
                    password = password
                };

                // Serialisasi data login ke JSON
                var jsonContent = new StringContent(
                    System.Text.Json.JsonSerializer.Serialize(loginData),
                    System.Text.Encoding.UTF8,
                    "application/json"
                );

                // Mengirim POST request ke endpoint login
                var response = await _httpClient.PostAsync("api/login", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    // Membaca dan mendeserialisasi response
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var loginResponse = System.Text.Json.JsonSerializer.Deserialize<LoginResponse>(responseContent);

                    return loginResponse; // Mengembalikan objek LoginResponse
                }
                else
                {
                    return null; // Return null jika login gagal
                }
            }
            catch (Exception ex)
            {
                // Menangani error dan melempar exception dengan pesan yang lebih jelas
                throw new Exception("Error during login: " + ex.Message);
            }

        }

        public async Task<List<Instructor>> GetInstructorsFromApi()
        {
            // Lakukan panggilan API untuk mendapatkan data instruktur
            var response = await _httpClient.GetAsync("api/instructors");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var instructors = JsonConvert.DeserializeObject<List<Instructor>>(responseContent);
                return instructors;
            }
            else
            {
                throw new Exception("Failed to fetch instructors.");
            }
        }

        public async Task<List<User>> GetUserFromApi()
        {
            // Lakukan panggilan API untuk mendapatkan data user
            var response = await _httpClient.GetAsync("api/users");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<List<User>>(responseContent);
                return user;
            }
            else
            {
                throw new Exception("Failed to fetch instructors.");
            }
        }

        public async Task<Enroll> Enrollments(int courseId)
        {
            await setAuthorizationHeader();

            List<Instructor> instructors = await GetInstructorsFromApi();
            string username = await SecureStorage.Default.GetAsync("userName");

            Console.WriteLine($"Username : {username}");

            // Cari instructorId berdasarkan username
            var instructorId = instructors
                .Where(i => i.userName == username)
                .Select(i => i.instructorId)
                .FirstOrDefault();

            if (instructorId == 0)
            {
                throw new Exception("Instructor not found for the current user.");
            }

            var enroll = new Enroll()
            {
                instructorId = instructorId,
                courseId = courseId
            };
            var jsonEnroll = JsonConvert.SerializeObject(enroll);
            var content = new StringContent(jsonEnroll, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/enrollments", content);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var createdEnroll = JsonConvert.DeserializeObject<Enroll>(json);
                return createdEnroll;
            }
            else
            {
                throw new Exception($"Failed to enroll: {response.ReasonPhrase}");
            }
        }
    }
}
