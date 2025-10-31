using MISA.Core.MISAAttributes;
using MISA.QLTS.Core.MISAAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QLTS.Core.DTOs.Asset
{
    public class AssetDto
    {
        public int RowIndex { get; set; }
        public Guid AssetId { get; set; }
        public string AssetCode { get; set; }
        public string AssetName { get; set; }
        public int Quantity { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime PurchaseDate { get; set; }
        public int UseYear { get; set; }
        public decimal Price { get; set; }
        public decimal? AnnualDepreciation { get; set; }
        public decimal? ResidualValue { get; set; }

        public Guid DepartmentId { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }

        public Guid AssetTypeId { get; set; }
        public string AssetTypeCode { get; set; }
        public string AssetTypeName { get; set; }
        public decimal DepreciationRate { get; set; }

    }
}
