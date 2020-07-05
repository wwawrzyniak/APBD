using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tutorial3.Models;

namespace tutorial3.IDL
{
    public interface IdbService
    {
         public IEnumerable<Student> GetStudents (string orderBy);
         public void UpdateStudent (Student s);
         public void DeleteStudent (string i);
        public void DeleteStudent(Student i);
        public Student GetStudent (string i);
         public bool AddStudent (Student s);

        public IEnumerable<Student> GetStudents();

    }
}
