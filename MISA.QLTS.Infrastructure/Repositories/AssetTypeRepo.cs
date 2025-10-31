using Microsoft.Extensions.Configuration;
using MISA.Core.Entities;
using MISA.Infrastructure.Repositories;
using MISA.QLTS.Core.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.QLTS.Infrastructure.Repositories
{
    public class AssetTypeRepo : BaseRepo<AssetType>, IAssetTypeRepo
    {
        public AssetTypeRepo(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
