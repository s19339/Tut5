using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Tut3.DTOs.Requests;
using Tut3.DTOs.Responses;
using Tut3.Services;

namespace Tut3.Controllers
{
    [Route("api/enrollments")]
    [ApiController] 
    public class EnrollmentsController : ControllerBase
    {
        
        private IStudentServiceDb _service;

       
        public EnrollmentsController(IStudentServiceDb service)
        {
            _service = service;
        }

        [HttpPost(Name = "EnrollStudents")]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {
            _service.EnrollStudent(request);

            var response = new EnrollStudentResponse();
            using (var connection = new SqlConnection("Data Source=db-mssql;Initial Catalog=s19339;Integrated Security=True")) 
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "Select * From Studies Where Name = @Name";
                    command.Parameters.AddWithValue("Name", request.Studies);
                    connection.Open();

                    var trans = connection.BeginTransaction();
                    command.Transaction = trans;
                    var dr = command.ExecuteReader();

                    if (!dr.Read())
                    {
                        dr.Close();
                        trans.Rollback();
                        return BadRequest("The studies does not exist");
                    }

                    int idStudy = (int)dr["IdStudy"];

                    dr.Close();

                    command.CommandText = "Select * From Enrollment Where Semester = 2021 And IdStudy = @idStudy";
                    int IdEnrollment = (int)dr["IdEnrollemnt"] + 1;
                    command.Parameters.AddWithValue("IdStudy", idStudy);
                    dr = command.ExecuteReader();

                    if (dr.Read())
                    {
                        dr.Close();
                        command.CommandText = "Select MAX(IdEnrollment) as 'IdEnrollment' From Enrollment";
                        dr = command.ExecuteReader();
                        dr.Close();

                        command.CommandText = "Insert Into Enrollment(IdEnrollment, Semester, IdStudy) Values (@IdEnrollemnt, 2021, @IdStudy)";
                        command.Parameters.AddWithValue("IdEnrollemnt", IdEnrollment);
                        command.ExecuteNonQuery();
                    }

                    dr.Close();

                    command.CommandText = "Select * From Student Where IndexNumber=@IndexNumber";
                    command.Parameters.AddWithValue("IndexNumber", request.IndexNumber);
                    dr = command.ExecuteReader();

                }

                return Ok();
            }
        }


        [HttpPost("promote")]
        public IActionResult PromoteStudents()
        {
            
            _service.PromoteStudents(1, "IT");

            return Ok();
        }
    }

}