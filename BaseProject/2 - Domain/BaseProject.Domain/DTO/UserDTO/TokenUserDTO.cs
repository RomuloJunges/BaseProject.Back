using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BaseProject.Domain.Enum;

namespace BaseProject.Domain.DTO.UserDTO
{
    public class TokenUserDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Status Status { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
