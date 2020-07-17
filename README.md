# APBD
2020, PJATK, Aplikacje baz danych

# Tutorial 1 - Introduction
Introduction to GitHub

# Tutorial 2 -XML, JSON
### 2.1.1 Task 1
In this task your goal will be to continue learning C# language and to prepare the console application, which is used for the data processing. 
Don’t forget to create a new repository for the task 2 and to place your code there. Remember about commands commit and push.
Lower you will find the application requirements. Turned out that University XYZ needs to export data and to properly prepare it for sending to the Ministry of Education.
Ministry created a system which lets you import the XML file of the certain format described by the Ministry. </br>
The IT system used at University XYZ allows data export only in the CSV file. </br>
Unfortunately, the exported data contains some errors or missing data. </br>
In addition, its format does not match the format expected by the ministry.</br>
We need to create a console application that will allow the correct processing of the received CSV file and receiving the resulting file compatible with the format expected by Ministry.
</br>
### 2.1.2 Program parameters
Program should take 3 parameters:
- Path of the CSV file
- Destination path
- Data format
Below there is an example of a call: :
.\export_xyz "C:\Users\Jan\Desktop\csvData.csv" </br>
"C:\Users\Jan\Desktop\wynik.xml" xml </br>
Below there is an example of a call:
- The default path of the CSV file is “data.csv”
- The default path of the CSV file is “data.csv”
- The default data type is “xml”
If the resulting file already exists in the given location - it should be overwritten. </br>
For now, the only available value for the third parameter is "xml". </br>
Expect that in the future there may be more output formats implemented in the application.</br>

### 2.2.1 Error handling
The application should be resistant to errors. All errors should be logged in to the txt file named “log.txt ". </br>
Also remember that error messages need to be understandable to the user.</br>
In case of:</br>
- The incorrect path - report error ArgumentException ("The path is incorrect").
- File does not exist – report error FileNotFoundException(“File does not exist”). </br>

### 2.3.1 Input data format
An example of a data file is shown below. Each record represents a single student. Each
column is separated by ",". Each student should be described by 9 columns. Here is an
example of a single record in the CSV file.</br>
Paweł,Nowak1,Informatyka dzienne,Dzienne,459,2000-02-12, 00:00:00.000,nowak@pjwstk.edu.pl,Alina,Adam</br>
Uwaga:
- Some records may contain errors. We omit those students who are not described by 9
data columns. We treat information about an omitted student as an error and log it
into the file “log.txt ".. </br>
- If one of the students has an empty value in the column - we treat this value as
missing. In this case, the student is not added to the result. Instead he is added to
the file “log.txt".</br>
In the above cases, you can define your own classes representing the error.</br>
In addition, it turned out that the data sometimes contain duplicate information about students. </br>
We must ensure that we do not add the student with the same name, surname and index number to the result twice. </br>
We always take the first student with given name, surname and index number. </br>
Every repetition of student data in source data is treated as an invalid duplicate.</br>

### 2.3.2 Target format
The ministry has provided the following example of a file showing the target input data
format in the form of an XML file.
<university</br>
createdAt="08.03.2020"</br>
author="Jan Kowalski"> - name and surname</br>
<students>
<student indexNumber="s1234">
<fname>Jan</fname>
<lname>Kowalski</lname>
<birthdate>03.04.1984</birthdate>
<email>kowalski@wp.pl</email>
<mothersName>Alina</mothersName>
<fathersName>Andrzej</fathersName>
<studies> 
<name>Computer Science</name> 
<mode>Stationary</mode> 
</studies> 
</student> 
<student indexNumber="s3455"> 
<fname>Anna</fname> 
<lname>Malewska</lname> 
<birthdate>09.07.1988</birthdate> 
<email>kowalski@wp.pl</email> 
<mothersName>Anna</mothersName> 
<fathersName>Michał</fathersName> 
<studies> 
<name>New Media Art</name> 
<mode>Non-stationary</mode> 
</studies> 
</student> 
</students> 
<activeStudies> 
<studies name="Computer Science" numberOfStudents="1" /> 
<studies name="New Media Art" numberOfStudents="1" /> 
</activeStudies> 
</university> 

