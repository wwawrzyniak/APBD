using System;
using System.Collections.Generic;

namespace Apbd_example_tutorial_10.Entities
{
    public partial class Enrollment
    {
        public Enrollment()
        {
            Student = new HashSet<Student>();
        }

        public int IdEnrollment { get; set; }
        public int Semester { get; set; }
        public int IdStudy { get; set; }
        public DateTime StartDate { get; set; }

        public virtual Studies IdStudyNavigation { get; set; }
        public virtual ICollection<Student> Student { get; set; }
    }
}
