using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tut3.DTOs.Requests;
using Tut3.DTOs.Responses;
using Tut3.Models;
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

        [HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {
            _service.EnrollStudent(request);

            var response = new EnrollStudentResponse();
            return Ok(response);
        }

        [HttpPost("promote")]
        public IActionResult PromoteStudents()
        {
            
            _service.PromoteStudents(1, "IT");

            return Ok();
        }
    }

}