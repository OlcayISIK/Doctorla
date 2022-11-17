using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Data.Entities
{
    public class Referance : IBaseEntity
    {
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsActive { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsDeleted { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime IDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int IUser { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime? UUDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int? UUser { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string Key { get; set; }
        public string Value { get; set; }
    }
}
