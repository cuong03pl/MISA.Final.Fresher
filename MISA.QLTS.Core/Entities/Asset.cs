using MISA.Core.MISAAttributes;
using MISA.QLTS.Core.MISAAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    /// <summary>
    /// Thông tin tài sản
    /// </summary>
    [TableAttribute("asset")]
    public class Asset : AuditInfo
    {
        /// <summary>
        /// Id tài sản
        /// </summary>
        [ColumnNameAttribute("asset_id")]
        public Guid AssetId { get; set; }

        /// <summary>
        /// Mã tài sản
        /// </summary>
        [NotEmptyAttribute("Mã tài sản")]
        [ColumnNameAttribute("asset_code")]
        [UniqueAttribute("Mã tài sản")]
        public string AssetCode { get; set; }

        /// <summary>
        /// Tên tài sản
        /// </summary>
        [NotEmptyAttribute("Tên tài sản")]
        [ColumnNameAttribute("asset_name")]
        public string AssetName { get; set; }

        /// <summary>
        /// Ngày mua
        /// </summary>
        [NotEmpty("Ngày mua")]
        [ColumnNameAttribute("purchase_date")]
        public DateTime PurchaseDate { get; set; }

        /// <summary>
        /// Số lượng (số nguyên dương)
        /// </summary>
        [NotEmpty("Số lượng")]
        [ColumnNameAttribute("quantity")]
        public int Quantity { get; set; }

        /// <summary>
        /// Nguyên giá (VNĐ)
        /// </summary>
        [NotEmpty("Nguyên giá")]
        [ColumnNameAttribute("price")]
        public decimal Price { get; set; }

        /// <summary>
        /// Giá trị hao mòn năm (tự động tính)
        /// </summary>
        [ColumnNameAttribute("annual_depreciation")]
        public decimal? AnnualDepreciation { get; set; }

        /// <summary>
        /// Id bộ phận sử dụng
        /// </summary>
        [NotEmpty("Bộ phận sử dụng")]
        [ColumnNameAttribute("department_id")]
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Id loại tài sản
        /// </summary>
        [ColumnNameAttribute("asset_type_id")]
        [NotEmpty("Loại tài sản")]
        public Guid AssetTypeId { get; set; }
    }
}
