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
        public ZoomApi Zoom { get; set; }
        public IyzicoAPI Iyzico { get; set; }
    }

    public class TokenOptions
    {
        public string AdminSecretKey { get; set; }
        public string UserSecretKey { get; set; }
        public string DoctorSecretKey { get; set; }
        public string HospitalSecretKey { get; set; }
        public int AccessTokenLifetime { get; set; } // Minutes
        public int RefreshTokenLifetime { get; set; } // Minutes
    }
    public class IyzicoAPI
    {
        public string ApiKey { get; set; }
        public string SecretKey { get; set; }
        public string Url { get; set; }
    }

    public class ZoomApi
    {
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
        public string Topic { get; set; }
        public string Email { get; set; }
    }
}

