using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Net;
using System.Text;

namespace Doctorla.Core.Communication
{
    public static class MeetingCreator
    {
        public static string CreateMeeting()
        {
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var now = DateTime.UtcNow;
            var apiSecret = "0b5IB0mHKQzeAPEyiDl5mAfLt25Fwl4k7bvS";
            byte[] symmetricKey = Encoding.ASCII.GetBytes(apiSecret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "JYWuSkOtQn67wDdQFrSfQQ",
                Expires = now.AddSeconds(300),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            var client = new RestClient("https://api.zoom.us/v2/users/bsvsdwnfletosmrvqr@tmmbt.com/meetings");
            var request = new RestRequest();
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(new { topic = "Meeting with Ussain", duration = "10", start_time = "2021-03-20T05:00:00", type = "2" });
            request.AddHeader("authorization", String.Format("Bearer {0}", tokenString));
            request.Method = Method.Post;

            var restResponse = client.Execute(request);
            HttpStatusCode statusCode = restResponse.StatusCode;
            int numericStatusCode = (int)statusCode;
            var jObject = JObject.Parse(restResponse.Content);
            var Host = (string)jObject["start_url"];
            var Join = (string)jObject["join_url"];
            var Code = Convert.ToString(numericStatusCode);

            return Host;
        }
    }
}
