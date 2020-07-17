using ConsoleApp1.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace ConsoleApp1
{
    [XmlRoot("Students")]
    public class Students
    {
       
        [XmlArrayItem("student")]
       // [JsonPropertyName("student")]
        public HashSet<Student> students = new HashSet<Student>(new CustomComparer());

       
       
    }
}