using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tut3.DTOs.Requests;

namespace Tut3.Services
{
   public interface IStudentServiceDb
    {
        void EnrollStudent(EnrollStudentRequest req);
        void PromoteStudents(int semester, string studies);
    }
}
