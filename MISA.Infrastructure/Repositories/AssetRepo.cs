using Microsoft.Extensions.Configuration;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructure.Repositories
{
    public class AssetRepo : BaseRepo<Asset>, IAssetRepo
    {
        public AssetRepo(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
