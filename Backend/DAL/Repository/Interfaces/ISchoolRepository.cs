using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface ISchoolRepository : IRepositoryAsync<School>
    {
            Task<IEnumerable<School>> GetAllIncludingClassesAsync();
            Task<School> GetByIdIncludingClassesAsync(int id);
       
    }
}
