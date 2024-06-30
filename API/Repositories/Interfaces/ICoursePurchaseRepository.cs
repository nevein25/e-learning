﻿using API.DTOs;

namespace API.Repositories.Interfaces
{
    public interface ICoursePurchaseRepository
    {
        Task<IList<CoursesPurshasedDto>> CoursesBoughtByStudentId(int studentId);
        Task<bool> IsCourseBoughtAsync(int courseId, int studentId);
        Task<IList<CourseUploadedDto>> CoursesUploadedByInstructor(int instructorId);

    }
}
