using DAL.Entities;
using DAL.Repository;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class SchoolService : ServiceBase<School>, ISchoolService
    {
        private readonly ISchoolRepository _repository;

        public SchoolService(ISchoolRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<School>> GetAllIncludingClassesAsync()
        {
            return await _repository.GetAllIncludingClassesAsync();
        }

        public async Task<School> GetByIdIncludingClassesAsync(int id)
        {
            return await _repository.GetByIdIncludingClassesAsync(id);
        }
    }
}
