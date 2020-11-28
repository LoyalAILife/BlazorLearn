using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Covid.Server.Entities;

namespace Covid.Server.Services.Interfaces
{
    public interface IEmployeeRepository
    {
        public Task<IList<Employee>> GetForDepartmentAsync(int departmentId);
        public Task<Employee> GetOneAsync(int id);
        public Task<Employee> AddAsync(Employee employee);
        public Task UpdateAsync(Employee employee);
        public Task DeleteAsync(int id);
    }
}