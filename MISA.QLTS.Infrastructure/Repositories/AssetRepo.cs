using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Repository;
using MISA.QLTS.Core.DTOs.Asset;
using MISA.QLTS.Core.DTOs.Paging;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.Infrastructure.Repositories
{
    public class AssetRepo : BaseRepo<Asset>, IAssetRepo
    {
        public AssetRepo(IConfiguration configuration) : base(configuration)
        {
        }
       

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
        public AssetPagedResult GetAllDto(string? q, string? departmentCode, string? assetTypeCode, int? pageNumber, int? pageSize)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                using (var multi = connection.QueryMultiple(
                    "proc_get_all_assets",
                    new
                    {
                        p_query = q,
                        p_department_code = departmentCode,
                        p_asset_type_code = assetTypeCode,
                        p_page_number = pageNumber,
                        p_page_size = pageSize
                    },
                    commandType: CommandType.StoredProcedure
                ))
                {
                    var data = multi.Read<AssetDto>().ToList();
                    var totals = multi.ReadSingleOrDefault<AssetPagedResult>();

                    return new AssetPagedResult
                    {
                        Data = data,
                        TotalRecords = totals.TotalRecords,
                        QuantityTotal = totals.QuantityTotal,
                        PriceTotal = totals.PriceTotal,
                        AnnualDepreciationTotal = totals.AnnualDepreciationTotal,
                        RemainingValueTotal = totals.RemainingValueTotal

                    };
                }
            }
        }

        /// <summary>
        /// Lấy 1 tài sản theo DTO
        /// </summary>
        /// <param name="assetId">Id tài sản</param>
        /// <returns>Tài sản theo DTO</returns>
        /// CreatedBy: HKC (30/10/2025)
        public AssetDto GetAssetDto(Guid assetId, string mode)
        {
            var newCode = GenerateNewCode();
            using (var connection = new MySqlConnection(connectionString))
            {
                var data = connection.Query<AssetDto>(
                    "proc_get_asset_by_id", // sua lai ten 
                    new { p_asset_id = assetId },
                    commandType: CommandType.StoredProcedure
                ).FirstOrDefault();
                if (data == null)
                {
                    return null;
                }
                if (mode == "duplicate")
                {
                    data.AssetCode = newCode;
                }
              
                return data;
            }
        }

        /// <summary>
        /// Xử lý lấy loại tài sản theo Id tài sản
        /// </summary>
        /// <param name="asseTypetId">Id loại tài sản</param>
        /// <returns>Loại tài sản</returns>
        /// CreatedBy: HKC (30/10/2025)
        public AssetType GetAssetTypeByAsset(Guid asseTypetId)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                var data = connection.Query<AssetType>(
                    "proc_get_asset_type_by_id",
                    new { p_asset_id = asseTypetId },
                    commandType: CommandType.StoredProcedure
                ).FirstOrDefault();

                return data;
            }
        }
    }
}
