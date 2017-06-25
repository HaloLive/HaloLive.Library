using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace HaloLive.Hosting
{
	/// <summary>
	/// Claims reader based on https://github.com/aspnet/Identity/blob/f555a26b4a554f73eea70b4b34fca823fab9a643/src/Microsoft.Extensions.Identity.Core/UserManager.cs
	/// </summary>
	/// <typeparam name="TUser">The user</typeparam>
	public interface IClaimsPrincipalReader
	{
		/// <summary>
		/// Returns the User ID claim value if present otherwise returns null.
		/// </summary>
		/// <param name="principal">The <see cref="ClaimsPrincipal"/> instance.</param>
		/// <returns>The User ID claim value, or null if the claim is not present.</returns>
		/// <remarks>The User ID claim is identified by <see cref="ClaimTypes.NameIdentifier"/>.</remarks>
		string GetUserId(ClaimsPrincipal principal);

		/// <summary>
		/// Returns the Name claim value if present otherwise returns null.
		/// </summary>
		/// <param name="principal">The <see cref="ClaimsPrincipal"/> instance.</param>
		/// <returns>The Name claim value, or null if the claim is not present.</returns>
		/// <remarks>The Name claim is identified by <see cref="ClaimsIdentity.DefaultNameClaimType"/>.</remarks>
		string GetUserName(ClaimsPrincipal principal);
	}
}
