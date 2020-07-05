using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tutorial3.Models;

namespace tutorial3.IDL
{
    public class MockDBService : IdbService
    {
        private static IEnumerable<Student> _students;
        static MockDBService() {
            _students = new List<Student>
        {
            new Student{IdStudent=1, FirstName="Jan", LastName="Kowalski"},
            new Student{IdStudent=2, FirstName="Stefan", LastName="Walski"},
            new Student{IdStudent=3, FirstName="Alex", LastName="Starski"}
        };

        }

        public IEnumerable<Student> GetStudents(string orderBy) {
            if (orderBy == "name")
                _students.OrderBy(Student => Student.FirstName);
            if (orderBy == "surname")
                _students.OrderBy(Student => Student.LastName);
            if (orderBy == "index")
                return _students.OrderBy(Student => Student.IndexNumber);
            else return _students.OrderBy(Student => Student.IdStudent);
        }

        public Student GetStudent(string id) {
            foreach (Student student in _students)
            {
                if (student.IndexNumber == id) return student;
            }
            return null;
        }
        public bool AddStudent(Student st)
        {
            if (st != null) { _students.Append(st); return true; }
            else return false;

        }
        public void UpdateStudent(Student st) {
            Student s1 = _students.First(x => x.IndexNumber == st.IndexNumber);
            s1.LastName = "Updated";
            _students = _students.Where(u => u.IndexNumber != st.IndexNumber).ToList();
            _students.Append(s1);

        }
        public void DeleteStudent(string index) {
            _students = _students.Where(u => u.IndexNumber != index).ToList();
        }
          public void DeleteStudent(Student s) {
            _students = _students.Where(u => u != s).ToList();
    }

    public IEnumerable<Student> GetStudents() { return _students; }
    }
}
