using DAL.Entities;
using DAL.Repository;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ClassService : ServiceBase<Class>, IClassService
    {
        private readonly IClassRepository _repository;

        public ClassService(IClassRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Class>> GetAllIncludingSchoolAsync()
        {
            return await _repository.GetAllIncludingSchoolAsync();
        }

        public async Task<IEnumerable<Class>> GetAllIncludingSchoolPaginatedAsync(int page, string search)
        {
            return await _repository.GetAllIncludingSchoolPaginatedAsync(page, search);
        }

        public async Task<Class> GetByIdIncludingSchoolAsync(int id)
        {
            return await _repository.GetByIdIncludingSchoolAsync(id);
        }
    }
}
