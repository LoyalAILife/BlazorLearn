using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Covid.Server.Entities;

namespace Covid.Server.Services.Interfaces
{
    public interface IDailyHealthRepository
    {
        public Task UpdateForDepartmentAsync(int departmentId, DateTime date, IList<DailyHealth> dailyHealths);
        public Task<IList<DailyHealth>> GetByDepartmentAsync(int departmentId, DateTime date);
    }
}