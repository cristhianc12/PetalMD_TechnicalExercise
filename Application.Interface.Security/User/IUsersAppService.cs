using Application.DTO.Security;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Security
{
    public interface IUsersAppService
    {
        Task<string> Login(CredentialDTO oCredentialDTO);
    }
}
