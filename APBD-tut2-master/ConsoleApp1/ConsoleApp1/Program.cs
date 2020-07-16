using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using ConsoleApp1.Models;

namespace ConsoleApp1
{

    class Program
    {
        static void Main(string[] args)
        {
            var listOfStudents = new HashSet<Student>(new CustomComparer());
            //error log
            using var streamWriter = new StreamWriter(@"log.txt");
            string path,outpath,format;
                //default xml, dane/dane.csv , result.xml
               format = args.Length > 2 ? args[2] : "xml";
            // path = args.Length > 0 ? args[0] : @"Dane/dane.csv"; TODO: UNCOMMENT
            path = @"C:\Users\Weronika Wawrzyniak\Desktop\APBD-tut2\ConsoleApp1\ConsoleApp1\Dane\dane.csv";
            outpath = args.Length > 1 ? args[1] : $"output/result.{format}";


          
            if (!File.Exists(path))
            {
                streamWriter.WriteLine("Data source doesnt exist");
            }
            if (!File.Exists(outpath))
            {
                streamWriter.WriteLine("Output file doesnt exist - creating a new one ");
            }
            else streamWriter.WriteLine("Output file exists - override");


            var fi = new FileInfo(path);
            using (var stream = new StreamReader(fi.OpenRead()))
            {
                string line = null;
                //creating a new student from the stream
                while ((line = stream.ReadLine()) != null)
                {
                    string[] columns = line.Split(',');
                    //only if number of columns is 9
                    if (columns.GetLength(0) == 9)
                    {
                        Boolean allGood = true;
                        //and if none of the columns is empty
                        foreach (string c in columns) if (string.IsNullOrWhiteSpace(c)) allGood = false;
                        if (allGood == true)
                        {
                            var buffStudent = new Student
                            {
                                FirstName = columns[0],
                                LastName = columns[1],
                                BirthDate = DateTime.Parse(columns[5]),
                                Studies = new Studies
                                {
                                    mode = columns[3],
                                    name = columns[2]
                                },
                                Email = columns[6],
                                MothersName = columns[7],
                                FathersName = columns[8],
                                StudentIndex = columns[4]
                            };
                            //will be added to the list only if there are no duplicates
                            if (listOfStudents.Contains(buffStudent))
                            {
                                //duplicate error into log.txt
                             
                                streamWriter.WriteLine($"Student with the firstName: {buffStudent.FirstName} was not added due to duplicate");
                            }
                            else listOfStudents.Add(buffStudent);
                        }
                        else
                        {  //empty columns error into log.txt
                            streamWriter.WriteLine("One or more columns empty");
                        }
                    }
                    else
                    {  //not 9 columns error into log.txt
                        streamWriter.WriteLine("Not described by 9 data columns.");
                    }

                }
            }
            activeStudies ac = new activeStudies() { studentsList = listOfStudents, };
            University uni = new University() { students = listOfStudents ,pary=ac.countSubjects()};
            uni.random();
            string author = "Weronika_Wawrzyniak_s19515";
            DateTime time = DateTime.Now;
            string message = ($"university_created_at_{time}_author_{author}");
            switch (format)
            {
                case "xml":
                  //  var writer = new FileStream(outpath, FileMode.Create); 
                   var writer = new FileStream("result.xml", FileMode.Create);
                   var serialaizer = new XmlSerializer(typeof(University), new XmlRootAttribute(message));
                      serialaizer.Serialize(writer, uni);
                    break;
                case "json":
                    var jsonString = JsonSerializer.Serialize(listOfStudents);
                    File.WriteAllText("data.json", jsonString);
                    break;
                default:
                    streamWriter.WriteLine("Invalid path");
                    break;

            }
        }
    }
}