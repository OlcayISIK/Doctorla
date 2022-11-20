using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Doctorla.Data.Entities
{
    public class AddressType : Entity, IType
    {
        public string Name { get; set; }
        public string DefaultName { get; set; }
        [JsonIgnore]
        public ICollection<Address> Address { get; set; }
    }
}
