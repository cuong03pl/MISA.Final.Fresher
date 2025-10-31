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

        public AssetDto GetAssetDto(Guid assetId)
        {
            return _assetRepo.GetAssetDto(assetId);
        }

        public string GenerateNewAssetCode()
        {
            return _assetRepo.GenerateNewAssetCode();
        }
    }
}
