using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SampleApi.Domain;
using SampleApi.Dto;
using SampleApi.Services;
using Willow.Infrastructure.Services;

namespace SampleApi.Controllers
{
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    public class StudentsController : ControllerBase
    {
        private readonly IDateTimeService _dateTimeService;
        private readonly IStudentService _studentService;

        public StudentsController(IDateTimeService dateTimeService, IStudentService studentService)
        {
            _dateTimeService = dateTimeService;
            _studentService = studentService;
        }

        [HttpGet("schools")]
        [ProducesResponseType(typeof(List<SchoolDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSchools()
        {
            var schools = await _studentService.GetSchools();
            return Ok(SchoolDto.Map(schools));
        }

        [HttpGet("schools/{schoolId}/students")]
        [ProducesResponseType(typeof(List<StudentDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetSchoolStudents([FromRoute] Guid schoolId)
        {
            var school = await _studentService.GetSchool(schoolId);
            if (school == null)
            {
                return NotFound();
            }

            var students = await _studentService.GetSchoolStudents(schoolId);
            return Ok(StudentDto.Map(students));
        }

        [HttpPost("schools/{schoolId}/students")]
        [ProducesResponseType(typeof(StudentDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> CreateStudent([FromRoute] Guid schoolId, [FromBody] CreateStudentRequest request)
        {
            var school = await _studentService.GetSchool(schoolId);
            if (school == null)
            {
                return NotFound();
            }

            var student = new Student
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                SchoolId = schoolId,
                CreatedTime = _dateTimeService.UtcNow
            };
            await _studentService.CreateStudent(student);
            return Ok(StudentDto.Map(student));
        }

        public class CreateStudentRequest
        {
            public string Name { get; set; }
        }
    }
}