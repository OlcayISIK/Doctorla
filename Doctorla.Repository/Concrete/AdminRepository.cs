using Doctorla.Data.EF;
using Doctorla.Data.Entities;
using Doctorla.Data.Members;
using Doctorla.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Repository.Concrete
{
    public class AdminRepository : Repository<Admin>, IAdminRepository
    {
        public AdminRepository(Context context) : base(context)
        {
        }
    }
}
