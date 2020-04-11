using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Tut3.DTOs.Requests
{
    public class EnrollStudentRequest
    {
        public string IndexNumber { get; set; }

        [Required]
        [MaxLength(20)]

        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        [Required]

        public string Studies { get; set; }

    }
}
