using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Service;

namespace MISA.Final.Fresher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : BasesController<Asset>
    {
        public AssetsController(IAssetService assetService) : base(assetService)
        {
        }
    }
}
