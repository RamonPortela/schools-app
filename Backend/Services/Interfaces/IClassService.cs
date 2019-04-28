using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IClassService : IServiceBase<Class>
    {
        Task<IEnumerable<Class>> GetAllIncludingSchoolAsync();
        Task<Class> GetByIdIncludingSchoolAsync(int id);
        Task<IEnumerable<Class>> GetAllIncludingSchoolPaginatedAsync(int page, string search);
    }
}
