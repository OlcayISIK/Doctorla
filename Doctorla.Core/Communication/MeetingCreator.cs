using Doctorla.Core.InternalDtos;
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
        public static string CreateMeeting(ZoomApi zoomApi)
        {
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var symmetricKey = Encoding.ASCII.GetBytes(zoomApi.ApiSecret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = zoomApi.ApiKey,
                Expires = DateTime.UtcNow.AddSeconds(300),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            var request = new RestRequest
            {
                Method = Method.Post,
                RequestFormat = DataFormat.Json
            };
            request.AddJsonBody(new { topic = zoomApi.Topic, type = "2" });
            request.AddHeader("authorization", String.Format($"Bearer {tokenString}"));

            var client = new RestClient($"https://api.zoom.us/v2/users/{zoomApi.Email}/meetings");
            var restResponse = client.Execute(request);
            var jObject = JObject.Parse(restResponse.Content);
            var Host = (string)jObject["start_url"];
            var Join = (string)jObject["join_url"];

            return $"DoctorlaHost:{Host}DoctorlaJoin:{Join}";
        }
    }
}
