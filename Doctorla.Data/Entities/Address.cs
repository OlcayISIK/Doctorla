using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Doctorla.Data.Entities.Region;
using Doctorla.Data.Entities.SystemUsers;
using Doctorla.Data.Members;

namespace Doctorla.Data.Entities
{
    public class Address : Entity
    {
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public int CountyId { get; set; }
        public string Name { get; set; }
        public string AddressDetail { get; set; }
        public int UserId { get; set; }
        public int TypeId { get; set; }

        public AddressType AddressType { get; set; }
        public Country Country { get; set; }
        public City City { get; set; }
        public County County { get; set; }
        [JsonIgnore]
        public User User { get; set; }
    }
}
