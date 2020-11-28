using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Covid.Server.Services;
using Covid.Server.Services.Interfaces;
using Covid.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Covid.Server.Controllers
{
    [ApiController]
    [Route("api/department/{departmentId}/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IList<EmployeeDto>>> GetAllForDepartment(int departmentId)
        {
            var employees = await _employeeRepository.GetForDepartmentAsync(departmentId);
            var dtos = employees.Select(x => new EmployeeDto
            {
                Id = x.Id,
                DepartmentId = x.DepartmentId,
                Name = x.Name,
                BirthDate = x.BirthDate,
                Gender = x.Gender,
                No = x.No,
                PictureUrl = x.PictureUrl
            }).ToList();

            return dtos;
        }
    }
}