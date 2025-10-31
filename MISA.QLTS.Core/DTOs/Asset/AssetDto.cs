using MISA.Core.MISAAttributes;
using MISA.QLTS.Core.MISAAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QLTS.Core.DTOs.Asset
{
    /// <summary>
    /// Đại diện cho thông tin tài sản
    /// </summary>
    public class AssetDto
    {
        /// <summary>
        /// Số thứ tự
        /// </summary>
        public int RowIndex { get; set; }

        /// <summary>
        /// ID tài sản
        /// </summary>
        public Guid AssetId { get; set; }

        /// <summary>
        /// Mã tài sản
        /// </summary>
        public string AssetCode { get; set; }

        /// <summary>
        /// Tên tài sản 
        /// </summary>
        public string AssetName { get; set; }

        /// <summary>
        /// Số lượng (số nguyên dương)
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Ngày bắt đầu sử dụng
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Ngày mua tài sản
        /// </summary>
        public DateTime PurchaseDate { get; set; }

        /// <summary>
        /// Năm sử dụng
        /// </summary>
        public int UseYear { get; set; }

        /// <summary>
        /// Nguyên giá (VNĐ)
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Giá trị hao mòn năm (tự động tính)
        /// </summary>
        public decimal? AnnualDepreciation { get; set; }

        /// <summary>
        /// Giá trị còn lại của tài sản sau khấu hao
        /// </summary>
        public decimal? ResidualValue { get; set; }

        /// <summary>
        /// Id bộ phận sử dụng
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Mã bộ phận
        /// </summary>
        public string DepartmentCode { get; set; }

        /// <summary>
        /// Tên bộ phận
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// Id loại tài sản
        /// </summary>
        public Guid AssetTypeId { get; set; }

        /// <summary>
        /// Mã loại tài sản
        /// </summary>
        public string AssetTypeCode { get; set; }

        /// <summary>
        /// Tên loại tài sản
        /// </summary>
        public string AssetTypeName { get; set; }

        /// <summary>
        /// Tỷ lệ hao mòn (%)
        /// </summary>
        public decimal DepreciationRate { get; set; }

    }
}
