﻿using System.Collections.Generic;
using HefceBot.Models;

namespace HefceBot.Services
{
    public interface IUnistatsService
    {
        IEnumerable<Institution> GetInstitutions();
        Institution GetInstitution(string pubukprn);
        IEnumerable<Course> GetCoursesForInstitution(string pubukprn);
        IEnumerable<CourseWithInstitution> GetAllCourses();
    }
}