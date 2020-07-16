using System;
using System.Collections.Generic;

namespace ConsoleApp1.Models
{
    public class CustomComparer : IEqualityComparer<Student>
    {
        public bool Equals(Student x, Student y)
        {
            return StringComparer.InvariantCultureIgnoreCase
                .Equals($"{x.Email}{x.StudentIndex}{x.FirstName}{x.LastName}",
                    $"{y.Email}{y.StudentIndex}{y.FirstName}{y.LastName}");
        }

        public int GetHashCode(Student x)
        {
            return StringComparer
                .InvariantCultureIgnoreCase
                .GetHashCode($"{x.Email}{x.StudentIndex}{x.FirstName}{x.LastName}");
        }
    }
}