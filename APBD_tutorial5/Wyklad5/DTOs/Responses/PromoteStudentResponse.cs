﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wyklad5.DTOs.Responses
{
    public class PromoteStudentResponse
    {
        public int IdEnrollment { get; set; }
        public string StudyName { get; set; }
        public int Semester { get; set; }
    }
}