### 2.4.1 Additional task
It turned out that customer requirements have changed. In order to limit the data sent, the Ministry prepared a new data format.</br>
This time it was decided to use the JSON format.</br>
Try adding to your code the ability to pass the "json" format call as the third parameter.</br>
How do you split your code into classes? </br>
Assuming that in the future the Ministry may introduce further data formats - how will you prepare the application in such a way that it is as flexible as possible and can be easily extended in the future?
{ </br>
university: { </br>
createdAt: "08.03.2020", </br>
author: "Jan Kowalski", </br>
students: [ </br>
{ </br>
indexNumber: "s1234", </br>
fname: "Jan", </br>
lname: "Kowalski", </br>
birthdate: "02.05.1980", </br>
email: "kowalski@wp.pl", </br>
mothersName: "Alina", </br>
fathersName: "Jan", </br>
studies: { </br>
name: "Computer Science", </br>
mode: "Dzienne" </br>
} </br>
}, </br>
{ </br>
indexNumber: "s2432", </br>
fname: "Anna", </br>
lname: "Malewska", </br>
birthdate: "07.10.1985", </br>
email: "malewska@wp.pl", </br>
mothersName: "Marta", </br>
fathersName: "Marcin", </br>
studies: { </br>
name: "New Media Art", </br>
mode: "Zaoczne" </br>
} </br>
} </br>
], </br>
activeStudies: [ </br>
{ </br>
name: "Computer Science", </br>
numberOfStudents: "1" </br>
}, </br>
{ </br>
name: "New Media Art", </br>
numberOfStudents: "2" </br>
} </br>
] </br>
} </br>
} </br>

# Tutorial 3 - Postman, Controllers, Http Get, Http Post
### 3.1 Task 1 - Postman installation
During this exercise, we will create a new Web API application. When testing a web application, we’ll need an additional application called Postman. The application is free and
exists both in the form of a plugin for the Chrome browser or a separate desktop application.
Please install it. The most convenient way to use the desktop version. Address URL:
https://www.postman.com/downloads/
### 3.2 Task 2 - New project creation
1. Create a new repository for tutorial 3.
2. Then, create a new project ASP.NET Core Web Application. From the available templates choose API.
3. After creating a new project you should delete Controllers/WeatherForecastController.cs
1
Figure 2: Available templates
Task 3 - Adding new controller
1. In this task we will add our first controller. The controller processes user requests and
returns the expected result.
2. We add a new controller called StudentsController.cs to the Controllers folder.
3. When we think of API and REST approach - we should think about the resources
that our API provides via HTTP. Controller names usually reflect the resources being
shared.
4. Then please modify the code so that our class is ready to handle HTTP REST requests.
5. We add two attributes above the class name. The first of these [ApiController]
marks our controller as an API controller. Thanks to this, we will be able to use
2
several built-in validation mechanisms that will be useful later. The [Route] attribute
allows you to specify the address that our controller will identify. All requests sent to
this address will be automatically redirected to this controller.
6. Then we change the base class - we should inherit from ControllerBase. The ControllerBase
class contains many useful helper methods that we will use.
7. The Index() method has stopped compiling. At this point, we are adding the first
method that will return data. We modify the Index() method as follows.
3
8. Then let’s try to run the application. Then try to enter https://localhost:44316/api/students.
Please make the same HTTP request using Postman. Remember that in your case the
address may differ (44316 is a port number and may be different).
4
Task 4 - passing parameter as a URL segment
1. By selecting the appropriate URL and HTTP method (e.g. GET, PUT, POST,
DELETE, PATCH) we can choose which method will be run. Usually, however, we
need to send some additional data to the server. We have several ways to do this. The
first is the use of the URL segment.
2. The URL segment is selected when the information sent is related to the identification
of a resource (similar to the id in the database). I.e. it can be a student id from the
database. It would be bad, however, to use the URL segment to send i.e. information
on how we want to sort the data.
3. Please add a new method that will allow us to pass the parameter. As you can see in the
HttpGet attribute, we’ve added the id parameter. In addition, the added method also
has a parameter with the same name. Then, when making the request, the parameter
from the corresponding part of the URL will be automatically passed to the appropriate
method.
Figure 3: Running the application
4. In addition, we use the IActionResult interface here. It is an interface used to return
various types of data - text, JSON files, XML and others. In our case, we use two additional methods Ok() and NotFound(). Both methods come from the ControllerBase
superclass. They allow us to easily return data and "wrap" them in the appropriate
response and HTTP code (eg 200 OK or 404 Not Found).
5. Then test the method with the help of the Postman app. Please try to transfer data
in different ways and check what the effect will be.
