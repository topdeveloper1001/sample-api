using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleApi.Domain
{
    public class StudentDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid SchoolId { get; set; }
        public DateTimeOffset CreatedTime { get; set; }

        public static StudentDto Map(Student student)
        {
            return new StudentDto
            {
                Id = student.Id,
                Name = student.Name,
                SchoolId = student.SchoolId,
                CreatedTime = student.CreatedTime
            };
        }

        public static List<StudentDto> Map(IEnumerable<Student> students)
        {
            return students.Select(Map).ToList();
        }

    }
}
