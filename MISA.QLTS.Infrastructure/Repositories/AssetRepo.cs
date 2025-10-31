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

        public string GenerateNewAssetCode()
        {
            var sql = @"
                SELECT asset_code
                FROM asset
                WHERE asset_code LIKE 'TS%'
                ORDER BY asset_code DESC
                LIMIT 1;
            ";

            using (var connection = new MySqlConnection(connectionString))
            {
                // Lấy trực tiếp chuỗi asset_code
                var code = connection.QueryFirstOrDefault<string>(sql);

                if (string.IsNullOrEmpty(code))
                {
                    // Nếu chưa có dữ liệu, bắt đầu từ TS001
                    return "TS00001";
                }

                // Tách phần chữ và phần số
                var match = Regex.Match(code, @"^([A-Za-z]+)(\d+)$");
                if (match.Success)
                {
                    string prefix = match.Groups[1].Value; // TS
                    string numberPart = match.Groups[2].Value;  // VD: 001

                    int number = int.Parse(numberPart) + 1;

                    // numberPart.Length: số chứ số tối thiểu
                    // nếu number.length < numberPart.Length thì sẽ thêm số 0 vào trước
                    // ngược lại thì không cần thêm số 0 và giữ nguyên 

                    string newCode = $"{prefix}{number.ToString(new string('0', numberPart.Length))}";

                   return newCode;
                }
                return code;
            }
        }


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

        public AssetDto GetAssetDto(Guid assetId)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                var data = connection.Query<AssetDto>(
                    "Proc_GetAssetById", // sua lai ten 
                    new { p_asset_id = assetId },
                    commandType: CommandType.StoredProcedure
                ).FirstOrDefault();

                return data;
            }
        }

        public AssetType GetAssetTypeByAsset(Guid asseTypetId)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                var data = connection.Query<AssetType>(
                    "Proc_GetAssetTypeByAssetId",
                    new { p_asset_id = asseTypetId },
                    commandType: CommandType.StoredProcedure
                ).FirstOrDefault();

                return data;
            }
        }
    }
}
