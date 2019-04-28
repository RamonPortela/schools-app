using DAL.DBConfig.EntityFramework;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class ClassRepository : RepositoryAsync<Class>, IClassRepository
    {
        public ClassRepository(ApplicationContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<Class>> GetAllIncludingSchoolAsync()
        {
            IQueryable<Class> query = await Task.FromResult(dbSet.Include(clasz => clasz.School));

            return query.AsEnumerable();
        }

        public async Task<IEnumerable<Class>> GetAllIncludingSchoolPaginatedAsync(int page, string search)
        {
            int pageSize = 5;
            return await Task.FromResult(dbSet.Where(x => x.Name.Contains(search ?? "")).Skip(pageSize * (page - 1)).Take(pageSize).Include(clasz => clasz.School));
        }

        public async Task<Class> GetByIdIncludingSchoolAsync(int id)
        {
            IQueryable<Class> query = await Task.FromResult(dbSet.Include(clasz => clasz.School));

            return query.SingleOrDefault(clasz => clasz.Id == id);
        }
    }
}
