using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Interfaces.Service;
using MISA.QLTS.Core.DTOs.Response;

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

        /// <summary>
        /// Lấy tất cả bản ghi của bảng trong database
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        /// CreatedBy: HKC (27/10/2025)

        [HttpGet]
        public virtual IActionResult Get()
        {
                var entities = _baseService.GetAll();
                return Ok(ServiceResponse<IEnumerable<T>>.Ok(entities, "Lấy dữ liệu thành công"));
        }

        /// <summary>
        /// Lấy bản ghi theo Id và mode 
        /// </summary>
        /// <param name="id">Id bản ghi cần lấy</param>
        /// <param name="mode"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public virtual IActionResult GetById(Guid id,[FromQuery] string? mode)
        {
                var entity = _baseService.GetById(id);
                return Ok(ServiceResponse<T>.Ok(entity, "Lấy dữ liệu thành công"));
        }

        /// <summary>
        /// Xử lý thêm mới bản ghi
        /// </summary>
        /// <param name="entity">Dữ liệu cần thêm</param>
        /// <returns>Số bản ghi bị ảnh hưởng trong database (1 - Thành công, 0 - Thất bại)</returns>
        /// CreatedBy: HKC (27/10/2025)
        [HttpPost]
        public virtual IActionResult Post([FromBody] T entity)
        {
                var res = _baseService.Insert(entity);
                return StatusCode(201, ServiceResponse<int>.Ok(res, "Thêm mới thành công"));
        }

        /// <summary>
        /// Xử lý cập nhật bản ghi
        /// </summary>
        /// <param name="entity">Dữ liệu thay đổi</param>
        /// <param name="id">Id của bản ghi cần thay đổi</param>
        /// <returns>Số bản ghi bị ảnh hưởng trong database (1 - Thành công, 0 - Thất bại)</returns>
        /// CreatedBy: HKC (27/10/2025)
        [HttpPut]
        [Route("{id}")]
        public virtual IActionResult Put([FromBody] T entity, Guid id)
        {
                var res = _baseService.Update(entity, id);
                return Ok(ServiceResponse<int>.Ok(res, "Cập nhật thành công"));
          
        }

        /// <summary>
        /// Xử lý xóa bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi cần xóa</param>
        /// <returns>Số bản ghi bị ảnh hưởng trong database (1 - Thành công, 0 - Thất bại)</returns>
        /// CreatedBy: HKC (27/10/2025)
        [HttpDelete]
        [Route("{id}")]
        public virtual IActionResult Delete(Guid id)
        {
                var res = _baseService.Delete(id);
                return Ok(ServiceResponse<int>.Ok(res, "Xóa thành công"));
            
            
        }

        /// <summary>
        /// Xử lý xóa nhiều bản ghi
        /// </summary>
        /// <param name="ids">Danh sách id bản ghi cần xóa</param>
        /// <returns></returns>
        [HttpPost]
        [Route("delete-multiple")]
        public virtual IActionResult DeleteMultiple([FromBody] List<Guid> ids)
        {
                _baseService.DeleteMutiple(ids);
                return Ok(ServiceResponse<int>.Ok(ids.Count, "Xóa thành công"));
            
           
        }

        /// <summary>
        /// Hàm sinh mã mới theo tiền tố
        /// Công thức: Lấy cái mới nhất ra sau đó + 1 
        /// </summary>
        /// <returns>Mã tài sản mới</returns>
        /// CreatedBy: HKC (01/11/2025)
        [HttpGet]
        [Route("new-code")]
        public virtual IActionResult GetNewCode()
        {
                var newCode = _baseService.GenerateNewCode();
                return Ok(ServiceResponse<string>.Ok(newCode, "Sinh mã mới thành công"));
        }
    }
}
