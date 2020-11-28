using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Covid.Server.Data;
using Covid.Server.Entities;
using Covid.Server.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Covid.Server.Services
{
    public class DailyHealthRepository : IDailyHealthRepository
    {
        private readonly MyDbContext _myDbContext;

        public DailyHealthRepository(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }
        public async Task UpdateForDepartmentAsync(int departmentId, DateTime date, IList<DailyHealth> dailyHealths)
        {
            var employeeIds = await _myDbContext.Employees
                .Where(x => x.DepartmentId == departmentId)
                .Select(x => x.Id)
                .ToListAsync();

            var inDb = await _myDbContext.DailyHealths
                .Where(x => x.Date == date && employeeIds.Contains(x.EmployeeId))
                .ToListAsync();

            foreach (var dbItem in inDb)
            {
                var one = dailyHealths
                    .SingleOrDefault(x => x.EmployeeId == dbItem.EmployeeId && x.Date == dbItem.Date);

                if (one != null)
                {
                    dbItem.HealthCondition = one.HealthCondition;
                    dbItem.Temperature = one.Temperature;
                    dbItem.Remark = one.Remark;

                    _myDbContext.Update(dbItem);
                }
            }

            var dbKeys = inDb.Select(x => new
            {
                x.EmployeeId
                ,
                x.Date
            }).ToList();

            var incomingKeys = dailyHealths.Select(x => new
            {
                x.EmployeeId,
                x.Date
            }).ToList();

            var toAddKeys = incomingKeys.Except(dbKeys);        // todo
            foreach (var addKey in toAddKeys)
            {
                var toAdd = dailyHealths.Single(x => x.EmployeeId == addKey.EmployeeId
                                                                    && x.Date == addKey.Date);

                await _myDbContext.AddAsync(toAdd);
            }

            var toRemoveKeys = dbKeys.Except(incomingKeys);     // todo
            foreach (var removeKey in toRemoveKeys)
            {
                var toRemove = inDb.Single(x => x.EmployeeId == removeKey.EmployeeId
                                                && x.Date == removeKey.Date);

                _myDbContext.Remove(toRemove);
            }

            await _myDbContext.SaveChangesAsync();
        }

        public async Task<IList<DailyHealth>> GetByDepartmentAsync(int departmentId, DateTime date)
        {
            var employeeIds = await _myDbContext.Employees
                .Where(x => x.DepartmentId == departmentId)
                .Select(x => x.Id)
                .ToListAsync();

            var dailyHealths = await _myDbContext.DailyHealths
                .Where(x => x.Date == date && employeeIds.Contains(x.EmployeeId))
                .ToListAsync();

            return dailyHealths;
        }
    }
}