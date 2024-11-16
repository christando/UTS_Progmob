using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace UTS_72210454.Data
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://actualbackendapp.azurewebsites.net/");
        }

        //Categories
        public async Task<IEnumerable<Categories>> GetCategoriesAsync()
        {
            var category = await _httpClient.GetFromJsonAsync<List<Categories>>("api/v1/categories");
            return category;
        }

        public async Task<Categories> addCategory(string name, string description)
        {
            var category = new Categories()
            {
                name = name,
                description = description
            };

            var jsonCategory = JsonConvert.SerializeObject(category);

            var content = new StringContent(jsonCategory, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/v1/categories", content);

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
            var response = await _httpClient.GetAsync($"api/v1/categories/{categoryId}");
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
            // Create an HttpRequestMessage for the PUT request
            var jsonCategory = JsonConvert.SerializeObject(category);
            var content = new StringContent(jsonCategory, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/v1/categories/{category.categoryId}", content);

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
            var response = await _httpClient.DeleteAsync($"api/v1/Categories/{categoryId}");


            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to delete category APIService: {response.ReasonPhrase}");
            }
        }


        //Courses
        public async Task<IEnumerable<Courses>> GetCoursesAsync()
        {
            var course = await _httpClient.GetFromJsonAsync<List<Courses>>("api/Courses");
            return course;
        }

        public async Task<AddCourse> AddCourse(string name, string imageName, int duration, string description, int categoryId)
        {

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
            var response = await _httpClient.PostAsync("api/Courses", content);

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
            var jsonCategory = JsonConvert.SerializeObject(course);
            var content = new StringContent(jsonCategory, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/Courses/{course.courseId}", content);

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
            var response = await _httpClient.DeleteAsync($"api/Courses/{courseId}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to delete course: {response.ReasonPhrase}");
            }
        }

        public async Task<Dictionary<string, List<Courses>>> GetCoursesByCategoryAsync()
        {
            // Get the courses from the API
            var courses = await _httpClient.GetFromJsonAsync<List<Courses>>("api/Courses");

            // Group the courses by the category's name
            var groupedCourses = courses.Where(c => c.Category != null) // Ensure the category object exists
                .GroupBy(c => c.Category.name)  // Use the 'name' from the Category object
                .ToDictionary(g => g.Key, g => g.ToList());

            return groupedCourses;
        }

        public async Task<List<Courses>> GetByName(string name)
        {
            var response = await _httpClient.GetAsync($"api/Courses/search/{name}");
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


    }
}
