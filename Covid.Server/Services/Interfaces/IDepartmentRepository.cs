using System.Collections.Generic;
using System.Threading.Tasks;
using Covid.Server.Entities;

namespace Covid.Server.Services.Interfaces
{
    public interface IDepartmentRepository
    {
        public Task<IList<Department>> GetAllAsync();
        public Task<Department> GetByIdAsync(int departmentId);
    }
}