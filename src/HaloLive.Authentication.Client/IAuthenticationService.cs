using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HaloLive.Models.Authentication;
using TypeSafe.Http.Net;

namespace HaloLive.Network
{
	/// <summary>
	/// Proxy interface for Authentication Server RPCs.
	/// </summary>
	public interface IAuthenticationService
	{
		/// <summary>
		/// Authenticate request method. Sends the request model as a URLEncoded body.
		/// See the documentation for information about the endpoint.
		/// https://github.com/HaloLive/Documentation
		/// </summary>
		/// <param name="request">The request model.</param>
		/// <returns>The authentication result.</returns>
		[Post("/api/auth")]
		Task <JWTModel> TryAuthenticate([UrlEncodedBody] AuthenticationRequestModel request);
	}
}
