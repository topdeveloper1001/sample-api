using System;
using System.ComponentModel.DataAnnotations;

namespace SampleApi.Domain
{
    public class Student
    {
        public Guid Id { get; set; }
        [Required(AllowEmptyStrings = true)]
        [StringLength(128)]
        public string Name { get; set; }
        public Guid SchoolId { get; set; }
        public DateTimeOffset CreatedTime { get; set; }

        public School School { get; set; }
    }
}
