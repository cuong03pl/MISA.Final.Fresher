using MISA.Core.Entities;
using MISA.Core.Exceptions;
using MISA.Core.Interfaces.Repository;
using MISA.Core.Interfaces.Service;
using MISA.QLTS.Core.DTOs.Asset;
using MISA.QLTS.Core.DTOs.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Services
{
    public class AssetService : BaseService<Asset>, IAssetService
    {
        private readonly IAssetRepo _assetRepo;
        
        public AssetService(IAssetRepo assetRepo) : base(assetRepo) {
            _assetRepo = assetRepo;
        }

        /// <summary>
        /// Lấy tất cả tài sản theo DTO
        /// </summary>
        /// <returns>Danh sách tài sản theo DTO</returns>
        /// CreatedBy: HKC (29/10/2025)
        public AssetPagedResult GetAllDto(string? q, string? departmentCode, string? assetTypeCode, int? pageNumber, int? pageSize)
        {
            return _assetRepo.GetAllDto(q, departmentCode, assetTypeCode, pageNumber, pageSize);
        }

        /// <summary>
        /// Xử lý custom validate cho tài sản
        /// </summary>
        /// <param name="asset">Thông tin tài sản</param>
        /// <exception cref="ValidateException"></exception>
        public override void CustomValidate(Asset asset)
        {
            var assetType = _assetRepo.GetAssetTypeByAsset(asset.AssetTypeId);
            var a = asset.Price * assetType.DepreciationRate / 100;
            var b = asset.AnnualDepreciation;
            if (asset.Price * assetType.DepreciationRate / 100 != asset.AnnualDepreciation)
            {
                throw new ValidateException("Giá trị hao mòn năm không hợp lệ.");
            }
        }

        /// <summary>
        /// Lấy 1 tài sản theo DTO
        /// </summary>
        /// <returns>Tài sản theo DTO</returns>
        /// CreatedBy: HKC (30/10/2025)
        public AssetDto GetAssetDto(Guid assetId)
        {
            return _assetRepo.GetAssetDto(assetId);
        }

        /// <summary>
        /// Hàm sinh mã tài sản mới theo tiền tố TS
        /// Công thức: Lấy cái mới nhất ra sau đó + 1 
        /// </summary>
        /// <returns>Mã tài sản mới</returns>
        /// CreatedBy: HKC (31/10/2025)
        public string GenerateNewAssetCode()
        {
            return _assetRepo.GenerateNewAssetCode();
        }
    }
}
