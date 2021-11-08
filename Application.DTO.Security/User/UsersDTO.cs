using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO.Security
{
    public class UsersDTO
    {
        public string UserId { get; set; }
    }

    public class CredentialDTO : UsersDTO
    {
        public string Password { get; set; }
        public string IP { get; set; }
        public string UserHostAddress { get; set; }
        public string OperatingSystem { get; set; }
        public string Browser { get; set; }
        public string Server { get; set; }
        public int ApplicationId { get; set; }
    }

}
