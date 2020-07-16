using ConsoleApp1.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ConsoleApp1
{
    [Serializable]
    [XmlRoot("University")]
    public class University
    {
        [XmlArrayItem("student")]
        public HashSet<Student> students = new HashSet<Student>(new CustomComparer());

        [XmlIgnore]
        public HashSet<Pair> pary { get; set; }
        [XmlArray("activeSubjects"),XmlArrayItem("Enrolled")]
        [JsonPropertyName("activeSubjects")]
        public HashSet<String> pary2 { get; set; }
       public void random()
        {
            this.pary2 = new HashSet<string>();
            foreach (Pair i in pary)
            {
                this.pary2.Add(i.ToString());
            }



        }

    }
}
