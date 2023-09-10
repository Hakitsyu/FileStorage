using FileStorage.SimpleClaimsAuthentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace FileStorage.UserAuthentication
{
    public class CustomAuthenticationSchemeHandler : AuthenticationHandler<CustomAuthenticationSchemeOptions>
    {
        public CustomAuthenticationSchemeHandler(IOptionsMonitor<CustomAuthenticationSchemeOptions> options, 
            ILoggerFactory logger, 
            UrlEncoder encoder, 
            ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!HasEntries())
            {
                return Task.FromResult(AuthenticateResult.NoResult());
            }

            var entriesWithHeader = GetEntriesWithHeader();
            var invalidEntries = entriesWithHeader
                .Where(e => e.Entry.Required && IsEmptyHeader(e.Header));

            if (invalidEntries.Any())
            {
                var invalidEntry = invalidEntries.First();
                return Task.FromResult(AuthenticateResult.Fail($"You need insert '{invalidEntry.Entry.HeaderKey}' header."));
            }

            var claims = entriesWithHeader.Select(e => new Claim(e.Entry.ClaimType, e.Header.Value));
            var identity = new ClaimsIdentity(claims);
            var ticket = new AuthenticationTicket(new ClaimsPrincipal(identity), CustomAuthenticationScheme.Name);
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }

        private bool HasEntries()
        {
            return Options.GetEntries().Any();
        }

        private IEnumerable<(CustomAuthenticationEntry Entry, KeyValuePair<string, StringValues> Header)> GetEntriesWithHeader()
        {
            return Options.GetEntries()
                .Select(entry =>
                {
                    var header = Context.Request.Headers.FirstOrDefault(h => h.Key == entry.HeaderKey);
                    return (Entry: entry, Header: header);
                });
        }

        private bool IsEmptyHeader(KeyValuePair<string, StringValues> header)
        {
            return header.Equals(default(KeyValuePair<string, StringValues>)) || string.IsNullOrWhiteSpace(header.Value);
        }
    }
}
