using MISA.Core.Entities;
using MISA.Core.Services;
using MISA.QLTS.Core.Interfaces.Repository;
using MISA.QLTS.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QLTS.Core.Services
{
    public class AssetTypeService : BaseService<AssetType> , IAssetTypeService
    {
        public AssetTypeService(IAssetTypeRepo assetTypeRepo) : base(assetTypeRepo)
        {
        }
    }
}
