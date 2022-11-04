using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctorla.Core.InternalDtos
{
    public class AppSettings
    {
        public string RedisConnectionString { get; set; }
        public string DatabaseConnectionString { get; set; }
        public TokenOptions TokenOptions { get; set; }
        public EmailSettings EmailSettings { get; set; }
        public int PageSize { get; set; }
        public string ClientUrl { get; set; }
        public string FileSystemImageBasePath { get; set; }
        public string FileSystemAudioBasePath { get; set; }
        public string DevelopmentApiUrl { get; set; }
        public string UatApiUrl { get; set; }
        public string GoogleAPIKey { get; set; }
    }

    public class TokenOptions
    {
        public string SecretKey { get; set; }
        public string CustomerSecretKey { get; set; }
        public string AdminSecretKey { get; set; }
        public int AccessTokenLifetime { get; set; } // Minutes
        public int RefreshTokenLifetime { get; set; } // Minutes
    }
}
}
