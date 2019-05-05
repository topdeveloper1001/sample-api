using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SampleApi.Domain
{
    public class School
    {
        public Guid Id { get; set; }
        [Required(AllowEmptyStrings = true)]
        [StringLength(64)]
        public string Name { get; set; }

        public List<Student> Students { get; set; }
    }
}
