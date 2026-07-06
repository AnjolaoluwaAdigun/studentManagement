using StudentManagement.Enums;
using StudentManagement.Helpers;
using StudentManagement.Managers.Interfaces;
using StudentManagement.Models;

namespace StudentManagement.Managers.Implementations
{
    public class AdminManager : IAdminManager
    {
        private static int _nextAdminId=1;
        private static List<User> _admins = new List<User>();

        private static int _nextInstructorId=1;
        private static List<Instructor> _instructors= new List<Instructor>();

        public AdminManager()
        {
            if(_admins.Count==0){
                var defaultAdmin= new User(_nextAdminId++,"Johanan", "Johanan@gmail.com", HashUtil.Hash("admin123"), Role.Admin);
                _admins.Add(defaultAdmin);

            }
        }

        public User CreateAdmin(string name,string email,string password)
        {
            var existing=GetAdminByEmail(email);
            if (existing != null)
            {
                Console.WriteLine("Admin already exists");
                return existing;
            }

            var newAdmins= new User(_nextAdminId++,name,email,HashUtil.Hash(password), Role.Admin);
            _admins.Add(newAdmins);
            Console.WriteLine($"{name} has been created.");
            return newAdmins;
        }
        public Instructor CreateInstructor(string name, string email, string password, string department)
        {
            var existing= GetInstructorByEmail(email);
            if(existing != null)
            {
                Console.WriteLine("Instructor already exists");
                return existing;
            }

            var newInstructor= new Instructor(_nextInstructorId++,name,email,HashUtil.Hash(password),department);
            _instructors.Add(newInstructor);
            Console.WriteLine($"{name} has been created.");
            return newInstructor;
            
        }
        public  User? GetAdminByEmail(string email)
        {
            return _admins.FirstOrDefault(a=>a.Email==email);
        }
        public Instructor? GetInstructorByEmail(string email)
        {
            return _instructors.FirstOrDefault(a=>a.Email==email);
        }

        public Instructor? GetInstructor(int id)
        {
            return _instructors.FirstOrDefault(a=>a.Id==id);
        }
        public bool LoginAdmin(string email, string password, out User? admin)
        {
            admin=GetAdminByEmail(email);
            if(admin==null) return false;
            return HashUtil.Verify(password,admin.Password);
        }

        public bool LoginInstructor(string email, string password, out Instructor? instructor)
        {
            instructor=GetInstructorByEmail(email);
            if(instructor== null) return false;
            return HashUtil.Verify(password,instructor.Password);
        }

        public List<User>  GetAllAdmins()
        {
            return _admins;
        }

        public List<Instructor> GetAllInstructors()
        {
            return _instructors;
        }

        public void OfferAdmission(int studentId, bool admit, IStudentManager studentManager)
        {
            var student= studentManager.GetStudent(studentId);
            if (student == null)
            {
                Console.WriteLine("Student not found.");
                return;
            }
           
            
        student.AdmissionStatus=admit? AdmissionStatus.Admitted: AdmissionStatus.Rejected;
        Console.WriteLine($"{student.Name} you have been {(admit? "admitted": "rejected")}!");
            
        }

    }


}
