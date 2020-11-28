using System.Collections.Generic;
using System.Threading.Tasks;
using Covid.Server.Data;
using Covid.Server.Entities;
using Covid.Server.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Covid.Server.Services
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly MyDbContext _myDbContext;

        public DepartmentRepository(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }
        public async Task<IList<Department>> GetAllAsync()
        {
            var all = await _myDbContext.Departments.ToListAsync();
            return all;
        }

        public async Task<Department> GetByIdAsync(int departmentId)
        {
            var one = await _myDbContext.FindAsync<Department>(departmentId);
            return one;
        }
    }
}