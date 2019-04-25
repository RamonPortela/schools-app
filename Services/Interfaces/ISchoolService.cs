using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ISchoolService : IServiceBase<School>
    {
        Task<IEnumerable<School>> GetAllIncludingClassesAsync();
        Task<School> GetByIdIncludingClassesAsync(int id);
    }
}
