﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tut3.Models
{
    public class Enrollment
    {
        public int IdStudy { get; set; }
        public int IdEnrollment { get; set; }
        public int Semester { get; set; }


        public Studies study { get; set; }
    }
}