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
        AssetPagedResult GetAllDto(string? q, string? departmentCode, string? assetTypeCode, int? pageNumber, int? pageSize);
        AssetDto GetAssetDto(Guid assetId);
        AssetType GetAssetTypeByAsset(Guid assetId);

        string GenerateNewAssetCode();
    }
}
