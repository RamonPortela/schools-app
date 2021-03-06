﻿using DAL.DBConfig.EntityFramework;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class SchoolRepository : RepositoryAsync<School>, ISchoolRepository
    {
        public SchoolRepository(ApplicationContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<School>> GetAllIncludingClassesAsync()
        {
            IQueryable<School> query = await Task.FromResult(dbSet.Include(school => school.Classes));

            return query.AsEnumerable();
        }

        public async Task<School> GetByIdIncludingClassesAsync(int id)
        {
            IQueryable<School> query = await Task.FromResult(dbSet.Include(school => school.Classes));

            return query.SingleOrDefault(school => school.Id == id);
        }

        public override async Task<IEnumerable<School>> GetPaginatedAsync(int page, string search)
        {
            int pageSize = 5;
            return await Task.FromResult(dbSet.Where(x => x.Name.Contains(search ?? "")).Skip(pageSize * (page - 1)).Take(pageSize).Include(school => school.Classes));
        }
    }
}
