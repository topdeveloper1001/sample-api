using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleApi.Domain
{
    [Table("WP_Students")]
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
