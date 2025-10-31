using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Service;
using MISA.Final.Fresher.Controllers;
using MISA.QLTS.Core.Interfaces.Service;

namespace MISA.QLTS.Fresher.Controllers
{
    [Route("api/asset-types")]
    [ApiController]
    public class AssetTypesController : BasesController<AssetType>
    {
        public AssetTypesController(IAssetTypeService assetTypeService) : base(assetTypeService)
        {
            
        }
    }
}
