using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.SimpleClaimsAuthentication
{
    public record CustomAuthenticationEntry(string ClaimType, string HeaderKey, bool Required = true);
}
