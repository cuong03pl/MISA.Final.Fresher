using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Service;
using MISA.Core.Services;
using MISA.QLTS.Core.DTOs.Response;

namespace MISA.Final.Fresher.Controllers
{
    [Route("api/assets")]
    [ApiController]
    public class AssetsController : BasesController<Asset>
    {
        private readonly IAssetService _assetService;

        public AssetsController(IAssetService assetService) : base(assetService)
        {
            _assetService = assetService;
        }

        /// <summary>
        /// API lấy tất cả tài sản theo DTO
        /// </summary>
        /// <returns>Danh sách tài sản theo DTO</returns>
        /// CreatedBy: HKC (29/10/2025)
        [HttpGet]
        [Route("paging")]
        public IActionResult Get([FromQuery] string? q, [FromQuery] string? departmentCode, [FromQuery] string? assetTypeCode, [FromQuery] int? pageNumber = 1, [FromQuery] int? pageSize = 20)
        {
                var assets = _assetService.GetAllDto(q, departmentCode, assetTypeCode, pageNumber, pageSize);
                return Ok(ServiceResponse<object>.Ok(assets, "Lấy dữ liệu thành công"));
           
        }

        /// <summary>
        /// API lấy tài sản bởi id theo DTO
        /// </summary>
        /// <returns>Danh sách tài sản theo DTO</returns>
        /// CreatedBy: HKC (29/10/2025)
        [HttpGet]
        [Route("{id}")]
        public override IActionResult GetById(Guid id, string? mode)
        {
                var entity = _assetService.GetAssetDto(id, mode);
                return Ok(ServiceResponse<object>.Ok(entity, "Lấy dữ liệu thành công"));
        }

    }
}
