using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tut3.Models;
using System.Data.SqlClient;

namespace Tut3.Controllers

{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {

        [HttpGet]  //https://localhost:44308/api/students
        public IActionResult GetStudents()
        {
            var students = new List<Student>();

            using (var connection = new SqlConnection("Data Source=db-mssql;Initial Catalog=s19339;Integrated Security=True"))
            {
                using var command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "select firstname, lastname, birthdate,  name, semester from student, enrollment, studies where student.idenrollment = enrollment.idenrollment and studies.idstudy = enrollment.idstudy";
                connection.Open();
                SqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    var st = new Student();
                    st.FirstName = dr["FirstName"].ToString();
                    st.LastName = dr["LastName"].ToString();
                    st.BirthDate = dr["BirthDate"].ToString();
                    st.enrollment = new Enrollment
                    {
                        Semester = (int)(dr["Semester"]),
                        study = new Studies { Name = dr["Name"].ToString() }
                    };
                    students.Add(st);

                }
            }
            return Ok(students);
        }

        [HttpGet("{IndexNumber}")]  //https://localhost:44308/api/students/s236

        public IActionResult GetStudent(string IndexNumber)
        {

            using var connection = new SqlConnection("Data Source=db-mssql;Initial Catalog=s19339;Integrated Security=True");
            using (var command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText = "select semester from student, enrollment, studies where student.idenrollment = enrollment.idenrollment and studies.idstudy = enrollment.idstudy and indexNumber=" + "'" + @IndexNumber + "'";
                command.Parameters.AddWithValue("IndexNumber", IndexNumber);
                connection.Open();
                var dr = command.ExecuteReader();
                if (dr.Read())
                {
                    var en = new Enrollment();
                    var sem = en.Semester = (int)(dr["Semester"]);
                    return Ok("Student with Index Number " + IndexNumber + " is on " + sem + " semester");
                }
                else
                {
                    return Ok("There is no student with Index Number " + IndexNumber + " Please enter other Index number. ");
                }
            }
        }
    }
}




    //    [HttpPost]
    //    public IActionResult CreateStudent(Student student)
    //    {
    //        //... add to database
    //        //... generating index number
    //        student.IndexNumber = $"s{new Random().Next(1, 2000)}";
    //        return Ok(student);

//    }

//    [HttpDelete("{id}")]
//    public IActionResult DeleteStudent(int id)
//    {
//        return Ok("Deleted Completed");
//    }

//    [HttpPut("{id}")]
//    public IActionResult PutStudent(int id)
//    {
//        return Ok("Update completed");
//    }
//}
//}