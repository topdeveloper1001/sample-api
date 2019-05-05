using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleApi.Domain
{
    public class SchoolDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public static SchoolDto Map(School school)
        {
            return new SchoolDto
            {
                Id = school.Id,
                Name = school.Name
            };
        }

        public static List<SchoolDto> Map(IEnumerable<School> schools)
        {
            return schools.Select(Map).ToList();
        }

    }
}
