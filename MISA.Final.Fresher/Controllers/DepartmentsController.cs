using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Service;

namespace MISA.Final.Fresher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        IDepartmentService _departmentService;
        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var departments = _departmentService.GetAll();
            return Ok(departments);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(Guid id)
        {
            var department = _departmentService.GetById(id);
            return Ok(department);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Department department)
        {
            var res = _departmentService.Insert(department);
            return StatusCode(201, res);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Put([FromBody] Department department, Guid id)
        {
            var res = _departmentService.Update(department, id);
            return Ok(res);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(Guid id)
        {
            var res = _departmentService.Delete(id);
            return Ok(res);
        }
    }
}
