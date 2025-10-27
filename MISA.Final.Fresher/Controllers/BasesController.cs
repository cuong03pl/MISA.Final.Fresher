using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Interfaces.Service;

namespace MISA.Final.Fresher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasesController<T> : ControllerBase
    {
        IBaseService<T> _baseService;
        public BasesController(IBaseService<T> baseService)
        {
            _baseService = baseService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var entities = _baseService.GetAll();
            return Ok(entities);
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(Guid id)
        {
            var entity = _baseService.GetById(id);
            return Ok(entity);
        }

        [HttpPost]
        public IActionResult Post([FromBody] T entity)
        {
            var res = _baseService.Insert(entity);
            return StatusCode(201, res);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Put([FromBody] T entity, Guid id)
        {
            var res = _baseService.Update(entity, id);
            return Ok(res);
        }
        
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(Guid id)
        {
            var res = _baseService.Delete(id);
            return Ok(res);
        }
    }
}
