using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Service;

namespace MISA.Final.Fresher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : BasesController<Department>
    {
        IDepartmentService _departmentService;
        public DepartmentsController(IDepartmentService departmentService) : base(departmentService)
        {
            _departmentService = departmentService;
        }

       
    }
}
