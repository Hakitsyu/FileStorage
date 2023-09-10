using FileStorage.SimpleClaimsAuthentication;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FileStorage.UserAuthentication
{
    public sealed class CustomAuthenticationSchemeOptions : AuthenticationSchemeOptions
    {
        private readonly List<CustomAuthenticationEntry> _entries = new();

        internal IReadOnlyCollection<CustomAuthenticationEntry> GetEntries()
        {
            return _entries;
        }

        public void Add(string claimType, string headerKey, bool required = false)
        {
            if (string.IsNullOrEmpty(claimType))
                throw new ArgumentNullException(nameof(claimType));
            if (string.IsNullOrEmpty(headerKey))
                throw new ArgumentNullException(nameof(headerKey));

            _entries.Add(new CustomAuthenticationEntry(claimType, headerKey, required));
        }
    }
}