using ConsoleApp1;
using ConsoleApp1.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;


namespace ConsoleApp1
{
    [XmlType(TypeName = "activeStudies")]
    public class activeStudies
    {
        public List<string> activeSubjects = new List<string>();
        public HashSet<Student> studentsList = new HashSet<Student>(new CustomComparer());
        public HashSet<Pair> pary = new HashSet<Pair>();
        public HashSet<Pair> countSubjects()
        {
            foreach (Student s in studentsList)
            {
                activeSubjects.Add(s.studName);
            }


            var KeyValuePair = activeSubjects
                .GroupBy(i => i)
                .ToDictionary(g => g.Key, g => g.Count());


            pary = Pair.assign(KeyValuePair);
            return pary;
        }


    }
}

