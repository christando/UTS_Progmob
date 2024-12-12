using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UTS_72210454.Data
{
    public class userInstructor
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }

        // Data dari Instructor
        public int? InstructorId { get; set; }

        public userInstructor(User user, Instructor instructor)
        {
            Email = user.email;
            UserName = user.userName;
            Password = user.password;
            FullName = user.fullName;

            // Jika instruktur ada, maka set InstructorId
            if (instructor != null)
            {
                InstructorId = instructor.instructorId;
            }


            
        }
    }
}
