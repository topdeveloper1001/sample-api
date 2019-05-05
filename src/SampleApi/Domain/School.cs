using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleApi.Domain
{
    [Table("WP_Schools")]
    public class School
    {
        public Guid Id { get; set; }
        [Required(AllowEmptyStrings = true)]
        [StringLength(64)]
        public string Name { get; set; }

        public List<Student> Students { get; set; }
    }
}
