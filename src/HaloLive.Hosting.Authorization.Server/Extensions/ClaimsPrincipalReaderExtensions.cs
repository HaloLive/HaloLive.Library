﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace HaloLive.Hosting.Authorization
{
	public static class ClaimsPrincipalReaderExtensions
	{
		/// <summary>
		/// Returns the User ID claim value if present otherwise will throw.
		/// </summary>
		/// <param name="principal">The <see cref="ClaimsPrincipal"/> instance.</param>
		/// <returns>The User ID claim value. Will throw if the principal doesn't contain the id.</returns>
		/// <exception cref="ArgumentException">Throws if the provided principal doesn't contain an id.</exception>
		/// <remarks>The User ID claim is identified by <see cref="ClaimTypes.NameIdentifier"/>.</remarks>
		public static int GetUserIdInt(this IClaimsPrincipalReader reader, ClaimsPrincipal principal)
		{
			int accountId;
			if(!int.TryParse(reader.GetUserId(principal), out accountId))
			{
				throw new ArgumentException($"Provided {nameof(ClaimsPrincipal)} does not contain a user id.", nameof(principal));
			}

			return accountId;
		}
	}
}
