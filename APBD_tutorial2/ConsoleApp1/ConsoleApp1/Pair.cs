using System;
using System.Collections.Generic;
using System.Text;
using ConsoleApp1.Models;
using System;
using System.Xml.Serialization;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ConsoleApp1
{
   
    public class Pair
    {
       [XmlAttribute("index")]
       [JsonPropertyName("index")]
        int indexes { get; set; }
        [JsonPropertyName("name")]
        [XmlAttribute("name")]
        string name { get; set; }
        

        static public HashSet<Pair> assign(Dictionary<string,int> list)
        {
            HashSet<Pair> pairs = new HashSet<Pair>();
            foreach (KeyValuePair<string, int> x in list)
            {
                pairs.Add(new Pair { name = x.Key, 
                    indexes = x.Value});

            }

            return pairs;
        }

        

        public override string ToString() { return $"Name = {name} Indexes = {indexes}"; }
    }
}
