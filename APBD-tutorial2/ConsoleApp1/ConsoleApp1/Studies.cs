using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace ConsoleApp1
{

    public class Studies
    {
        [JsonPropertyName("name")]
        [XmlElement(elementName: "name")]
        public string name { set; get; }

        [JsonPropertyName("mode")]
        [XmlElement(elementName: "mode")]
        public string mode { set; get; }

        public Studies() { }
  
        public void SetName(string Name)
        {
            if (string.IsNullOrEmpty(Name))
            {
                throw new Exception("Name cannot be null or empty");
            }
            this.name = Name;
        }

        public string GetName()
        {
            if (string.IsNullOrEmpty(this.name))
            {
                return "No Name";
            }
            else
            {
                return this.name;
            }
        }

        public void SetMode(string Mode)
        {
            if (string.IsNullOrEmpty(Mode))
            {
                throw new Exception("Mode cannot be null or empty");
            }
            this.mode = Mode;
        }

        public string GetMode()
        {
            if (string.IsNullOrEmpty(this.mode))
            {
                return "No mode";
            }
            else
            {
                return this.mode;
            }
        }


    }
}
