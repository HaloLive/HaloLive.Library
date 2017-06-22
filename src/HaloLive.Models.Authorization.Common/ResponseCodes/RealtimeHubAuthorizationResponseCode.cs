using System;
using System.Collections.Generic;
using System.Text;

namespace HaloLive.Models.Authorization
{
	/// <summary>
	/// Enumeration of all authorization response results.
	/// </summary>
	public enum RealtimeHubAuthorizationResponseCode
	{
		/// <summary>
		/// Indicates that the authorization was successful.
		/// </summary>
		Success = 0,

		/// <summary>
		/// Indicates that the hub connection was already authorized for this client.
		/// </summary>
		AlreadyAuthorized = 1,

		/// <summary>
		/// Indicates that the hub connection was already authorized but for a different account/user.
		/// (Ex. a begin known exploit to this system allows for a very short tiny window for MITM to auth their account for you)
		/// </summary>
		ConnectionAuthorizedToDifferentClient = 2,

		/// <summary>
		/// Indicates that the service is unavailable for an unknown reason.
		/// </summary>
		GeneralServiceUnavailable = 3,

		/// <summary>
		/// Indicates that the authorization request was sent with invalid authorization credentials.
		/// </summary>
		InvalidAuthorizationCredentials = 4

		//TODO: Add more
	}
}
