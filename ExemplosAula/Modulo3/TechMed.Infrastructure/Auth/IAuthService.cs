using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechMed.Infrastructure.Auth
{
    public interface IAuthService
    {
        string GenerateJwtToken (string username, string role);

        string ComputeSha256Hash(string pass);
    }
}