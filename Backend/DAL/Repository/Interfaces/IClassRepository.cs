using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface IClassRepository : IRepositoryAsync<Class>
    {
        Task<IEnumerable<Class>> GetAllIncludingSchoolAsync();
        Task<Class> GetByIdIncludingSchoolAsync(int id);
    }
}
