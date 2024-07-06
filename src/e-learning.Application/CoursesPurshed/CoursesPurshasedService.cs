using AutoMapper;
using e_learning.Application.CoursesPurshed.DTOs;
using e_learning.Domain.Entities;
using e_learning.Domain.Repositories;

namespace e_learning.Application.CoursesPurshed
{
    internal class CoursesPurshasedService : ICoursesPurshasedService
    {
        private readonly ICoursesPurshasedRepository _coursesPurshasedRepository;
        private readonly IMapper _mapper;

        public CoursesPurshasedService(ICoursesPurshasedRepository coursesPurshasedRepository, IMapper mapper)
        {
            _coursesPurshasedRepository = coursesPurshasedRepository;
            _mapper = mapper;
        }
        public async Task<IList<CoursesPurshasedDto>> CoursesBoughtByStudentId(int studentId)
        {
            var coursePurchases = await _coursesPurshasedRepository.GetCoursePurchasesByStudentIdAsync(studentId);

            var courseIds = coursePurchases
                .Select(cp => int.TryParse(cp.CourseId, out var id) ? (int?)id : null)
                .Where(id => id.HasValue)
                .Select(id => id.Value)
                .ToList();

            var courses = await _coursesPurshasedRepository.GetCoursesByIdsAsync(courseIds);

            return courses.Select(c => new CoursesPurshasedDto
            {
                CourseId = c.Id,
                CourseName = c.Name,
                Thumbnail = c.Thumbnail,
                Description = c.Description
            }).ToList();

         

        }

        public async Task<bool> IsCourseBoughtAsync(int courseId, int studentId)
        {
            return await _coursesPurshasedRepository.IsCourseBoughtExisitAsync(courseId, studentId);
        }

        public async Task<bool> IsStudentFinishedCourseAsync(int studentId, int courseId)
        {
            var coursePurchase = await _coursesPurshasedRepository.GetCoursePurchaseAsync(studentId, courseId);
            return coursePurchase?.IsFinished ?? false;
        }

        public async Task<IList<CourseUploadedDto>> CoursesUploadedByInstructor(int instructorId)
        {
            var coursesUploaded = await _coursesPurshasedRepository.GetCoursesByInstructorIdAsync(instructorId);


            var courses = _mapper.Map<IList<CourseUploadedDto>>(coursesUploaded);

            foreach (var course in courses)
            {
                course.NumberOfEnrolledStudents = await NumberOfStudentsEnrolledByCourseIdAsync(course.Id);
            }

            return courses;

        }

        private async Task<int> NumberOfStudentsEnrolledByCourseIdAsync(int courseId)
        {
            int numberOfStudentsEnrolled = 0;
            numberOfStudentsEnrolled = (await _coursesPurshasedRepository.GetCoursePurchasesByCourseIdAsync(courseId.ToString())).Count();
            return numberOfStudentsEnrolled;
        }

        public async Task<CoursePurchase> GetEnrollmentAsync(int courseId, int UserId)
        {

            return await _coursesPurshasedRepository.GetCoursePurchaseAsync(UserId, courseId);

        }

        public async Task<List<int>> GetVisitedLessonsByCourseId(string courseId)
        {
            var enrollment = await _coursesPurshasedRepository.GetCoursePurchasesByCourseId(courseId);
            if (enrollment != null)
            {
                return enrollment.GetVisitedLessons();
            }
            else
            {
                return new List<int>();
            }
        }


        public async Task<EnrollmentOperationResult> AddOrUpdateEnrollment(EnrollmentDto enrollmentDto, int userId)
        {
            enrollmentDto.StudentId = userId;
            var existingEnrollment = await _coursesPurshasedRepository.GetCoursePurchaseAsync(enrollmentDto.StudentId, int.Parse(enrollmentDto.CourseId));

            if (existingEnrollment != null)
            {
                existingEnrollment.IsFinished = enrollmentDto.IsFinished;
                existingEnrollment.Progress = enrollmentDto.Progress;

                existingEnrollment.SetVisitedLessons(enrollmentDto.VisitedLessons);

                if (existingEnrollment.Progress == 100)
                {
                    existingEnrollment.FinishedDate = DateOnly.FromDateTime(DateTime.Now);
                }
                var updateResult = await _coursesPurshasedRepository.UpdateCoursePurchaseAsync(existingEnrollment);

                return new EnrollmentOperationResult
                {
                    IsSuccessful = updateResult,
                    Message = updateResult ? "Updated Successfully" : "Error updating enrollment"
                };
            }
            else
            {
                var newEnrollment = _mapper.Map<CoursePurchase>(enrollmentDto);

                newEnrollment.UserId = userId;
                newEnrollment.SetVisitedLessons(enrollmentDto.VisitedLessons);

                var addResult = await _coursesPurshasedRepository.AddCoursePurchaseAsync(newEnrollment);

                return new EnrollmentOperationResult
                {
                    IsSuccessful = addResult,
                    Message = addResult ? "Added Successfully" : "Error adding enrollment"
                };
            }
        }
    }
}
