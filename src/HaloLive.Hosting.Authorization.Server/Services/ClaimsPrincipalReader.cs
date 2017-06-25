using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Builder;

namespace HaloLive.Hosting
{
	public sealed class ClaimsPrincipalReader : IClaimsPrincipalReader
	{
		/// <summary>
		/// The <see cref="IdentityOptions"/> used to configure Identity.
		/// </summary>
		private IdentityOptions Options { get; }

		public ClaimsPrincipalReader(IdentityOptions options)
		{
			if (options == null) throw new ArgumentNullException(nameof(options));

			Options = options;
		}

		/// <inheritdoc />
		public string GetUserId(ClaimsPrincipal principal)
		{
			if (principal == null)
			{
				throw new ArgumentNullException(nameof(principal));
			}
			return principal.FindFirstValue(Options.ClaimsIdentity.UserIdClaimType);
		}

		/// <inheritdoc />
		public string GetUserName(ClaimsPrincipal principal)
		{
			if (principal == null)
			{
				throw new ArgumentNullException(nameof(principal));
			}
			return principal.FindFirstValue(Options.ClaimsIdentity.UserNameClaimType);
		}
	}
}
