using MISA.Core.Entities;
using MISA.QLTS.Core.DTOs.Asset;
using MISA.QLTS.Core.DTOs.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Repository
{
    public interface IAssetRepo : IBaseRepo<Asset>
    {
        /// <summary>
        /// Hàm lấy tất cả tài sản theo DTO và phân trang
        /// </summary>
        /// <param name="q">Từ khoá tìm kiếm</param>
        /// <param name="departmentCode">Mã bộ phận cần lọc</param>
        /// <param name="assetTypeCode">Mã loại tài sản cần lọc</param>
        /// <param name="pageNumber">Trang hiện tại</param>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <returns>Tất cả tài sản theo điều kiện lọc, tìm kiếm và phân trang</returns>
        /// CreatedBy: HKC (30/10/2025)
        AssetPagedResult GetAllDto(string? q, string? departmentCode, string? assetTypeCode, int? pageNumber, int? pageSize);

        /// <summary>
        /// Lấy 1 tài sản theo DTO
        /// </summary>
        /// <param name="assetId">Id tài sản</param>
        /// <returns>Tài sản theo DTO</returns>
        /// CreatedBy: HKC (30/10/2025)
        AssetDto GetAssetDto(Guid assetId);

        /// <summary>
        /// Xử lý lấy loại tài sản theo Id tài sản
        /// </summary>
        /// <param name="asseTypetId">Id loại tài sản</param>
        /// <returns>Loại tài sản</returns>
        /// CreatedBy: HKC (30/10/2025) 
        AssetType GetAssetTypeByAsset(Guid assetId);


        /// <summary>
        /// Hàm sinh mã tài sản mới theo tiền tố TS
        /// Công thức: Lấy cái mới nhất ra sau đó + 1 
        /// </summary>
        /// <returns>Mã tài sản mới</returns>
        /// CreatedBy: HKC (31/10/2025)
        string GenerateNewAssetCode();
    }
}
