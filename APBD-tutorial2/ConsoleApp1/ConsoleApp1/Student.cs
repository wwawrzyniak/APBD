using System;
using System.Collections;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace ConsoleApp1.Models
{
    public class Student
    {
        [JsonPropertyName("fname")]
        [XmlElement(elementName: "fname")]
        public string FirstName { get; set; }
        [JsonPropertyName("lname")]
        [XmlElement(elementName: "lname")]
        public string LastName { get; set; }
        [JsonPropertyName("bdate")]
        [XmlElement(elementName: "birthdate")]
        public DateTime BirthDate { get; set; }
        [JsonPropertyName("indexNumber")]
        [XmlAttribute(attributeName: "indexNumber")]
        public string StudentIndex { get; set; }
        private string _email;
        [JsonPropertyName("mothersName")]
        [XmlElement(elementName: "mothersName")]
        public string MothersName { get; set; }
        [XmlElement(elementName: "fathersName")]
        [JsonPropertyName("fathersName")]
        public string FathersName { get; set; }
        [XmlElement(elementName: "email")]
        [JsonPropertyName("email")]
        public string Email
        {
            get => this._email;
            set => this._email = value ?? throw new ArgumentNullException();
        }
       [XmlElement(ElementName = "studies")]
        public Studies Studies { get => studies1; set => studies1 = value ?? throw new ArgumentNullException(); }

        private Studies studies1;

        public string studName { get => studies1.name; }
        public override string ToString()
        {
            return FirstName.ToString();
        }

        
    }
}