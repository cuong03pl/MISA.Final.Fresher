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
    public class DepartmentRepo : BaseRepo<Department>, IDepartmentRepo
    {
        public DepartmentRepo(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
