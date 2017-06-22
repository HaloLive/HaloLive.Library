﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HaloLive.Models.NameResolution
{
	/// <summary>
	/// Enumeration of all service endpoint response results.
	/// </summary>
	public enum ResolveServiceEndpointResponseCode
	{
		/// <summary>
		/// Indicates that the request was successful.
		/// </summary>
		Success = 0,

		/// <summary>
		/// Indicates that the service is unknown or unlisted by the directory.
		/// </summary>
		ServiceUnlisted = 1,

		/// <summary>
		/// Indicates that the service is not available right now.
		/// </summary>
		ServiceUnavailable = 2,
	}
}
