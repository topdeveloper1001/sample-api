using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SampleApi.Domain;

namespace SampleApi.Services
{
    public interface IStudentService
    {
        Task<List<School>> GetSchools();
        Task<School> GetSchool(Guid schoolId);
        Task<List<Student>> GetSchoolStudents(Guid schoolId);
        Task CreateStudent(Student student);
    }

    public class StudentService : IStudentService
    {
        private readonly SampleDbContext _dbContext;
        private readonly ILogger<StudentService> _logger;

        public StudentService(SampleDbContext dbContext, ILogger<StudentService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<List<School>> GetSchools()
        {
            return await _dbContext.Schools.ToListAsync();
        }

        public async Task<School> GetSchool(Guid schoolId)
        {
            return await _dbContext.Schools.FirstOrDefaultAsync(s => s.Id == schoolId);
        }

        public async Task<List<Student>> GetSchoolStudents(Guid schoolId)
        {
            return await _dbContext.Students.Where(s => s.SchoolId == schoolId).ToListAsync();
        }

        public async Task CreateStudent(Student student)
        {
            _dbContext.Students.Add(student);
            await _dbContext.SaveChangesAsync();
        }

    }
}
