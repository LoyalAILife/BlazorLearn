using System.Collections.Generic;
using System.Threading.Tasks;
using Covid.Server.Entities;
using Covid.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Covid.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetAll()
        {
            var departments = await _departmentRepository.GetAllAsync();
            return Ok(departments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Department>>> GetOne(int id)
        {
            var department = await _departmentRepository.GetByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            return Ok(department);
        }
    }
}