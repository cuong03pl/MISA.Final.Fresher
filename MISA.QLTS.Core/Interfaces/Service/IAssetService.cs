using MISA.Core.Entities;
using MISA.QLTS.Core.DTOs.Asset;
using MISA.QLTS.Core.DTOs.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Service
{
    public interface IAssetService : IBaseService<Asset>
    {
        /// <summary>
        /// Lấy tất cả tài sản theo DTO
        /// </summary>
        /// <returns>Danh sách tài sản theo DTO</returns>
        /// CreatedBy: HKC (29/10/2025)
        AssetPagedResult GetAllDto(string? q, string? departmentCode, string? assetTypeCode, int? pageNumber, int? pageSize);

        /// <summary>
        /// Lấy 1 tài sản theo DTO
        /// </summary>
        /// <returns>Tài sản theo DTO</returns>
        /// CreatedBy: HKC (30/10/2025)
        AssetDto GetAssetDto(Guid assetId, string mode);

    }
}
